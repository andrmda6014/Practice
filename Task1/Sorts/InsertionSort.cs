using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Practice.Program;

namespace Sorts
{
        public class InsertionSort : SortingAlgorithm
        {
            public override SortingResult Sort(int[] array)
            {
                int[] sortedArray = (int[])array.Clone();
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();

                int n = sortedArray.Length;
                for (int i = 1; i < n; i++)
                {
                    int key = sortedArray[i];
                    int j = i - 1;

                    while (j >= 0 && sortedArray[j] > key)
                    {
                        sortedArray[j + 1] = sortedArray[j];
                        j--;
                    }
                    sortedArray[j + 1] = key;
                }

                stopwatch.Stop();
                return new SortingResult(stopwatch.ElapsedMilliseconds, sortedArray);
            }
        }
    }

