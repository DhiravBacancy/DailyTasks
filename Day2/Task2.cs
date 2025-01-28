using System;
using System.Text.RegularExpressions;

namespace DailyTasks
{
    public class Task2
    {
        public void ValidateEmail()
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; // Basic regex pattern for email validation
            string userInput;

            while (true)
            {
                Console.Write("Enter a valid email address: ");
                userInput = Console.ReadLine();

                // Validate the email using regex
                if (Regex.IsMatch(userInput, emailPattern))
                {
                    Console.WriteLine("Valid email address entered!");
                    break; // Exit the loop if valid
                }
                else
                {
                    Console.WriteLine("Invalid email address. Please try again.");
                }
            }
           
        }
    }
}
