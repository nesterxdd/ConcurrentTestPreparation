using System;
using System.Threading;

namespace ConcurrentPrep
{
    class DataMonitor
    {
        public string mystring { get; private set; }
        private object lockObject = new object();

        public bool isDone { get; private set; } = false;
        public int countA { get; private set; } = 0;
        public int countB { get; private set; } = 0;
        public int countC { get; private set; } = 0;

        public DataMonitor()
        {
            mystring = "*";
        }

        private int AddChar(char character, int counter)
        {
            mystring += character;
            counter++;

            // Check if the total character count has reached 15
            if (counter >= 15)
            {
                isDone = true;
                Monitor.PulseAll(lockObject); // Notify all threads to terminate
            }

            return counter;
        }

        public void AddA()
        {
            lock (lockObject)
            {
                countA = AddChar('A', countA);

                Monitor.PulseAll(lockObject); // Notify if finished after adding

            }
        }

        public void AddB()
        {
            lock (lockObject) // Lock before waiting
            {
                while (countA < 3 && !isDone)
                {
                    Monitor.Wait(lockObject);
                }


                countB = AddChar('B', countB);

            }
        }

        public void AddC()
        {
            lock (lockObject) // Lock before waiting
            {
                while (countA < 3 && !isDone)
                {
                    Monitor.Wait(lockObject);
                }


                countC = AddChar('C', countC);

            }
        }
    }
}
