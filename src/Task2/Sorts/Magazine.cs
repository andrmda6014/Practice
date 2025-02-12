using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Interface;

namespace Task2.Sorts
{
    public class Magazine : Item, ILendable
    {
        public bool IsAvailable { get; private set; }

        public Magazine(string title, int publicationYear) : base(title, publicationYear)
        {
            IsAvailable = true;
        }

        public void Lend()
        {
            IsAvailable = false;
        }

        public void Return()
        {
            IsAvailable = true;
        }
    }
}
