using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Homework
{
    class TextFilter
    {
        /* Write a program that takes a text and a string of banned words. All words included in the ban
         * list should be replaced with asterisks "*", equal to the word's length. The entries in the ban
         * list will be separated by a comma and space ", ". The ban list should be entered on the first
         * input line and the text on the second input line.
         */

        public static void FlitrateText()
        {
            Console.WriteLine("Please enter the banned words: ");
            string[] bannedWords = Console.ReadLine().Split(new[] {',', ' ', '?', '!', '.'}, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine("Please enter the some text:");
            string text = Console.ReadLine();

            foreach (var item in bannedWords)
            {
                Regex regex = new Regex(@"(" + item + ")");
                text = regex.Replace(text, MakeReplacement(item));
            }
            Console.WriteLine("Here is the result:");
            Console.WriteLine("{0}", text);
        }

        private static string MakeReplacement(string word)
        {
            StringBuilder replacement = new StringBuilder();
            foreach (var item in word)
            {
                replacement.Append("*");
            }
            return replacement.ToString();
        }

    }
}
