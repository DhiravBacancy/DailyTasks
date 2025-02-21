using System;
using System.Collections.Generic;

namespace DailyTasks
{
    public class PowerOfTwoChecker
    {
        public void runTask()
        {
            Console.WriteLine("Enter numbers separated by spaces:");
            string[] input = Console.ReadLine().Split(' ');

            List<int> numbersList = new List<int>();

            foreach (string item in input)
            {
                if (int.TryParse(item, out int number))
                {
                    numbersList.Add(number);
                }
                else
                {
                    Console.WriteLine($"Invalid input skipped: {item}");
                }
            }

            int[] numbers = numbersList.ToArray();

            List<int> notPowersOfTwo = GetNotPowersOfTwo(numbers);

            if (notPowersOfTwo.Count == 0)
            {
                Console.WriteLine("All numbers are powers of two.");
            }
            else
            {
                Console.WriteLine("Numbers that are not powers of two:");
                Console.WriteLine(string.Join(", ", notPowersOfTwo));
            }
        }

        static List<int> GetNotPowersOfTwo(int[] numbers)
        {
            List<int> notPowersOfTwo = new List<int>();

            foreach (int num in numbers)
            {
                if (!IsPowerOfTwo(num))
                    notPowersOfTwo.Add(num);
            }

            return notPowersOfTwo;
        }

        static bool IsPowerOfTwo(int num)
        {
            return num > 0 && (num & (num - 1)) == 0;
        }
    }
}
