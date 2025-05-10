using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Models;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Library.LoadBooks();
            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Чтение из файла");
                Console.WriteLine("2. Сохранить в файл");
                Console.WriteLine("3. Добавить книгу");
                Console.WriteLine("4. Отредактировать книгу");
                Console.WriteLine("5. Удалить книгу");
                Console.WriteLine("6. Вывести информацию об одной книге");
                Console.WriteLine("7. Вывести информацию о нескольких книгах");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите пункт меню: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Library.LoadBooks();
                        break;
                    case "2":
                        Library.SaveBooks();
                        break;
                    case "3":
                        Library.AddBook();
                        break;
                    case "4":
                        Library.UpdateBook();
                        break;
                    case "5":
                        Library.RemoveBook();
                        break;
                    case "6":
                        Library.DisplayBooks();
                        break;
                    case "7":
                        Library.DisplayBooks();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                        break;
                }
            }
        }
    }
}
