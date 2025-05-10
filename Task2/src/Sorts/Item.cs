using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Sorts
{
    public abstract class Item
    {
        public string Title { get; set; }
        public int year { get; set; }

        protected Item(string title, int publicationYear)
        {
            year = publicationYear;
        }
    }
}
