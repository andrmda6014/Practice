using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSafety
{
    internal class Program
    {
        private static object obj = new object();
        static int counter = 0;
        static void Main(string[] args)
        {
            Thread t1 = new Thread(new ThreadStart(Counter));
            Thread t2 = new Thread(new ThreadStart(Counter));

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.WriteLine($"Результат: " + counter);
        }

        static void Counter()
        {
            for (int i = 0; i < 100000; i++)
            {
                {
                    lock(obj)
                    {
                        counter++;
                    }
                }
            }
        }
    }
}  