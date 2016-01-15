using System;

namespace Homework
{
    class MaximalSum
    {
        /* Write a program that reads a rectangular integer matrix of some size and finds in it the matrix N x M that has maximal sum of its elements. 
         * (On the first line, you will receive the rows N and columns M. On the next N lines you will receive each row with its columns.) (skiped that part) 
         * Print the elements of the 3 x 3 square as a matrix, along with their sum.
         */
        private static int bestSum = Int32.MinValue;
        public static void FindMaximalSumInMatrix()
        {
            int[,] array = {
                               {1,6,34,8,45,2,43,82,32,12},
                               {12,77,24,53,6,21,78,90,21,10},
                               {50,21,6,2,65,78,21,11,49,21},
                               {42,21,79,45,73,55,1,43,25,4},
                               {52,12,67,24,56,23,87,4,32,10},
                               {88,21,32,53,77,12,4,16,23,44}
                           };

            Console.Write("Please enter N: ");
            int n = int.Parse(Console.ReadLine());
            if (array.GetLength(0) < n)
            {
                Console.WriteLine("N should be smaller!");
                return;
            }
            Console.Write("Please enter M: ");

            int m = int.Parse(Console.ReadLine());
            if (array.GetLength(1) < m)
            {
                Console.WriteLine("M should be smaller!");
                return;
            }

            int[,] bestSumArray = FindMaximalSum(array, n, m);

            PrintSum(bestSumArray);

            Console.WriteLine("Best sum: {0}", bestSum);
        }

        private static int[,] FindMaximalSum(int[,] array, int n, int m)
        {
            int[,] bestSumArray = new int[n,m];
            int[,] tempSum;
            for (int row = 0; row <= array.GetLength(0) - n; row++)
            {
                for (int col = 0; col <= array.GetLength(1) - m; col++)
                {
                    tempSum = new int[n, m];  
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < m; j++) 
                        {
                            tempSum[i,j] += array[row + i,col + j];
                        }
                    }
                    if (FindSum(tempSum) > FindSum(bestSumArray))
                    {
                        bestSumArray = CopyArray(tempSum);
                        bestSum = FindSum(tempSum);
                    }
                }
            }
            return bestSumArray;
        }

        private static int FindSum(int[,] array)
        {
            int sum = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    sum += array[i, j];
                }
            }
            return sum;
        }

        private static int[,] CopyArray(int[,] array)
        {
            int[,] copy = new int[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    copy[i,j] = array[i, j];
                }
            }
            return copy;
        }

        private static void PrintSum(int[,] array)
        {
            Console.WriteLine("Printing array:");
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write("{0,3}", array[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
