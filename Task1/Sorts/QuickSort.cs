using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Practice.Program;

namespace Sorts
{
        public class QuickSort : SortingAlgorithm
        {
            public override SortingResult Sort(int[] array)
            {
                int[] sortedArray = (int[])array.Clone();
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();

                QuickSortAlgorithm(sortedArray, 0, sortedArray.Length - 1);

                stopwatch.Stop();
                return new SortingResult(stopwatch.ElapsedMilliseconds, sortedArray);
            }

            private void QuickSortAlgorithm(int[] array, int low, int high)
            {
                if (low < high)
                {
                    int pi = Partition(array, low, high);
                    QuickSortAlgorithm(array, low, pi - 1);
                    QuickSortAlgorithm(array, pi + 1, high);
                }
            }

            private int Partition(int[] array, int low, int high)
            {
                int pivot = array[high];
                int i = (low - 1);
                for (int j = low; j < high; j++)
                {
                    if (array[j] < pivot)
                    {
                        i++;
                        int temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
                int temp1 = array[i + 1];
                array[i + 1] = array[high];
                array[high] = temp1;
                return i + 1;
            }
        }
    }
