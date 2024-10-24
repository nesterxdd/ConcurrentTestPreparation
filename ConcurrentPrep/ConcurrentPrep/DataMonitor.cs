using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentPrep
{
    class DataMonitor
    {
        public string mystring { get; private set; }
        object lockObject = new object();

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
            if (counter >= 15)
            {
                isDone = true;
                System.Threading.Monitor.PulseAll(lockObject);
                return counter;
            }
            Monitor.PulseAll(lockObject);
            return counter;
        }

        public void AddA()
        {
            lock (lockObject)
            {
                countA = AddChar('A', countA);
            }
        }

        public void AddB()
        {
            lock (lockObject)
            {
                if (countA < 3)
                {
                    Monitor.Wait(lockObject);
                }
                countB = AddChar('B', countB);
            }
        }

        public void AddC()
        {
            lock (lockObject)
            {
                if (countA < 3)
                {
                    Monitor.Wait(lockObject);
                }
                countC = AddChar('C', countC);
            }
        }
    }
}
