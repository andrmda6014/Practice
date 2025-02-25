using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Task3
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                await Task.WhenAll
                    (Task.Run(() => Task1()), 
                    Task.Run(() => Task2()), 
                    Task.Run(() => Task3()));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
            }
            stopwatch.Stop();

            Console.WriteLine($"Время рассчета асинхронно:  {stopwatch.Elapsed.TotalSeconds} секунд");

            stopwatch.Restart();

            await Task1();
            await Task2();
            await Task3();

            stopwatch.Stop();

            Console.WriteLine($"Время рассчета последовательно:  {stopwatch.Elapsed.TotalSeconds} секунд");
        }
        private static async Task Task1()
        {
            Console.WriteLine("$Имя задачи: Task1");
            await Task.Delay(new Random().Next(1000, 3000));
        }
        private static async Task Task2()
        {
            Console.WriteLine("$Имя задачи: Task2");
            await Task.Delay(new Random().Next(1000, 3000));
        }
        private static async Task Task3()
        {
            try
            {
                await Task.Delay(new Random().Next(1000, 3000)); 
                throw new Exception("Ошибка в задаче 3!"); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение в задаче 3: {ex.Message}");
            }
        }
    }
}
