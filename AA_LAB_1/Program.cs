using System;
using System.Diagnostics;

namespace Fibonacci
{
    internal class Program
    {
        static int[] nths = new int[] {5, 15, 22, 27, 32, 43, 44, 45};

        static void Main(string[] args)
        {
            // MethodRecursive();
            MethodIterative();
            MethodBinet();
            MethodGoldenRatio();
            MethodExponentialMatrix();
        }

        //recursive method-----------------------------------------------------
        public static void MethodRecursive()
        {
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("Recursive method =======================");

            foreach (int nth in nths)
            // for (int nth = 3; nth <= 45; nth++)
            {
                stopwatch.Start();
                int response = Fib(nth-1);
                stopwatch.Stop();

                Console.WriteLine("{0} = {1} : {2}ns"
                    , nth
                    , response
                    , 1000000000.0 * (double)stopwatch.ElapsedTicks / Stopwatch.Frequency
                    );
            }
        }

        public static int Fib(int n)
        {
            if (n <=2)
                return 1;
            else
                return Fib( n - 1 ) + Fib( n - 2 );
        }

        // end of recursive method----------------------------------------------

        // binet --------------------------------------------------------------

        public static void MethodBinet()
        {
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("\nBinet method ===========================");

            foreach (int nth in nths)
            // for (int nth = 3; nth <= 45; nth++)
            {
                stopwatch.Start();
                int response = GetNthFib_binet(nth);
                stopwatch.Stop();

                Console.WriteLine("{0} = {1} : {2}ns"
                    , nth
                    , response
                    , 1000000000.0 * (double)stopwatch.ElapsedTicks / Stopwatch.Frequency
                    );
            }
        }

        // din cauza ca se foloseste double si virgula nu ne permite
        // sa luam numere asa de mari ca int, metoda ne returneaza corect
        // doar pana la 45
        public static int GetNthFib_binet(int n)
        {
            n--;
            double phi = (1 + Math.Sqrt(5)) / 2;
            double psi = -1 * (1 - Math.Sqrt(5)) / 2;
            double res = (int)(Math.Pow(phi, n) - Math.Pow(psi, n)) / Math.Sqrt(5);
            return (int)Math.Round(res);
        }

        // end binet ----------------------------------------------------------

        // Method iterative ---------------------------------------------------
        public static void MethodIterative()
        {
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("\nIterative method =======================");

            foreach (int nth in nths)
            // for ( int nth = 3; nth <= 45; nth++)
            {
                stopwatch.Start();
                UInt64 response = GetNthFib_Ite(nth);
                stopwatch.Stop();

                Console.WriteLine("{0} = {1} : {2}ns"
                    , nth
                    , response
                    , 1000000000.0 * (double)stopwatch.ElapsedTicks / Stopwatch.Frequency
                    );

            }
        }

        public static UInt64 GetNthFib_Ite(int n)
        {
            int number = n - 1; //Need to decrement by 1 since we are starting from 0
            UInt64[] Fib = new UInt64[number + 1];
            Fib[0]= 0;
            Fib[1]= 1;
            for (int i = 2; i <= number;i++)
            {
                Fib[i] = Fib[i - 2] + Fib[i - 1];
            }
            return Fib[number];
        }

        // end of iterative method---------------------------------------------

        // 4 th method to get the nth Fib term golden ratio -------------------

        public static void MethodGoldenRatio()
        {
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("\nGolden ration method ===================");

            foreach (int nth in nths)
            // for (int nth = 3; nth <= 45; nth++)
            {
                stopwatch.Start();
                double response = GetNthFib_GoldR(nth);
                stopwatch.Stop();

                Console.WriteLine("{0} = {1} : {2}ns"
                    , nth
                    , response
                    , 1000000000.0 * (double)stopwatch.ElapsedTicks / Stopwatch.Frequency
                    );

            }
        }
        public static double  GetNthFib_GoldR(int n)
        {
            n--;
            double phi = (1 + Math.Sqrt(5)) / 2;
            double res = (int)Math.Pow(phi, n) / Math.Sqrt(5);
            return Math.Round(res);
        }

        // End of golden ratio ------------------------------------------------

        // Exponential matrix -------------------------------------------------

        static void MethodExponentialMatrix()
        {
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("\nExponenential matrix method ============");

            foreach (int nth in nths)
            // for (int nth = 3; nth <= 45; nth++)
            {
                stopwatch.Start();
                int response = GetNthFib_Exponential(nth);
                stopwatch.Stop();

                Console.WriteLine("{0} = {1} : {2}ns"
                    , nth
                    , response
                    , 1000000000.0 * (double)stopwatch.ElapsedTicks / Stopwatch.Frequency
                    );

            }
        }

        static int GetNthFib_Exponential(int n)
        {
            n-=2;
            int[,] F = {{1, 1}, {1, 0}};
            int[,] result = {{1, 1}, {1, 0}};
            if (n <= 0)
                return 0;
            for (int i = 2; i <= n; i++)
            {
                result = MultiplyMatrix(result, F);
                // Console.WriteLine(String.Join(", ", result.Cast<int>()));
            }
            return result[0, 0];
        }

        static int[,] MultiplyMatrix(int[,] A, int[,] B)
        {
            int[,] C = new int[2, 2];
            C[0, 0] = A[0, 0] * B[0, 0] + A[0, 1] * B[1, 0];
            C[0, 1] = A[0, 0] * B[0, 1] + A[0, 1] * B[1, 1];
            C[1, 0] = A[1, 0] * B[0, 0] + A[1, 1] * B[1, 0];
            C[1, 1] = A[1, 0] * B[0, 1] + A[1, 1] * B[1, 1];
            return C;
        }

        // End exponential matrix ---------------------------------------------
    }
}






