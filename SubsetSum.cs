using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class SubsetSum
    {
        /* Write a program that reads from the console a number N and an array of integers given on a single line.
         * Your task is to find all subsets within the array which have a sum equal to N and print them on the console
         * (the order of printing is not important). Find only the unique subsets by filtering out repeating numbers first.
         * In case there aren't any subsets with the desired sum, print "No matching subsets."
         */
        private static bool matchCheck = false;
        public static void FindSubsetSum()
        {
            Console.Write("Please enter the number N: ");
            int number = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the numbers:");
            int[] num = Console.ReadLine().Split().Select(int.Parse).ToArray();
            num = num.Distinct().ToArray();

            List<List<int>> allSums = FindUniqueSums(num, number);

            if (!matchCheck)
            {
                Console.WriteLine("No matching subsets.");
            }
            else
            {
                foreach (var item in allSums)
                {
                    Print(item, number);
                }

            }
        }

        private static void Print(List<int> nums, int sum)
        {

            foreach (var item in nums)
            {
                Console.Write("{0} + ", item);
            }
            Console.WriteLine("\b\b\b = {0}", sum);
        }

        private static List<List<int>> FindUniqueSums(int[] numbers, int number)
        {
            double combinations = Math.Pow(2, numbers.Length);

            List<int> temporaryArray = new List<int>();
            List<List<int>> allSums = new List<List<int>>();

            for (int index = 1; index < combinations; index++)
            {
                int sum = 0;
                for (int index2 = 0; index2 < numbers.Length; index2++)
                {
                    int mask = index & (1 << index2);
                    if (mask != 0)
                    {
                        sum += numbers[numbers.Length - 1 - index2];
                        temporaryArray.Add(numbers[numbers.Length - 1 - index2]);
                    }
                }

                if (sum == number)
                {
                    allSums.Add(temporaryArray);
                    matchCheck = true;
                }
                temporaryArray = new List<int>();
            }
            return allSums;
        }
    }
}
