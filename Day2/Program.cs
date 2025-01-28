using System;

namespace DailyTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Dhirav's Console";
            Task2 task2 = new Task2();

            task2.ValidateEmail();

            Console.ReadLine();
        }
    }
}
