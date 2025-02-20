using System;
using System.IO;
using OfficeOpenXml;

namespace Task5
{
    public class Program
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
                Console.WriteLine("5. Показать полезные данные");
                Console.WriteLine("6. Экспортировать в Excel");
                Console.WriteLine("7. Выход");
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
                        int n = int.Parse(Console.ReadLine()) - 1; // Вычитаем 1 для корректного индекса
                        Console.Write("Введите M: ");
                        int m = int.Parse(Console.ReadLine());
                        _dataProcessor.ShowData(n, m); // -1 для корректного индекса
                        break;
                    case "5":
                        _dataProcessor.ShowUsefulData(); // Вызов метода для показа полезных данных
                        break;
                    case "6":
                        _dataProcessor.ExportToExcel(); // Вызов метода для экспорта в Excel
                        break;
                    case "7":
                        Console.WriteLine("Выход из программы.");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор, попробуйте снова.");
                        break;
                }
            }
        }
    }
}
