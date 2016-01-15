using System;
using System.Collections.Generic;

namespace Homework
{
    class LIS
    {
        /* Write a program to find all increasing sequences inside an array of integers.
         * The integers are given on a single line, separated by a space. Print the sequences 
         * in the order of their appearance in the input array, each at a single line. Separate
         * the sequence elements by a space. Find also the longest increasing sequence and print
         * it at the last line. If several sequences have the same longest length, print the left-most of them.
         */

        public static void FindTheLongestSequence()
        {
            Console.WriteLine("Please eneter the numbers:");
            string temp = Console.ReadLine();
            int[] numbers = new int[temp.Split(' ').Length];
            for (int index = 0; index < numbers.Length; index++)
            {
                try
                {
                    numbers[index] = Int32.Parse(temp.Split(' ')[index]);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }

            List<int> temporaryList = new List<int>();
            List<int> longestSeq = new List<int>();
            for (int index = 0; index < numbers.Length - 1; index++)
            {
                if (numbers[index] >= numbers[index + 1])
                {
                    Console.WriteLine("{0}", numbers[index]);
                    temporaryList.Add(numbers[index]);
                    if (temporaryList.Count > longestSeq.Count)
                    {
                        longestSeq.Clear();
                        foreach (var item in temporaryList)
                        {
                            longestSeq.Add(item);     
                        }
                    }
                    temporaryList = new List<int>();
                }
                else
                {
                    Console.Write("{0}, ", numbers[index]);
                    temporaryList.Add(numbers[index]);
                }
            }

            if (numbers[numbers.Length - 1] < numbers[numbers.Length - 2])
            {
                Console.WriteLine("{0}", numbers[numbers.Length - 1]);
                temporaryList.Add(numbers[numbers.Length - 1]);
                if (temporaryList.Count > longestSeq.Count)
                {
                    longestSeq.Clear();
                    foreach (var item in temporaryList)
                    {
                        longestSeq.Add(item);
                    }
                    temporaryList.Clear();
                }
            }
            else
            {
                Console.WriteLine("{0}", numbers[numbers.Length - 1]);
                temporaryList.Add(numbers[numbers.Length - 1]);
                if (temporaryList.Count > longestSeq.Count)
                {
                    longestSeq.Clear();
                    foreach (var item in temporaryList)
                    {
                        longestSeq.Add(item);
                    }
                    temporaryList.Clear();
                }
            }
            Console.WriteLine("Longest sequence:");
            foreach (var item in longestSeq)
            {
                if (longestSeq.IndexOf(item) + 1 == longestSeq.Count)
                {
                    Console.WriteLine("{0}!", item);

                }
                else
                {
                    Console.Write("{0}, ", item);
                }
            }
        }
    }
}
