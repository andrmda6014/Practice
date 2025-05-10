using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task2.Sorts
{
    public class Author : Person
    {
        public string Biography { get; set; }

        public Author(string name, DateTime dateOfBirth, string biography) : base(name, dateOfBirth)
        {
            Biography = biography;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Автор: {Name}, Дата рождения: {DateOfBirth.ToShortDateString()}, Биография: {Biography}");
        }
    }
}
