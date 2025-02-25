using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task6
{
    class Program
    {
        private static DateTime startTime;

        private static void Main(string[] args)
        {
            DateTime startTime = DateTime.Now;
            Thread t1 = new Thread(new ThreadStart(Thread1));
            t1.Name = "Thread1";
            t1.Start();
            t1.Join();
            Thread t2 = new Thread(new ThreadStart(Thread2));
            t2.Name = "Thread2";
            t2.Start();
            t2.Join();
            Thread t3 = new Thread(new ThreadStart(Thread3));
            t3.Name = "Thread3";
            t3.Start();
            t3.Join();
            DateTime endTime = DateTime.Now; // Запоминаем время окончания
            TimeSpan executionTime = endTime - startTime;

            Console.WriteLine($"Время выполнения: {executionTime.TotalSeconds:F2} секунд");
        }
        static void Thread1()
        {
            Thread currentThread = Thread.CurrentThread;
             for (int i = 1; i < 6; i++)
             {
                 Thread.Sleep(500);
                 Console.WriteLine($"Имя потока: {currentThread.Name}");
                 Console.WriteLine($"Номер итерации: " + i);
                 Console.WriteLine($" ");
             }
        }
         static void Thread2()
         {
             Thread currentThread = Thread.CurrentThread;

             for (int i = 1; i < 6; i++)
             {
                 Thread.Sleep(500);
                 Console.WriteLine($"Имя потока: {currentThread.Name}");
                 Console.WriteLine($"Номер итерации: " + i);
                 Console.WriteLine($" ");
             }
         }
         static void Thread3()
         {
             Thread currentThread = Thread.CurrentThread;

             for (int i = 1; i < 6; i++)
             {
                 Thread.Sleep(500);
                 Console.WriteLine($"Имя потока: {currentThread.Name}");
                 Console.WriteLine($"Номер итерации: " + i);
                 Console.WriteLine($" ");
             }
         }
    }

}

