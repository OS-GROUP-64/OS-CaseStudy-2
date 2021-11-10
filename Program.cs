using System;
using System.Threading;

namespace OS_Problem_02
{
    class Thread_safe_buffer
    {
        static int[] TSBuffer = new int[10];
        static int Front = 0;
        static int Back = 0;
        static int Count = 0;

        private static Object _Lock = new Object();

        static void EnQueue(int eq)
        {
            TSBuffer[Back] = eq;
            Back++;
            Back %= 10;
            Count += 1;
        }

        static int DeQueue()
        {
            int x = 0;
            x = TSBuffer[Front];
            Front++;
            Front %= 10;
            Count -= 1;
            return x;
        }

        static void th01(object t)
        {
            int i;

            for (i = 1; i < 51; i++)
            {
                lock (_Lock)
                {

                    while (Count >= 10) // more than or equal to 10
                    {
                        Console.WriteLine("\n-- EnQueue Waiting thread:{0} --\n", t);
                        Monitor.Wait(_Lock);
                    }

                    EnQueue(i);
                    //Console.WriteLine("** EnQueue i={0}, thread:{1} **", i, t);
                    Thread.Sleep(5);
                    if (Count >= 1)
                    { // if has something, wake up DeQueue Thread
                        Monitor.Pulse(_Lock);
                    }

                }

            }

        }

        static void th011(object t)
        {
            int i;

            for (i = 100; i < 151; i++)
            {
                lock (_Lock)
                {

                    while (Count >= 10) // more than or equal to 10
                    {
                        Console.WriteLine("\n-- EnQueue Waiting thread:{0} --\n", t);
                        Monitor.Wait(_Lock);
                    }

                    EnQueue(i);
                    //Console.WriteLine("** EnQueue i={0}, thread:{1} **", i, t);
                    Thread.Sleep(5);
                    if (Count >= 1)
                    { // if has something, wake up DeQueue Thread
                        Monitor.Pulse(_Lock);
                    }

                }
            }

        }


        static void th02(object t)
        {
            int i;
            int j;


            for (i = 0; i < 60; i++)
            {
                lock (_Lock)
                {
                    while (Count <= 0) // equal or less than 0 
                    {
                        Console.WriteLine("\n-- Nothing to DeQueue thread:{0} --\n", t);
                        Monitor.Wait(_Lock);
                    }

                    j = DeQueue();
                    Console.WriteLine("## DeQueue j={0}, thread:{1} ##", j, t);
                    Thread.Sleep(100);
                    if (Count <= 9)
                    { // if not Full, wake up EnQueue Thread
                        Monitor.Pulse(_Lock);
                    }

                }
            }
        }

        // EnQueue is **
        // DeQueue is ##
        // Waiting is --

        static void Main(string[] args)
        {
            Thread t1 = new Thread(th01);
            Thread t11 = new Thread(th011);
            Thread t2 = new Thread(th02);
            Thread t21 = new Thread(th02);
            Thread t22 = new Thread(th02);

            t1.Start(1);
            t11.Start(2);
            t2.Start(3);
            t21.Start(4);
            t22.Start(5);
        }
    }
}
