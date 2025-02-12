using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Interface;

namespace Task2.Sorts
{
    public class Book : Item, ILendable
    {
        public Author Author { get; set; }
        public bool Available { get; private set; }

        public Book(string title, Author author, int publicationYear) : base(title, publicationYear)
        {
            Author = author;
            Available = true;
        }

        public void Lend()
        {
            Available = false;
        }

        public void Return()
        {
            Available = true;
        }
    }
}

