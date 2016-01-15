using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class SubstringOccurrences
    {
        /* Write a program to find how many times a given string appears in a given text as substring.
         * The text is given at the first input line. The search string is given at the second input line.
         * The output is an integer number. Please ignore the character casing. Overlapping between
         * occurrences is allowed. 
         */

        public static void CountSubstringOccurrences()
        {
            Console.WriteLine("Please enter a string: ");
            string input = Console.ReadLine();
            Console.WriteLine("Please enter the word to compare with:");
            string word = Console.ReadLine();
            int count = 0;
            for (int index = 0; index <= input.Length - word.Length; index++)
            {
                if (input.Substring(index,word.Length).Equals(word)) 
                {
                    count++;
                }
            }
            Console.WriteLine("The given string appears {0} times", count);
        }
    }
}
