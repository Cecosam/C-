using System;
using System.Collections.Generic;


namespace Homework
{
    class StringCompare
    {
           /* Write a program that reads an array of strings and finds in it all sequences of equal
            * elements (comparison should be case-sensitive). The input strings are given as a single
            * line, separated by a space. 
            */
        public static void EqualStringsComparer()
        {
            Console.WriteLine("Please enter the words to compare:");
            string temp = Console.ReadLine();
            if(temp.Length == 0) {
                return;
            }
            string[] words = temp.Split(' ');
            for (int index = 0; index < words.Length-1; index++)
            {
                if(words[index].Equals(words[index+1])) {
                    Console.Write("{0} ",words[index]);
                } else {
                    Console.WriteLine("{0} ", words[index]);
                }
            }
            if (words[words.Length-1].Equals(words[words.Length-2])) {
                Console.Write("{0} ",words[words.Length-1]);
            } else {
                Console.WriteLine("{0} ", words[words.Length-1]);
            }
            Console.WriteLine();
        }
    }
}
