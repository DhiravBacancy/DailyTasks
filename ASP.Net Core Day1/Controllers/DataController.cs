using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Xml;

[Route("api/[controller]")]
[ApiController]
public class DataController : ControllerBase
{
    // Your WeatherAPI key
    private const string WeatherApiKey = "564f9dd8a1464c58a3981325251802";
    private const string WeatherApiUrl = "http://api.weatherapi.com/v1/current.json"; // Change if necessary

    [HttpPost("save")]
    public async Task<IActionResult> SaveData([FromBody] CityRequest cityRequest)
    {
        // Ensure the model is valid
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid data. Ensure Name and City are provided.");
        }

        // Normalize city to lowercase (you can also use proper title case if preferred)
        string normalizedCity = NormalizeCity(cityRequest.City);

        try
        {
            // Fetch weather data for the city
            var weatherData = await GetWeatherData(normalizedCity);

            // If no weather data is returned, it means the city wasn't found or there was an error
            if (weatherData == null)
            {
                return BadRequest($"City '{normalizedCity}' not found or an error occurred fetching weather data.");
            }

            // Prepare data to save, including name, city, weather, and current date/time
            var dataToSave = new
            {
                Name = cityRequest.Name,
                City = normalizedCity,  // Store the normalized city name
                Weather = weatherData,  // Store as a structured object
                DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            // Convert data to a formatted JSON string using Newtonsoft.Json
            string jsonString = JsonConvert.SerializeObject(dataToSave, Formatting.Indented);

            // Define the file path (you can choose a different location as needed)
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "SavedData.txt");

            // Append the JSON data to the file asynchronously
            await System.IO.File.AppendAllTextAsync(filePath, jsonString + Environment.NewLine + Environment.NewLine + Environment.NewLine);

            // If everything is successful, return a success message
            return Ok("Data saved successfully.");
        }
        catch (Exception ex)
        {
            // Handle any unexpected exceptions
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // Normalize city name to lowercase (can also use TitleCase if preferred)
    private string NormalizeCity(string city)
    {
        if (string.IsNullOrEmpty(city))
        {
            throw new ArgumentException("City name cannot be empty.");
        }

        // Convert city name to proper case (Title Case)
        return city.Trim().ToLower();
    }

    // Fetch weather data for the specific city using the WeatherAPI
    private async Task<object> GetWeatherData(string city)
    {
        using (var client = new HttpClient())
        {
            try
            {
                // Build the request URL
                string requestUrl = $"{WeatherApiUrl}?key={WeatherApiKey}&q={city}&aqi=no";

                // Send a GET request to WeatherAPI
                var response = await client.GetStringAsync(requestUrl);

                // Parse the response JSON
                dynamic weatherResponse = JsonConvert.DeserializeObject(response);

                // Check if the response contains an error (city not found)
                if (weatherResponse.error != null)
                {
                    return null; // Return null if the city is not found
                }

                // Extract relevant weather information from the response
                var currentWeather = weatherResponse.current;

                // Structured format of weather data
                var weatherInfo = new
                {
                    Temperature = currentWeather.temp_c + "°C",
                    Condition = currentWeather.condition.text,
                    Wind = currentWeather.wind_kph + " km/h",
                    Humidity = currentWeather.humidity + "%",
                    FeelsLike = currentWeather.feelslike_c + "°C"
                };

                // Return weather information as a structured object (not a string)
                return weatherInfo;
            }
            catch (Exception ex)
            {
                // Handle error, return a fallback message if the API call fails
                return null; // Return null when an error occurs
            }
        }
    }

    // DTO to bind the incoming data (name and city)
    public class CityRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
    }
}