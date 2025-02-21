# **PowerOfTwoChecker**  
A simple C# console application that checks whether the given numbers are powers of two. It also handles invalid inputs and ensures a smooth user experience.  

## **📌 Features**  
✅ Accepts space-separated numbers from user input.  
✅ Filters and displays numbers that are NOT powers of two.  
✅ Skips nvalid (non-numeric) inputs gracefully.  
✅ Provides clear output messages.  

---

## **🛠 How to Run**  

### **1️⃣ Prerequisites**  
- .NET 8 SDK installed  
- Any C# IDE (Visual Studio, VS Code, JetBrains Rider)  

### **2️⃣ Steps to Execute**  
1. Clone or download this repository.  
2. Open the project in your IDE.  
3. Build and run the project.  
4. Enter space-separated numbers when prompted.  
5. View the results!  

---

## **📌 Example Outputs**  

### ✅ **Case 1: All Numbers Are Powers of Two**  
**User Input:**  
```
Enter numbers separated by spaces:  
2 4 8 16 32  
```
**Output:**  
```
All numbers are powers of two.
```
📷 *[Attach Screenshot Here]*  

---

### ❌ **Case 2: Invalid Characters in Input**  
**User Input:**  
```
Enter numbers separated by spaces:  
3 a 4 7 b 16  
```
**Output:**  
```
Invalid input skipped: a  
Invalid input skipped: b  
Numbers that are not powers of two:  
3, 7  
```
📷 *[Attach Screenshot Here]*  

---

## **📌 Code Structure**  
- **`PowerOfTwoChecker`** (Main class)  
  - `runTask()` → Handles user input and processing.  
  - `GetNotPowersOfTwo()` → Filters numbers **not** being powers of two.  
  - `IsPowerOfTwo()` → Checks if a number is a power of two.  

---

## **📌 Future Improvements**  
🚀 Add support for batch file input.  
🚀 Implement a GUI for better visualization.  
🚀 Include an option for continuous input mode.  

---

## **📌 Author**  
👨‍💻 **Dhirav Agrawal**  

---

### 📌 **Add Screenshots**  
Once you have screenshots of your output, replace the placeholders:  

```md
![Valid Input Output](path/to/valid_output.png)  
![Invalid Input Output](path/to/invalid_output.png)
```
---

Let me know if you need any changes! 🚀🔥
