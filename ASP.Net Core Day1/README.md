# Web API Project with JSON Data Handling and Weather API Integration  

## Project Overview  
This project is a .NET Web API that:  
1. Accepts JSON data, converts it to a string, saves it to a text file, and returns success or failure.  
2. Integrates with weather APIs (`Weather.NET`, `OpenWeather`) to fetch weather data.  

---

## API Endpoints  

### 1. Save JSON Data to File  
- **Endpoint:** `POST /api/json/save`  
- **Request Body:** JSON Object  
- **Response:** `true` (success) or `false` (error)  

### 2. Fetch Weather Data  
- **Endpoint:** `GET /api/weather/{city}`  
- **Response:** Weather details of the requested city  

---
### Output

### When Correct City Entered 
![](./output/1.png)

### Output
![](./output/2.png)

### When Incorrect City Entered 
![](./output/3.png)

### Output
No data will be added inside text file
