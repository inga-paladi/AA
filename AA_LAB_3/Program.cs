using System;
using System.Diagnostics;

namespace AA_LAB_3
{
    internal class Program
    {
        static void Main(string[] args)
            {
                int[] nValues = { 1000,5000, 10000, 50000, 100000 };
                foreach (int n in nValues)
                {
                    Console.WriteLine($"{n} " );
                    bool[] c = new bool[n + 1];

                    Stopwatch sw = new Stopwatch();

                    // measure the time for Algorithm1
                    sw.Start();
                    Algorithm1(c, n);
                    sw.Stop();
                    Console.WriteLine($" {sw.ElapsedTicks * 1000000000 / Stopwatch.Frequency}ns");

                    // measure the time for Algorithm2
                    sw.Restart();
                    Algorithm2(c, n);
                    sw.Stop();
                    Console.WriteLine($" {sw.ElapsedTicks * 1000000000 / Stopwatch.Frequency}ns");


                    // measure the time for Algorithm3
                    sw.Restart();
                    Algorithm3(c, n);
                    sw.Stop();
                    Console.WriteLine($" {sw.ElapsedTicks * 1000000000 / Stopwatch.Frequency}ns");


                    // measure the time for Algorithm4
                    sw.Restart();
                    Algorithm4(c, n);
                    sw.Stop();
                    Console.WriteLine($" {sw.ElapsedTicks * 1000000000 / Stopwatch.Frequency}ns");


                    // measure the time for Algorithm5
                    sw.Restart();
                    Algorithm5(c, n);
                    sw.Stop();
                    Console.WriteLine($" {sw.ElapsedTicks * 1000000000 / Stopwatch.Frequency}ns");

                    }
            }

                static void Algorithm1(bool[] c, int n)
                {
                    c[1] = false;
                    for (int i = 2; i <= n; i++)
                    {
                        c[i] = true;
                    }

                    for (int i = 2; i * i <= n; i++)
                    {
                        if (c[i] == true)
                        {
                            for (int j = i * i; j <= n; j += i)
                            {
                                c[j] = false;
                            }
                        }
                    }
                }

                static void Algorithm2(bool[] c, int n)
                {
                    // Mark all numbers as potential primes
                    for (int i = 2; i <= n; i++)
                    {
                        c[i] = true;
                    }
                    // Sieve algorithm
                    for (int i = 2; i <= n; i++)
                    {
                        if (c[i])
                        {
                            for (int j = i * 2; j <= n; j += i)
                            {
                                c[j] = false;
                            }
                        }
                    }
                }

                static void Algorithm3(bool[] c, int n)
                {
                    // Mark all numbers as potential primes
                    for (int i = 2; i <= n; i++)
                    {
                        c[i] = true;
                    }
                    // Sieve algorithm
                    for (int i = 2; i <= n; i++)
                    {
                        if (c[i])
                        {
                            for (int j = i + 1; j <= n; j++)
                            {
                                if (j % i == 0)
                                {
                                    c[j] = false;
                                }
                            }
                        }
                    }
                }

                static void Algorithm4(bool[] c, int n)
                {
                    // Mark all numbers as potential primes
                    for (int i = 2; i <= n; i++)
                    {
                        c[i] = true;
                    }
                    // Trial division algorithm
                    for (int i = 2; i <= n; i++)
                    {
                        for (int j = 2; j < i; j++)
                        {
                            if (i % j == 0)
                            {
                                c[i] = false;
                                break;
                            }
                        }
                    }
                }

                static void Algorithm5(bool[] c, int n)
                {
                    // Mark all numbers as potential primes
                    for (int i = 2; i <= n; i++)
                    {
                        c[i] = true;
                    }
                    // Optimized trial division algorithm
                    for (int i = 2; i <= n; i++)
                    {
                        int sqrti = (int)Math.Sqrt(i);
                        for (int j = 2; j <= sqrti; j++)
                        {
                            if (i % j == 0)
                            {
                                c[i] = false;
                                break;
                            }
                        }
                    }
                }
    }
}






