using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyTasks
{
    public class Task1
    {

        public void runTask()
        {

            int[] numbers;
            //User Input If you want 
            Console.WriteLine("Enter numbers separated by spaces:");
            numbers = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            //Hard Coded Input
            //numbers = new int[] { 3, 4, 7, 16 }; // Example hardcoded input



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
            if (num <= 0) return false;
            return (num & (num - 1)) == 0;
        }
    }
}






