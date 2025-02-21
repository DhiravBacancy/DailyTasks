using System;
using System.Threading;

namespace Task4
{
    class Program
    {
        static void Main()
        {
            try {
                SharedResources resource = new SharedResources();
                Thread[] threads = new Thread[10];

                for (int i = 0; i < threads.Length; i++)
                {
                    threads[i] = new Thread(resource.Increment);
                    threads[i].Start();
                }

                foreach (Thread t in threads)
                {
                    t.Join(); // Wait for all threads to complete
                }

                Console.WriteLine("Final value of shared variable: " + resource.GetSharedVariable());
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            
        }
    }

}
