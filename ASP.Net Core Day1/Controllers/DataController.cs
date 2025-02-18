using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

[Route("api/[controller]")]
[ApiController]
public class DataController : ControllerBase
{
    private const string WeatherApiKey = "564f9dd8a1464c58a3981325251802";
    private const string WeatherApiUrl = "http://api.weatherapi.com/v1/current.json";

    [HttpPost("save")]
    public async Task<IActionResult> SaveData([FromBody] CityRequest cityRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResponseModel(false, "Invalid data. Ensure Name and City are provided."));
        }

        string normalizedCity = NormalizeCity(cityRequest.City);

        try
        {
            var weatherData = await GetWeatherData(normalizedCity);

            if (weatherData == null)
            {
                return BadRequest(new ResponseModel(false, $"City '{normalizedCity}' not found or an error occurred fetching weather data."));
            }

            var dataToSave = new
            {
                Name = cityRequest.Name,
                City = normalizedCity,
                Weather = weatherData,
                DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            string jsonString = JsonConvert.SerializeObject(dataToSave, Formatting.Indented);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "SavedData.txt");

            await System.IO.File.AppendAllTextAsync(filePath, jsonString + Environment.NewLine + Environment.NewLine);

            return Ok(new ResponseModel(true, "Data saved successfully."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResponseModel(false, $"Internal server error: {ex.Message}"));
        }
    }

    private string NormalizeCity(string city)
    {
        if (string.IsNullOrEmpty(city))
        {
            throw new ArgumentException("City name cannot be empty.");
        }
        return city.Trim().ToLower();
    }

    private async Task<object> GetWeatherData(string city)
    {
        using (var client = new HttpClient())
        {
            try
            {
                string requestUrl = $"{WeatherApiUrl}?key={WeatherApiKey}&q={city}&aqi=no";
                var response = await client.GetStringAsync(requestUrl);
                dynamic weatherResponse = JsonConvert.DeserializeObject(response);

                if (weatherResponse.error != null)
                {
                    return null;
                }

                var currentWeather = weatherResponse.current;
                return new
                {
                    Temperature = currentWeather.temp_c + "°C",
                    Condition = currentWeather.condition.text,
                    Wind = currentWeather.wind_kph + " km/h",
                    Humidity = currentWeather.humidity + "%",
                    FeelsLike = currentWeather.feelslike_c + "°C"
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public class CityRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
    }

    public class ResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public ResponseModel(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
