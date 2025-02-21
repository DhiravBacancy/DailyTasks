# Enhanced Web API with Dependency Injection, File Handling & Weather Data  

## Project Overview  
This .NET Web API project enhances file handling and weather data retrieval while demonstrating Dependency Injection (DI) and service lifetimes (Scoped, Singleton, Transient).  

---

## Primary Tasks  

### 1. **File Handling with Dependency Injection**  
- **Implemented DI for file saving** by creating a `FileService` and injecting it into the controller.  
- The controller no longer directly interacts with file operations; instead, it calls the injected service.  

### 2. **New Controller for Reading Logged Requests**  
- A new controller is added to **read data from the log file** and return it as JSON.  
- This controller also **uses the `FileService`** for reading file content.  

### 3. **Weather Data API (Restricted to Ahmedabad)**  
- Added a new API to fetch weather data.  
- **Restricts requests to Ahmedabad only** to ensure location-specific responses.  

---

## API Endpoints  

### **File Handling**  
1. `POST /api/json/save` – Saves JSON data to a file using DI.  
2. `GET /api/json/read` – Reads stored JSON data from the file.  

### **Weather Data**  
3. `GET /api/weather/ahmedabad` – Retrieves weather data (restricted to Ahmedabad).  

### **GuidService Demonstration**  
4. `GET /api/guid` – Returns a GUID, demonstrating Scoped, Singleton, and Transient behavior.  

---
### Output

### Saving the data for city entred inside text file
![](./Outputs/1.png)

### Output
![](./Outputs/2.png)

---

### Reading data from text file
![](./Outputs/3.png)

---

### Fetching weather details of rajkot which will be blocked by middleware
![](./Outputs/4.png)

### Fetching weather details of ahmedabad which only will be allowed by middleware
![](./Outputs/5.png)

---

### Retrieving GUIDs from Singleton, Transient and Scoped Services by dependency injection(DI)
![](./Outputs/6.png)
