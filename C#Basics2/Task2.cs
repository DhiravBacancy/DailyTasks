using System;
using System.Text.RegularExpressions;

namespace Task2
{
    public class ValidateEmail
    {
        public void check()
        {
            string emailPattern = @"^(?!.*\.\.)[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            string userInput;

            while (true)
            {
                Console.Write("Enter a valid email address: ");
                userInput = Console.ReadLine()?.Trim(); // Trim spaces and handle null input

                // Check if input is empty
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Email cannot be empty. Please try again.");
                    continue;
                }

                // Validate the email using regex
                if (Regex.IsMatch(userInput, emailPattern, RegexOptions.IgnoreCase))
                {
                    Console.WriteLine(" Valid email address entered!");
                    break; // Exit loop if valid
                }
                else
                {
                    Console.WriteLine("Invalid email address. Please try again.");
                }
            }
        }
    }
}
