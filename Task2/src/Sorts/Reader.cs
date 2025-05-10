using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task2.Sorts
{
    public class Reader : Person
    {
        public List<Book> BorrowedBooks { get; private set; }

        public Reader(string name, DateTime dateOfBirth) : base(name, dateOfBirth)
        {
            BorrowedBooks = new List<Book>();
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Читатель: {Name}, День рождения: {DateOfBirth.ToShortDateString()}, Взятых книг: {BorrowedBooks.Count}");
        }

        public void BorrowBook(Book book)
        {
            if (book.Available)
            {
                BorrowedBooks.Add(book);
                book.Lend();
            }
            else
            {
                Console.WriteLine($"Книга '{book.Title}' недоступна.");
            }
        }

        public void ReturnBook(Book book)
        {
            if (BorrowedBooks.Contains(book))
            {
                BorrowedBooks.Remove(book);
                book.Return();
            }
            else
            {
                Console.WriteLine($"Книга '{book.Title}' не была взята {Name}.");
            }
        }
    }
}
