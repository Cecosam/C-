using System;
using System.Collections.Generic;



namespace Homework
{
    class Palindromes
    {   
        /* Write a program that extracts from a given text all palindromes, e.g. ABBA, lamal,
         * exe and prints them on the console on a single line, separated by comma and space.
         * Use spaces, commas, dots, question marks and exclamation marks as word delimiters.
         * Print only unique palindromes, sorted lexicographically.
         */

        public static void FindPalindromes()
        {
            Console.WriteLine("Please enter some text:");
            string[] text = Console.ReadLine().Split(new[] {',',' ','?','!','.'}, StringSplitOptions.RemoveEmptyEntries);
            bool check;
            SortedSet<string> result = new SortedSet<string>();
            for (int index = 0; index < text.Length; index++)
            {
                check = true;
                for (int index2 = 0; index2 < Math.Ceiling((double)text[index].Length/2) ; index2++)
                {
                    if (text[index][index2] != text[index][text[index].Length - index2 - 1])
                    {
                        check = false;
                        break;
                    }
                }
                if (check == true)
                {
                    result.Add(text[index]);
                }
            }
            Console.WriteLine("Here are the palindromes:");
            foreach (var item in result)
            {
                if (result.Max.Equals(item))
                {
                    Console.WriteLine("{0}!",item);
                }
                else
                {
                    Console.Write("{0}, ",item);
                }
            }
        }
    }
}
