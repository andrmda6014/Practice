using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Sorts;


namespace Practice
{
    internal class Program

    {
        // Абстрактный класс для алгоритмов сортировки
        public abstract class SortingAlgorithm
        {
            public abstract SortingResult Sort(int[] array);
        }

        // Класс для сортировки пузырьком
       

        // Структура для хранения результатов сортировки
        public struct SortingResult
        {
            public long ExecutionTime { get; }
            public int[] SortedArray { get; }

            public SortingResult(long executionTime, int[] sortedArray)
            {
                ExecutionTime = executionTime;
                SortedArray = sortedArray;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество элементов");
            int size = int.Parse(Console.ReadLine());

            if (size <= 0)
            {
                Console.WriteLine("Некорректный ввод. Программа завершена.");
                return;
            }

            int[] array = GenerateRandomArray(size);
            SortingAlgorithm[] algorithms = {
            new BubbleSort(),
            new InsertionSort(),
            new QuickSort(),
            new MergeSort()
               };

            foreach (var algorithm in algorithms)
            {
                var result = algorithm.Sort(array);
                Console.WriteLine($"{algorithm.GetType().Name}: Время выполнения = {result.ExecutionTime} мс");
            }
        }

        static int[] GenerateRandomArray(int size)
        {
            Random random = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(0, 1000000); // Генерация случайных чисел от 0 до 9999
            }
            return array;
        }
    }
}