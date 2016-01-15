using System;
using System.Collections.Generic;


namespace Homework
{
    class MinMaxSumAverage
    {
        /* Write a program that reads N floating-point numbers from the console. Your task is to separate them in two sets,
         * one containing only the round numbers (e.g. 1, 1.00, etc.) and the other containing the floating-point numbers with non-zero fraction.
         * Print both arrays along with their minimum, maximum, sum and average (rounded to two decimal places).
         */

        public static void FindMinMaxAverage()
        {
            Console.WriteLine("Please enter the numbers with space between them: ");
            string temp = Console.ReadLine();
            List<int> roundNumbers = new List<int>();
            List<double> floatingPointNumbers = new List<double>();
            double[] numbers = new double[temp.Split(' ').Length];
            for (int index = 0; index < numbers.Length; index++)
            {
                try
                {
                    numbers[index] = double.Parse(temp.Split(' ')[index]);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }

            for (int index = 0; index < numbers.Length; index++)
            {
                if (numbers[index] % 1 == 0)
                {
                    roundNumbers.Add(Convert.ToInt32(numbers[index]));
                }
                else
                {
                    floatingPointNumbers.Add(numbers[index]);
                }
            }

            int maxRounded = GetMaxNumber(roundNumbers);
            int minRounded = GetMinNumber(roundNumbers);
            int sumRounded = GetSum(roundNumbers);
            double averageRounded = GetAverage(roundNumbers);

            double maxFloatingPoint = GetMaxNumber(floatingPointNumbers);
            double minFloatingPoint = GetMinNumber(floatingPointNumbers);
            double sumFloatingPoint = GetSum(floatingPointNumbers);
            double averageFloatingPoint = GetAverage(floatingPointNumbers);

            Console.WriteLine("Round Numbers:");
            foreach (var item in roundNumbers)
            {
                Console.Write("{0}, ", item);
            }
            Console.Write("max: {0}, min: {1}, sum: {2}, average: {3}!", maxRounded,minRounded,sumRounded,Math.Round(averageRounded,2));
            Console.WriteLine();
            Console.WriteLine("Floating-Point Numbers:");
            foreach (var item in floatingPointNumbers)
            {
                Console.Write("{0}, ", item);
            }
            Console.Write("max: {0}, min: {1}, sum: {2}, average: {3}!", Math.Round(maxFloatingPoint,2), Math.Round(minFloatingPoint,2),
                Math.Round(sumFloatingPoint,2), Math.Round(averageFloatingPoint,2));
            Console.WriteLine();
        }

        private static int GetMaxNumber(List<int> roundNumbers)
        {
            int maxValue = Int32.MinValue;
            foreach (var item in roundNumbers)
            {
                if (item > maxValue)
                {
                    maxValue = item;
                }
            }
            return maxValue;
        }

        private static double GetMaxNumber(List<double> floatingPointNumbers)
        {
            double maxValue = double.MinValue;
            foreach (var item in floatingPointNumbers)
            {
                if (item > maxValue)
                {
                    maxValue = item;
                }
            }
            return maxValue;
        }

        private static int GetMinNumber(List<int> roundNumbers)
        {
            int minValue = Int32.MaxValue;
            foreach (var item in roundNumbers)
            {
                if (item < minValue)
                {
                    minValue = item;
                }
            }
            return minValue;
        }

        private static double GetMinNumber(List<double> floatingPointNumbers)
        {
            double minValue = double.MaxValue;
            foreach (var item in floatingPointNumbers)
            {
                if (item < minValue)
                {
                    minValue = item;
                }
            }
            return minValue;
        }

        private static int GetSum(List<int> roundNumbers)
        {
            int sum = 0;
            foreach (var item in roundNumbers)
            {
                sum += item;
            }
            return sum;
        }

        private static double GetSum(List<double> floatingPointNumbers)
        {
            double sum = 0;
            foreach (var item in floatingPointNumbers)
            {
                sum += item;
            }
            return sum;
        }

        private static double GetAverage(List<int> roundNumbers)
        {
            int sum = 0;
            foreach (var item in roundNumbers)
            {
                sum += item;
            }
            return sum / roundNumbers.Count;
        }

        private static double GetAverage(List<double> floatingPointNumbers)
        {
            double sum = 0;
            foreach (var item in floatingPointNumbers)
            {
                sum += item;
            }
            return sum / floatingPointNumbers.Count;
        }
    }
}
