using System;
using System.Threading;

namespace CS.StateMachine.Demo
{
    internal class Program
    {
        static StateMachine sm = new StateMachine();
        static void Main(string[] args)
        {
            sm.Start(14);

            sm.OnDone += (sender, eventArgs) =>
            {
                try
                {
                    Console.WriteLine($"Work is done, the result is {sm.Result}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Catching the exception in Main! {e.Message}");
                }
               
            };

            Console.WriteLine("Main ends!");
        }
    }


    class StateMachine
    {
        private int _result = default(int);
        private Exception _exception = default(Exception);

        public bool IsFinished { get; set; }
        
        public bool IsFaulted { get; set; }
        public Exception Error => _exception;

        public event EventHandler OnDone; 

        public int Result => _exception != null ? throw _exception : _result;



        public void Start(int n)
        {

            Thread t = new Thread(() =>
            {
                try
                {
                    Thread.Sleep(1200);
                    
                    _result = n * n;

                    if (_result == 196)
                    {
                        throw new Exception("Purposely!");
                    }
                    IsFinished = true;
                    OnDone?.Invoke(this, null);
                }
                catch (Exception e)
                {
                    IsFaulted = true;
                    _exception = e;
                    OnDone?.Invoke(this, null);
                }
            });

            t.Start();
        }
    }


    
}
