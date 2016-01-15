using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class StringReverser
    {
        // Write a program that reads a string from the console, reverses it and prints the result back at the console.

        public static void ReverseString()
        {
            Console.WriteLine("Please enter a string to reverse:");
            char[] temp = Console.ReadLine().ToCharArray();
            StringBuilder result = new StringBuilder();
            for (int index = 1; index <= temp.Length; index++)
            {
                result.Append(temp[temp.Length - index]);
            }
            Console.WriteLine("Here is the reversed string:");
            Console.WriteLine("{0}", result.ToString());
        }
    }
}
