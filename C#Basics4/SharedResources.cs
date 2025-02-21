using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    class SharedResources
    {
         private int sharedVariable = 0;
    private Mutex mutex = new Mutex();

    public void Increment()
    {
        for (int i = 0; i < 1000; i++)
        {
            mutex.WaitOne(); // Acquire the mutex
            sharedVariable++;
            mutex.ReleaseMutex(); // Release the mutex
        }
    }

    public int GetSharedVariable()
    {
        return sharedVariable;
    }
    }
}
