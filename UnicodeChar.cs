using System;
using System.Collections.Generic;
using System.Text;


namespace Homework
{
    class UnicodeChar
    {
        // Write a program that converts a string to a sequence of C# Unicode character literals.

        public static void ConvertStringIntoUnicode()
        {
            Console.WriteLine("Please enter some text:");
            char[] text = Console.ReadLine().ToCharArray();
            StringBuilder result = new StringBuilder();
            foreach (var item in text)
            {
                result.Append("\\u" + ((int)item).ToString("X4") + " ");
            }
            Console.WriteLine("Here is the result:");
            Console.WriteLine("{0}", result.ToString());
        }
    }
}
