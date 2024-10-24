using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static object lockObject = new object();
    static string mystring = "";
    static int countA = 0;
    static int countB = 0;
    static int countC = 0;
    static int countVowels = 0;

    static bool isDone = false;
    static void Main(string[] args)
    {


        Thread threadA = new Thread(ThreadA);
        Thread threadB = new Thread(ThreadB);
        Thread threadC = new Thread(ThreadC);

        Thread printThread = new Thread(PrintThread);

        threadA.Start();
        threadB.Start();
        threadC.Start();
        printThread.Start();

        threadA.Join();
        threadB.Join();
        threadC.Join();

        printThread.Join();

        Console.WriteLine("Final string: " + mystring);
        Console.WriteLine($"CountA {countA} countB {countB} countC {countC}");
    }

    public static void ThreadA()
    {

        while (!isDone)
        {
            lock (lockObject)
            {
                mystring += "A";
                countA++;
                countVowels++;
                if (countA >= 15)
                {
                    isDone = true;
                    Monitor.PulseAll(lockObject);
                    return;
                }
                
                Monitor.PulseAll(lockObject);
            }
            Thread.Sleep(100); // Simulate work with a short sleep

        }
    }

    public static void ThreadB()
    {

        while (!isDone)
        {
            lock (lockObject)
            {
                while (countVowels < 3)
                {
                    Monitor.Wait(lockObject);
                }
                mystring += "B";
                countB++;
                if (countB >= 15)
                {
                    isDone = true;
                    Monitor.PulseAll(lockObject);
                    return;
                }

                Monitor.PulseAll(lockObject);
            }
            Thread.Sleep(100); // Simulate work with a short sleep
        }



    }

    public static void ThreadC()
    {

        while (!isDone)
        {
            lock (lockObject)
            {
                while (countVowels <= 3)
                {
                    Monitor.Wait(lockObject);
                }
                mystring += "C";
                countC++;
                if (countC >= 15)
                {
                    isDone = true;
                    Monitor.PulseAll(lockObject);
                    return;
                }
                Monitor.PulseAll(lockObject);

            }
            Thread.Sleep(100); // Simulate work with a short sleep
        }



    }

    public static void PrintThread()
    {
        while (!isDone)
        {
            lock (lockObject)
            {
                Console.WriteLine(mystring);
                Monitor.Wait(lockObject); 
            }
           
        }
    }
}
