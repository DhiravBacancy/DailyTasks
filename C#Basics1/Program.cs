using System;
using System.Collections.Generic;

namespace DailyTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "Dhirav's Console";
                PowerOfTwoChecker obj = new PowerOfTwoChecker();
                obj.runTask();
            }
            catch (Exception ex) { 
                Console.WriteLine("An error occurred. Please try again.");
            }
            

        }
    }

}