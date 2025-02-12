using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Practice.Program;
using Task1.Sorts;

namespace Sorts
{
        public class BubbleSort : SortingAlgorithm
        {
            public override SortingResult Sort(int[] array)
            {
                int[] sortedArray = (int[])array.Clone();
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();

                int n = sortedArray.Length;
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - i - 1; j++)
                    {
                        if (sortedArray[j] > sortedArray[j + 1])
                        {
                            int temp = sortedArray[j];
                            sortedArray[j] = sortedArray[j + 1];
                            sortedArray[j + 1] = temp;
                        }
                    }
                }

                stopwatch.Stop();
                return new SortingResult(stopwatch.ElapsedMilliseconds, sortedArray);
            }
        }
    }

