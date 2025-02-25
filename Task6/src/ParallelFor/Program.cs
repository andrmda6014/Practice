using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelFor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConcurrentDictionary<int, long> factorials = new ConcurrentDictionary<int, long>();

            Stopwatch stopwatch = Stopwatch.StartNew();

            Parallel.For(1, 11, i =>
            {
                factorials[i] = Factorial(i);
            });

            stopwatch.Stop();
            Console.WriteLine("Параллельный цикл завершен за: " + stopwatch.Elapsed.TotalSeconds + " секунд");

            foreach (var kvp in factorials)
            {
                Console.WriteLine($"Факториал {kvp.Key} = {kvp.Value}");
            }

            stopwatch.Restart();
            for (int i = 1; i <= 10; i++)
            {
                factorials[i] = Factorial(i);
            }
            stopwatch.Stop();
            Console.WriteLine("Обычный цикл завершен за: " + stopwatch.Elapsed.TotalSeconds + " секунд");
        }
        static long Factorial(int n)
        {
            if (n == 0 || n == 1)
                return 1;
            long result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}
