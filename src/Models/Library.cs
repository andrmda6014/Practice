using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Task3.Models
{
    public static class Library
    {
        private static List<Book> books = new List<Book>();
        private const string filePath = "books.json";

        public static void LoadBooks()
        {
            if (File.Exists(filePath))
            {
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var json = reader.ReadToEnd();
                        books = JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
                    }
                }
            }
            else
            {
                Console.WriteLine("Файл не обнаружен.");
            }
        }

        public static void SaveBooks()
        {
            var json = JsonSerializer.Serialize(books);
            // Пересоздаем файл при записи
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                }
            }
        }

        public static void AddBook()
        {
            Console.Write("Введите название книги: ");
            string title = Console.ReadLine();
            Console.Write("Введите автора книги: ");
            string author = Console.ReadLine();
            Console.Write("Введите ISBN книги: ");
            string isbn = Console.ReadLine();
            Console.Write("Введите год публикации: ");
            int publicationYear;
            while (!int.TryParse(Console.ReadLine(), out publicationYear))
            {
                Console.Write("Некорректный ввод. Пожалуйста, введите год публикации (число): ");
            }

            books.Add(new Book(title, author, isbn, publicationYear));
            Console.WriteLine("Книга добавлена.");
        }

        public static void RemoveBook()
        {
            Console.WriteLine("Список книг:");
            DisplayBooks();

            Console.Write("Введите ISBN книги для удаления: ");
            string isbn = Console.ReadLine();

            var bookToRemove = books.FirstOrDefault(b => b.ISBN == isbn);
            if (bookToRemove != null)
            {
                books.Remove(bookToRemove);
                Console.WriteLine("Книга удалена.");
            }
            else
            {
                Console.WriteLine("Книга не найдена.");
            }
        }

        public static void UpdateBook()
        {
            Console.WriteLine("Список книг:");
            DisplayBooks();

            Console.Write("Введите ISBN книги для обновления: ");
            string isbn = Console.ReadLine();

            var bookToUpdate = books.FirstOrDefault(b => b.ISBN == isbn);
            if (bookToUpdate != null)
            {
                Console.Write("Введите новое название (или оставьте пустым для пропуска): ");
                string newTitle = Console.ReadLine();
                if (!string.IsNullOrEmpty(newTitle)) bookToUpdate.Title = newTitle;

                Console.Write("Введите нового автора (или оставьте пустым для пропуска): ");
                string newAuthor = Console.ReadLine();
                if (!string.IsNullOrEmpty(newAuthor)) bookToUpdate.Author = newAuthor;

                Console.Write("Введите новый год публикации (или оставьте пустым для пропуска): ");
                string newYearInput = Console.ReadLine();
                if (int.TryParse(newYearInput, out int newYear)) bookToUpdate.PublicationYear = newYear;

                Console.WriteLine("Книга обновлена.");
            }
            else
            {
                Console.WriteLine("Книга не найдена.");
            }
        }

        public static void DisplayBooks()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("Нет книг в библиотеке.");
                return;
            }

            foreach (var book in books)
            {
                Console.WriteLine(book.ToString());
            }
        }

        public static void DisplayBookDetails()
        {
            Console.Write("Введите ISBN книги для отображения: ");
            string isbn = Console.ReadLine();

            var book = books.FirstOrDefault(b => b.ISBN == isbn);
            if (book != null)
            {
                Console.WriteLine(book.ToString());
            }
            else
            {
                Console.WriteLine("Книга не найдена.");
            }
        }
    }
}

