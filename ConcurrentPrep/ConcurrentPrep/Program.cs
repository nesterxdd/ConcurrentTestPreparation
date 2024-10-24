using ConcurrentPrep;
using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    

    static DataMonitor dataMonitor = new DataMonitor();
    static void Main(string[] args)
    {


        Thread threadA = new Thread(ThreadA);
        Thread threadB = new Thread(ThreadB);
        Thread threadC = new Thread(ThreadC);
        


        threadA.Start();
        threadB.Start();
        threadC.Start();

        while(dataMonitor.isDone == false)
        {
            Console.WriteLine(dataMonitor.mystring);
        }

        threadA.Join();
        threadB.Join();
        threadC.Join();


        Console.WriteLine("Final string: " + dataMonitor.mystring);
        Console.WriteLine($"CountA {dataMonitor.countA} countB {dataMonitor.countB} countC {dataMonitor.countC}");
    }

    public static void ThreadA()
    {
        while (!dataMonitor.isDone)
        {
            dataMonitor.AddA();
            Thread.Sleep(100); // Simulate work with a short sleep
        }
    }

    public static void ThreadB()
    {

        while (!dataMonitor.isDone)
        {

            dataMonitor.AddB();
            Thread.Sleep(100); // Simulate work with a short sleep
        }



    }

    public static void ThreadC()
    {

        while (!dataMonitor.isDone)
        {
            dataMonitor.AddC();
            Thread.Sleep(100); // Simulate work with a short sleep
        }



    }

}
