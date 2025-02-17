using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Sorts
{
    public class Library
    {
        private List<Book> books;
        private List<Reader> readers;
        private List<Loan> loans;

        public Library()
        {
            books = new List<Book>();
            readers = new List<Reader>();
            loans = new List<Loan>();
        }

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public void AddReader(Reader reader)
        {
            readers.Add(reader);
        }

        public void LendBook(Reader reader, Book book)
        {
            if (book.Available)
            {
                reader.BorrowBook(book);
                Loan loan = new Loan(book, reader, DateTime.Now, DateTime.Now.AddDays(14)); 
                loans.Add(loan);
            }
        }

        public void ReturnBook(Reader reader, Book book)
        {
            if (loans.Exists(loan => loan.Book == book && loan.Reader == reader))
            {
                reader.ReturnBook(book);
                loans.RemoveAll(loan => loan.Book == book && loan.Reader == reader);
            }
        }
    }
}
