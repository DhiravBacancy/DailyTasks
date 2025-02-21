using System;
using System.Collections.Generic;

namespace Task3
{
    public class Program
    {
       public static void Main(string[] args)
       {
            try
            {
                // Creating a List<int> with sample values
                List<int> intList = new List<int> { 10, 20, 30, 40 };

                // Creating a List<object> to store boxed values using BoxUnbox class
                List<object> boxedList = BoxUnbox.BoxValues(intList);

                // Unboxing values from List<object> back to List<int>
                List<int> unboxedList = BoxUnbox.UnboxValues<int>(boxedList);

                // Displaying results with differences
                Console.WriteLine("Original List<int>:");
                BoxUnbox.PrintList(intList);

                Console.WriteLine("\nBoxed List<object> (Memory addresses or boxed representation):");
                BoxUnbox.PrintBoxedList(boxedList);

                Console.WriteLine("\nUnboxed List<int> (Restored original values):");
                BoxUnbox.PrintList(unboxedList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
    
       }

    }

}