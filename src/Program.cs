using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4.Class;

namespace Task4
{
    public class Program
    {
        private static List<Worker> workers = new List<Worker>();

        public static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Добавить работника");
                Console.WriteLine("2. Редактировать работника");
                Console.WriteLine("3. Удалить работника");
                Console.WriteLine("4. Вывести на экран");
                Console.WriteLine("5. Сохранить в файл");
                Console.WriteLine("6. Чтение из файла");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите пункт меню: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddWorker();
                        break;
                    case "2":
                        EditWorker();
                        break;
                    case "3":
                        RemoveWorker();
                        break;
                    case "4":
                        DisplayWorkers();
                        break;
                    case "5":
                        SaveToFile();
                        break;
                    case "6":
                        LoadFromFile();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        private static void AddWorker()
        {
            try
            {
                Console.Write("Введите имя: ");
                string name = Console.ReadLine();
                Console.Write("Введите возраст: ");
                int age = int.Parse(Console.ReadLine());
                if (age < 0) throw new ArgumentException("Возраст не может быть отрицательным.");
                Console.Write("Введите дату трудоустройства (Y-M-D): ");
                DateTime dateOfEmployment = DateTime.Parse(Console.ReadLine());
                Console.Write("Введите класс: ");
                string grade = Console.ReadLine();

                workers.Add(new Worker(name, age, dateOfEmployment, grade));
                Console.WriteLine("Работник добавлен.");
            }
            catch (FormatException ex)
            {
                LogError(ex.Message);
                Console.WriteLine("Ошибка формата ввода.");
            }
            catch (ArgumentException ex)
            {
                LogError(ex.Message);
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                Console.WriteLine("Произошла ошибка.");
            }
        }

        private static void EditWorker()
        {
            try
            {
                Console.Write("Введите индекс работника для редактирования: ");
                int index = int.Parse(Console.ReadLine());
                if (index < 0 || index >= workers.Count) throw new ArgumentOutOfRangeException("Индекс вне диапазона.");

                Console.Write("Введите новое имя: ");
                string name = Console.ReadLine();
                Console.Write("Введите новый возраст: ");
                int age = int.Parse(Console.ReadLine());
                if (age < 0) throw new ArgumentException("Возраст не может быть отрицательным.");
                Console.Write("Введите новую дату трудоустройства (yyyy-mm-dd): ");
                DateTime dateOfEmployment = DateTime.Parse(Console.ReadLine());
                Console.Write("Введите новую степень: ");
                string grade = Console.ReadLine();

                workers[index] = new Worker(name, age, dateOfEmployment, grade);
                Console.WriteLine("Работник обновлен.");
            }
            catch (FormatException ex)
            {
                LogError(ex.Message);
                Console.WriteLine("Ошибка формата ввода.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                LogError(ex.Message);
                Console.WriteLine("Индекс вне диапазона.");
            }
            catch (ArgumentException ex)
            {
                LogError(ex.Message);
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                Console.WriteLine("Произошла ошибка.");
            }
        }

        private static void RemoveWorker()
        {
            try
            {
                Console.Write("Введите индекс работника для удаления: ");
                int index = int.Parse(Console.ReadLine());
                if (index < 0 || index >= workers.Count) throw new ArgumentOutOfRangeException("Индекс вне диапазона.");

                workers.RemoveAt(index);
                Console.WriteLine("Работник удален.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                LogError(ex.Message);
                Console.WriteLine("Индекс вне диапазона.");
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                Console.WriteLine("Произошла ошибка при удалении работника.");
            }
        }


        private static void DisplayWorkers()
        {
            foreach (var worker in workers)
            {
                Console.WriteLine(worker);
            }
        }

        private static void SaveToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("workers.csv"))
                {
                    writer.WriteLine("Name;Age;DateOfEmployment;Grade");
                    foreach (var worker in workers)
                    {
                        writer.WriteLine(worker);
                    }
                }
                Console.WriteLine("Данные сохранены в файл.");
            }
            catch (FileNotFoundException ex)
            {
                LogError(ex.Message);
                Console.WriteLine("Файл не найден.");
            }
            catch (UnauthorizedAccessException ex)
            {
                LogError(ex.Message);
                Console.WriteLine("Доступ к файлу запрещен.");
            }
            catch (IOException ex)
            {
                LogError(ex.Message);
                Console.WriteLine("Ошибка ввода-вывода.");
            }
        }

        private static void LoadFromFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader("workers.csv"))
                {
                    string header = reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');
                        if (values.Length == 4)
                        {
                            string name = values[0];
                            int age = int.Parse(values[1]);
                            DateTime dateOfEmployment = DateTime.Parse(values[2]);
                            string grade = values[3];

                            workers.Add(new Worker(name, age, dateOfEmployment, grade));
                        }
                    }
                }
                Console.WriteLine("Данные загружены из файла.");
            }
            catch (FileNotFoundException ex)
            {
                LogError(ex.Message);
                Console.WriteLine("Файл не найден.");
            }
            catch (UnauthorizedAccessException ex)
            {
                LogError(ex.Message);
                Console.WriteLine("Доступ к файлу запрещен.");
            }
            catch (IOException ex)
            {
                LogError(ex.Message);
                Console.WriteLine("Ошибка ввода-вывода.");
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                Console.WriteLine("Произошла ошибка при загрузке данных.");
            }
        }

        private static void LogError(string message)
        {
            using (StreamWriter writer = new StreamWriter("error_log.txt", true))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }
    }
}
