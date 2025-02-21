using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3{
    class BoxUnbox
    {
        // Generic method to box values from List<T> to List<object>
        public static List<object> BoxValues<T>(List<T> values) where T : struct
        {
            List<object> boxedList = new List<object>();
            foreach (T value in values)
            {
                boxedList.Add(value); // Boxing
            }
            return boxedList;
        }

        // Generic method to unbox values from List<object> to List<T>
        public static List<T> UnboxValues<T>(List<object> values) where T : struct
        {
            List<T> unboxedList = new List<T>();
            foreach (object obj in values)
            {
                if (obj is T value)
                {
                    unboxedList.Add(value); // Unboxing
                }
                else
                {
                    throw new InvalidCastException($"Cannot unbox {obj} to {typeof(T)}");
                }
            }
            return unboxedList;
        }

        // Method to print List<T>
        public static void PrintList<T>(List<T> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

        // Method to print boxed List<object> to show difference
        public static void PrintBoxedList(List<object> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine($"Boxed value: {item} (Type: {item.GetType()})");
            }
        }
    }
}
