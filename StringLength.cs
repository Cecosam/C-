using System;
using System.Text;

namespace Homework
{
    class StringLength
    {
        /* Write a program that reads from the console a string of maximum 20 characters.
         * If the length of the string is less than 20, the rest of the characters should
         * be filled with *. Print the resulting string on the console.
         */
        private const int magicNumber = 20;
        public static void PrintString()
        {
            Console.WriteLine("Please enter a string:");
            char[] input = Console.ReadLine().ToCharArray();
            StringBuilder result = new StringBuilder();
            if (input.Length >= magicNumber)
            {
                for (int index = 0; index < magicNumber; index++)
                {
                    result.Append(input[index]);
                }
            }
            else
            {
                foreach (var item in input)
                {
                    result.Append(item);
                }
                for (int index = input.Length; index < magicNumber; index++)
                {
                    result.Append('*');
                }
            }
            Console.WriteLine("Here is the result:");
            Console.WriteLine("{0}", result.ToString());

        }
    }
}
