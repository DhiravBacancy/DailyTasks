# **Thread Synchronization using Mutex in C#**  

## **Overview**  
This program demonstrates **thread synchronization** using a **Mutex** in C#. It ensures that multiple threads safely update a shared variable without race conditions.

## **Project Structure**  
- **`SharedResource` Class**  
  - Contains the **shared variable**.  
  - Provides a method (`Increment`) to safely update the variable using a **mutex**.  
  - Includes `GetSharedVariable()` to retrieve the final value.  
- **`Program` Class**  
  - Creates **10 threads**, each calling `Increment()`.  
  - Ensures proper thread management using `Join()`.  
  - Displays the final value of the shared variable.  

## **How It Works**  
1. Each thread calls `Increment()`, which:  
   - Acquires the **mutex** before modifying the shared variable.  
   - Releases the **mutex** after modification.  
2. **Mutex ensures only one thread modifies the shared variable at a time.**  
3. The **main thread waits** for all threads to complete.  
4. The final shared variable value is printed.  

## **Expected Output**  
![](./1.png)