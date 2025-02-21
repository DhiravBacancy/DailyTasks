using System;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "Dhirav's Console";
                ValidateEmail obj = new ValidateEmail();

                obj.check();

                Console.ReadLine();
            } catch (Exception ex)
            {
                Console.WriteLine("An error occurred. Please try again.");
            }
        }
    }
}
