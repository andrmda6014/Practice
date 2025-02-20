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
            ShowMenu(); // Запуск меню
        }

        public static void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\nВыберите действие:");

                Console.WriteLine("1. Загрузить конфигурацию");
                Console.WriteLine(_dataProcessor.IsConfigurationLoaded ? "2. Показать конфигурацию" : "2. Показать конфигурацию (недоступно)");
                Console.WriteLine(_dataProcessor.IsDataLoaded ? "3. Загрузить данные" : "3. Загрузить данные (недоступно)");
                Console.WriteLine(_dataProcessor.IsDataLoaded ? "4. Показать строки данных (введите N и M)" : "4. Показать строки данных (недоступно)");
                Console.WriteLine(_dataProcessor.IsDataLoaded ? "5. Показать полезные данные" : "5. Показать полезные данные (недоступно)");
                Console.WriteLine("6. Экспортировать в Excel");
                Console.WriteLine("7. Выход");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        _dataProcessor.LoadConfiguration();
                        break;
                    case "2":
                        if (!_dataProcessor.IsConfigurationLoaded)
                        {
                            Console.WriteLine("Конфигурация еще не загружена.");
                        }
                        else
                        {
                            _dataProcessor.ShowConfiguration();
                        }
                        break;
                    case "3":
                        _dataProcessor.LoadData(); // Здесь вызывается метод загрузки данных
                        break;
                    case "4":
                        if (!_dataProcessor.IsDataLoaded)
                        {
                            Console.WriteLine("Данные еще не загружены.");
                        }
                        else
                        {
                            Console.WriteLine("Введите N:");
                            int n = int.Parse(Console.ReadLine());
                            Console.WriteLine("Введите M:");
                            int m = int.Parse(Console.ReadLine());
                            _dataProcessor.ShowData(n - 1, m);
                        }
                        break;
                    case "5":
                        if (!_dataProcessor.IsDataLoaded)
                        {
                            Console.WriteLine("Данные еще не загружены.");
                        }
                        else
                        {
                            _dataProcessor.ShowUsefulData();
                        }
                        break;
                    case "6":
                        _dataProcessor.ExportToExcel();
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
