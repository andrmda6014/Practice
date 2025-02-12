using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practice;
using static Practice.Program;

namespace Sorts
{
    public abstract class SortingAlgorithm
    {
        public abstract SortingResult Sort(int[] array);
        public abstract override SortingResult Sort(int[] array);
    }
}
