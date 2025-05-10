using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Sorts
{
    public abstract class Person
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        protected Person(string name, DateTime dateOfBirth)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
        }

        public abstract void DisplayInfo();
    }
}
