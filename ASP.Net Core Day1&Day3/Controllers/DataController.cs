using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using static DataController;

[Route("api/[controller]")]
[ApiController]
public class DataController : ControllerBase
{
    private const string WeatherApiKey = "564f9dd8a1464c58a3981325251802";
    private const string WeatherApiUrl = "http://api.weatherapi.com/v1/current.json";
    private readonly FileService _fileService;

    public DataController(FileService fileService)
    {
        _fileService = fileService;
    }

    // Save Data to File
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
            await _fileService.WriteToFileAsync(jsonString);

            return Ok(new ResponseModel(true, "Data saved successfully."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResponseModel(false, $"Internal server error: {ex.Message}"));
        }
    }

    // Read Data from File
    [HttpGet("read")]
    public async Task<IActionResult> ReadData()
    {
        try
        {
            string fileContent = await _fileService.ReadFromFileAsync();

            if (string.IsNullOrWhiteSpace(fileContent))
            {
                return NotFound(new ResponseModel(false, "No data found in the file."));
            }
            var deserializedData = JsonConvert.DeserializeObject<SavedWeatherData>(fileContent);

            return Ok(new { Success = true, Data = deserializedData });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResponseModel(false, $"Internal server error: {ex.Message}"));
        }
    }

    [HttpPost("AhmWeather")]
    public async Task<IActionResult> GetAhmData([FromBody] CityRequest cityRequest)
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

            //var deserializedData = JsonConvert.DeserializeObject<SavedWeatherData>(dataToSave);

            return Ok(new { Success = true, Data = dataToSave });
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
                dynamic weatherResponse = JsonConvert.DeserializeObject<dynamic>(response);

                // Ensure response contains valid weather data
                if (weatherResponse == null || weatherResponse.current == null)
                {
                    return null;
                }

                var currentWeather = weatherResponse.current;

                return new
                {
                    Temperature = $"{currentWeather.temp_c}°C",
                    Condition = (string)currentWeather.condition.text,
                    Wind = $"{currentWeather.wind_kph} km/h",
                    Humidity = $"{currentWeather.humidity}%",
                    FeelsLike = $"{currentWeather.feelslike_c}°C"
                };
            }
            catch (Exception)
            {
                return null; // Handle exceptions properly (log them if necessary)
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

    // Define a model to match the saved data structure
    public class SavedWeatherData
    {
        public string Name { get; set; }
        public string City { get; set; }
        public WeatherInfo Weather { get; set; }
        public string DateTime { get; set; }
    }

    public class WeatherInfo
    {
        public string Temperature { get; set; }
        public string Condition { get; set; }
        public string Wind { get; set; }
        public string Humidity { get; set; }
        public string FeelsLike { get; set; }
    }
}
