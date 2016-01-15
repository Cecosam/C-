using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Homework
{
    class LettersChangeNumbersGame
    {
        /* Nakov likes Math. But he also likes the English alphabet a lot. He invented a game with
         * numbers and letters from the English alphabet. The game was simple. You get a string consisting
         * of a number between two letters. Depending on whether the letter was in front of the number or 
         * after it you would perform different mathematical operations on the number to achieve the result.
         * 
         * First you start with the letter before the number. If it's Uppercase you divide the number by the
         * letter's position in the alphabet. If it's lowercase you multiply the number with the letter's position.
         * Then you move to the letter after the number. If it's Uppercase you subtract its position from the 
         * resulted number. If it's lowercase you add its position to the resulted number. But the game became
         * too easy for Nakov really quick. He decided to complicate it a bit by doing the same but with multiple
         * strings keeping track of only the total sum of all results. Once he started to solve this with more
         * strings and bigger numbers it became quite hard to do it only in his mind. So he kindly asks you to
         * write a program that calculates the sum of all numbers after the operations on each number have been
         * done.
         */
        private static List<int> totalSum = new List<int>();
        public static void StartTheGame()
        {
            string[] input = GetInput();

            Console.WriteLine();

            foreach (var item in input)
            {
                CalculateItem(item);
                Console.WriteLine();
            }

            PrintResult();
        }

        private static string[] GetInput()
        {
            bool check = true;
            while (check)
            {
                Console.WriteLine("Please enter the string");
                string[] input = Console.ReadLine().Split(new[] { ',', ' ', '?', '!', '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (!CheckInput(input))
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }
                return input;
            }
            return null;
        }

        private static bool CheckInput(string[] input)
        {
            bool check = true;
            if (input.Length == 0)
            {
                check = false;
                return check;
            }
            foreach (var item in input)
            {
                if (item.Length <= 2)
                {
                    check = false;
                    return check;
                }
            }
            Regex regex = new Regex(@"[A-Za-z]");
            foreach (var item in input)
            {
                if (!regex.IsMatch(item[0].ToString()) || !regex.IsMatch(item[item.Length - 1 ].ToString()))
                {
                    check = false;
                    return check;
                }
            }

            regex = new Regex(@"[0-9]");
            foreach (var item in input)
            {
                for (int index = 1; index < item.Length - 1; index++)
                {
                    if (!regex.IsMatch(item[index].ToString()))
                    {
                        check = false;
                        return check;
                    }
                }
                
            }
            return check;
        }

        private static void CalculateItem(string item)
        {
            int number = int.Parse(GetNumber(item));
            int tempNumber = 0;
            if (Char.IsLower(item[0])) 
            {
                tempNumber = number * (char.ToUpper(item[0]) - 64);
                Console.WriteLine("{0} * {1} = {2} ", number, char.ToUpper(item[0]) - 64, tempNumber);
                if (Char.IsLower(item[item.Length - 1]))
                {
                    totalSum.Add(tempNumber + (char.ToUpper(item[item.Length - 1]) - 64));
                    Console.WriteLine("{0} + {1} = {2} ", tempNumber, (char.ToUpper(item[item.Length - 1]) - 64),
                        tempNumber + (char.ToUpper(item[item.Length - 1]) - 64));
                }
                else
                {
                    totalSum.Add(tempNumber - (char.ToUpper(item[item.Length - 1]) - 64));
                    Console.WriteLine("{0} - {1} = {2} ", tempNumber, (char.ToUpper(item[item.Length - 1]) - 64),
                        tempNumber - (char.ToUpper(item[item.Length - 1]) - 64));
                }
            }
            else
            {
                tempNumber = number / (char.ToUpper(item[0]) - 64);
                Console.WriteLine("{0} / {1} = {2} ", number, char.ToUpper(item[0]) - 64, tempNumber);
                if (Char.IsLower(item[item.Length - 1]))
                {
                    totalSum.Add(tempNumber + (char.ToUpper(item[item.Length - 1]) - 64));
                    Console.WriteLine("{0} + {1} = {2} ", tempNumber, (char.ToUpper(item[item.Length - 1]) - 64),
                        tempNumber + (char.ToUpper(item[item.Length - 1]) - 64));
                }
                else
                {
                    totalSum.Add(tempNumber - (char.ToUpper(item[item.Length - 1]) - 64));
                    Console.WriteLine("{0} - {1} = {2} ", tempNumber, (char.ToUpper(item[item.Length - 1]) - 64),
                       tempNumber - (char.ToUpper(item[item.Length - 1]) - 64));
                }
            }
        }

        private static string GetNumber(string item)
        {
            return item.Substring(1, item.Length - 2);
        }

        private static void PrintResult()
        {
            int result = 0;
            Console.WriteLine("Result is:");
            for (int index = 0; index < totalSum.Count; index++)
            {
                if (index + 1 == totalSum.Count)
                {
                    result += totalSum[index];
                    Console.WriteLine("{0} = {1}!",totalSum[index], result);
                    continue;
                }
                Console.Write("{0} +", totalSum[index]);
                result += totalSum[index];
            }
        }
    }
}
