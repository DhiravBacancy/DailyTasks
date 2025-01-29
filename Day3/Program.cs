using System;
using System.Collections.Generic;

class BoxingUnboxingDemo
{
    static void Main()
    {
        try
        {
            // Creating a List<int> with sample values
            List<int> intList = new List<int> { 10, 20, 30, 40 };

            // Creating a List<object> to store boxed values
            List<object> boxedList = BoxValues(intList);

            // Unboxing values from List<object> back to List<int>
            List<int> unboxedList = UnboxValues<int>(boxedList);

            // Displaying results with differences
            Console.WriteLine("Original List<int>:");
            PrintList(intList);

            Console.WriteLine("\nBoxed List<object> (Memory addresses or boxed representation):");
            PrintBoxedList(boxedList);

            Console.WriteLine("\nUnboxed List<int> (Restored original values):");
            PrintList(unboxedList);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Generic method to box values from List<T> to List<object>
    static List<object> BoxValues<T>(List<T> values) where T : struct
    {
        List<object> boxedList = new List<object>();
        foreach (T value in values)
        {
            boxedList.Add(value); // Boxing
        }
        return boxedList;
    }

    // Generic method to unbox values from List<object> to List<T>
    static List<T> UnboxValues<T>(List<object> values) where T : struct
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
    static void PrintList<T>(List<T> list)
    {
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
    }

    // Method to print boxed List<object> to show difference
    static void PrintBoxedList(List<object> list)
    {
        foreach (var item in list)
        {
            Console.WriteLine($"Boxed value: {item} (Type: {item.GetType()})");
        }
    }
}
