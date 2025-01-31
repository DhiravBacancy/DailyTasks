using System;
using System.Threading;

class Program
{
    private static int sharedVariable = 0;
    private static Mutex mutex = new Mutex();

    static void Increment()
    {
        for (int i = 0; i < 1000; i++)
        {
            mutex.WaitOne(); // Acquire the mutex
            sharedVariable++;
            mutex.ReleaseMutex(); // Release the mutex
        }
    }

    static void Main()
    {
        Thread[] threads = new Thread[10];

        for (int i = 0; i < threads.Length; i++)
        {
            threads[i] = new Thread(Increment);
            threads[i].Start();
        }
              
        foreach (Thread t in threads)
        {
            t.Join(); // Wait for all threads to complete
        }

        Console.WriteLine("Final value of shared variable: " + sharedVariable);
    }
}
