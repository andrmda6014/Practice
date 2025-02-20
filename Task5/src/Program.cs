using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using Task5.Classes;

namespace Task5
{
    internal class Program
    {
        private static DataProcessor _dataProcessor = new DataProcessor();

        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Прочитать конфигурацию");
                Console.WriteLine("2. Вывести конфигурацию на экран");
                Console.WriteLine("3. Прочитать файл с данными");
                Console.WriteLine("4. Вывести строки данных (с N до M)");
                Console.WriteLine("5. Выход");
                Console.Write("Выберите опцию: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        _dataProcessor.LoadConfiguration();
                        break;
                    case "2":
                        _dataProcessor.ShowConfiguration();
                        break;
                    case "3":
                        _dataProcessor.LoadData();
                        break;
                    case "4":
                        Console.Write("Введите N: ");
                        int n = int.Parse(Console.ReadLine());
                        Console.Write("Введите M: ");
                        int m = int.Parse(Console.ReadLine());
                        _dataProcessor.ShowData(n - 1, m); // -1 для корректного индекса
                        break;
                    case "5":
                        return; // Выход из программы
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                        break;
                }
            }
        }
    }
}