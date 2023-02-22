using System;
using System.Diagnostics;

namespace AA_LAB_2
{
    internal class SortingAlgorithms
    {

        static void Main(string[] args)
        {
            int[] sizeArray = new int[]{100,250,500,1000,2500,5000,10000,50000,100000};
            foreach (int element in sizeArray){
            Console.WriteLine("\nThe size of array is " + element);
                List<Int64> generatedArray = GenerateArrayOfUnorderedNumbers(element);
                //QuickSort----------------------------------------------------

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                List<Int64> arrayForQuickSort = new List<Int64>(generatedArray);
                QuickSort(arrayForQuickSort, 0, arrayForQuickSort.Count - 1);
                stopwatch.Stop();
                // Format and display the TimeSpan value.
                Console.WriteLine("QuickSort: RunTime {0} ms", stopwatch.Elapsed);
                stopwatch.Reset();

                //MergeSort----------------------------------------------------
                stopwatch.Start();
                List<Int64> arrayForMergeSort = new List<Int64>(generatedArray);
                MergeSort(arrayForMergeSort, 0, arrayForMergeSort.Count - 1);
                stopwatch.Stop();
                // Format and display the TimeSpan value.
                Console.WriteLine("MergeSort: RunTime {0} ms", stopwatch.Elapsed);
                stopwatch.Reset();

                stopwatch.Start();
                List<Int64> arrayForHeapSort = new List<Int64>(generatedArray);
                HeapSort(arrayForHeapSort,arrayForHeapSort.Count - 1);
                stopwatch.Stop();
                // Format and display the TimeSpan value.
                Console.WriteLine("HeapSort: RunTime {0} ms", stopwatch.Elapsed);
                stopwatch.Reset();

                stopwatch.Start();
                List<Int64> arrayForRadixSort = new List<Int64>(generatedArray);
                RadixSort(arrayForRadixSort);
                stopwatch.Stop();
                // Format and display the TimeSpan value.
                Console.WriteLine("RadixSort: RunTime {0} ms", stopwatch.Elapsed);
                stopwatch.Reset();
            }
        }

        private static List<Int64> GenerateArrayOfUnorderedNumbers(int sizeArray)
        {
            List<Int64> generatedArray = new List<Int64>(sizeArray);
            Random random = new Random();
            for (int i = 0; i < generatedArray.Capacity; i++)
            {
                generatedArray.Add(random.NextInt64(1,sizeArray));
            }
            return generatedArray;

        }
        //quickSort method-----------------------------------------------------
        public static void QuickSort(List<Int64> unsortedArray,int left, int right)
        {
        int pivot;
        if (left < right){
            pivot = Partition(unsortedArray, left, right);
            if (pivot > 1) {
               QuickSort(unsortedArray, left, pivot - 1);
            }
            if (pivot + 1 < right) {
               QuickSort(unsortedArray, pivot + 1, right);
            }
        }
        }
        static public int Partition(List<Int64> unsortedArray, int left, int right)
        {
            Int64 temp;
            Int64 pivot = unsortedArray[right];
            int i = left - 1;

            for (int j = left; j <= right - 1; j++)
            {
                if (unsortedArray[j] <= pivot)
                {
                    i++;
                    temp = unsortedArray[i];
                    unsortedArray[i] = unsortedArray[j];
                    unsortedArray[j] = temp;
                }
            }
            temp = unsortedArray[i + 1];
            unsortedArray[i + 1] = unsortedArray[right];
            unsortedArray[right] = temp;
            return i + 1;
        }
        //END quickSort method -------------------------------------------------
        //MergeSort method-----------------------------------------------------
        public static void MergeSort(List<Int64> unsortedArray, int left, int right) {
        if (left < right) {
            int midle = (left + right) / 2;
            MergeSort(unsortedArray, left, midle);
            MergeSort(unsortedArray, midle + 1, right);
            Merge(unsortedArray, left, midle, right);
        }
    }
        public static void Merge(List<Int64> unsortedArray, int left, int midle, int right)
        {
            int i, j, k;
            int n1 = midle - left + 1;
            int n2 = right - midle;
            Int64[] L = new Int64[n1];
            Int64[] R = new Int64[n2];
            for (i = 0; i < n1; ++i)
                L[i] = unsortedArray[left + i];
            for (j = 0; j < n2; ++j)
                R[j] = unsortedArray[midle + 1 + j];
            i = 0;
            j = 0;
            k = left;
            while (i < n1 && j < n2) {
                if (L[i] <= R[j]) {
                    unsortedArray[k] = L[i];
                    i++;
                }
                else {
                    unsortedArray[k] = R[j];
                    j++;
                }
                k++;
            }
            while (i < n1) {
                unsortedArray[k] = L[i];
                i++;
                k++;
            }
            while (j < n2) {
                unsortedArray[k] = R[j];
                j++;
                k++;
            }
        }

        //END MergeSort method -------------------------------------------------
        //HeapSort method------------------------------------------------------
        public static void HeapSort(List<Int64> unsortedArray, int v)
        {
            int n = unsortedArray.Count();

            // Build max heap
            for (int i = n / 2 - 1; i >= 0; i--)
                heapify(unsortedArray, n, i);

            // Extract elements from heap one by one
            for (int i = n - 1; i >= 0; i--) {
                // Move current root to end
                Int64 temp = unsortedArray[0];
                unsortedArray[0] = unsortedArray[i];
                unsortedArray[i] = temp;

                // Call max heapify on the reduced heap
                heapify(unsortedArray, i, 0);
            }
        }
        static void heapify(List<Int64> unsortedArray, int n, int i) {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;

        // Check if left child is larger than root
        if (left < n && unsortedArray[left] > unsortedArray[largest])
            largest = left;

        // Check if right child is larger than largest so far
        if (right < n && unsortedArray[right] > unsortedArray[largest])
            largest = right;

        // If largest is not root
        if (largest != i) {
            Int64 swap = unsortedArray[i];
            unsortedArray[i] = unsortedArray[largest];
            unsortedArray[largest] = swap;

            // Recursively heapify the affected sub-tree
            heapify(unsortedArray, n, largest);
            }
        }
        //END HeapSort method --------------------------------------------------

        //radixSort method-----------------------------------------------------
        public static void RadixSort(List<Int64> unsortedArray)
        {
            Int64 maxDigits = GetMaxDigits(unsortedArray);
            int divisor = 1;
            for (int i = 0; i < maxDigits; i++)
            {
                CountingSort(unsortedArray, divisor);
                divisor *= 10;
            }
        }

        private static void CountingSort(List<Int64> unsortedArray, int divisor)
        {
            int n = unsortedArray.Count;
            Int64[] counts = new Int64[10];
            Int64[] sortedArray = new Int64[n];

            for (int i = 0; i < n; i++)
            {
                Int64 digit = (unsortedArray[i] / divisor) % 10;
                counts[digit]++;
            }

            for (int i = 1; i < 10; i++)
            {
                counts[i] += counts[i - 1];
            }

            for (int i = n - 1; i >= 0; i--)
            {
                Int64 digit = (unsortedArray[i] / divisor) % 10;
                Int64 index = counts[digit] - 1;
                sortedArray[index] = unsortedArray[i];
                counts[digit]--;
            }

            for (int i = 0; i < n; i++)
            {
                unsortedArray[i] = sortedArray[i];
            }
        }

        private static int GetMaxDigits(List<Int64> unsortedArray)
        {
            int max = 0;
            foreach (int num in unsortedArray)
            {
                int digits = (int)Math.Log10(num) + 1;
                if (digits > max)
                {
                    max = digits;
                }
            }
            return max;
        }
         //END radixSort method -------------------------------------------------
    }
}






