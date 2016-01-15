using System;
using System.Collections.Generic;

namespace Homework
{
    class Symbols
    {
        //Write a program that reads some text from the console and counts the occurrences of each character in it.
        //Print the results in alphabetical (lexicographical) order. 

        public static void CountSymbols()
        {
            Console.Write("Please enter a string: ");
            char[] symbols = Console.ReadLine().ToCharArray();
            SortedDictionary<char, int> symbCollection = new SortedDictionary<char,int>();
            for (int index = 0; index < symbols.Length; index++)
            {
                if (!symbCollection.ContainsKey(symbols[index]))
                {
                    symbCollection.Add(symbols[index],1);
                }
                else
                {
                    symbCollection[symbols[index]] += 1;
                }
            }
            foreach (var item in symbCollection)
            {
                Console.WriteLine("{0}: {1} time(s)",item.Key,item.Value);
            }
        }
    }
}
