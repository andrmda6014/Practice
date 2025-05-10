using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Sorts
{
    public class Loan
    {
        public Book Book { get; private set; }
        public Reader Reader { get; private set; }
        public DateTime LoanDate { get; private set; }
        public DateTime ReturnDate { get; private set; }

        public Loan(Book book, Reader reader, DateTime loanDate, DateTime returnDate)
        {
            Book = book;
            Reader = reader;
            LoanDate = loanDate;
            ReturnDate = returnDate;
        }
    }
}
