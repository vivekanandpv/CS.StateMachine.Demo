using System;
using System.Threading;

namespace CS.StateMachine.Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread t1 = new Thread(() =>
            {
                PrintMessage("XYZ");
            }) {Name = "Thread 1"};

            Thread t2 = new Thread(() =>
            {
                PrintMessage("ABC");
            }) {Name = "Thread 2"};

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();
        }

        static void PrintMessage(string message)
        {
            string lv = "Good morning";
            Console.WriteLine($"Thread: {Thread.CurrentThread.Name} says: {lv}; message is: {message}");
        }
    }


    
}
