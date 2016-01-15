using System;
using System.Collections.Generic;
using System.IO;


namespace Homework
{
    class WordsInText
    {
        /* Write a program that reads a list of words from the file words.txt and finds how many times
         * each of the words is contained in another file text.txt. Matching should be case-insensitive.
         * Write the results in file results.txt. Sort the words by frequency in descending order. Use
         * StreamReader in combination with StreamWriter.
         */
        private static Dictionary<string, int> result = new Dictionary<string, int>();
        public static void CountWordsInFile()
        {
            StreamReader readerWords = new StreamReader("File.txt");
            StreamReader readerText = new StreamReader("Text.txt");
            StreamWriter writeResult = new StreamWriter("Result.txt");            

            try
            {
                while (true)
                {
                    string word = readerWords.ReadLine();
                    if (word == null) {
                        break;
                    }
                    CheckIfWordExists(word);
                    readerText = new StreamReader("Text.txt");
                    while (true)
                    {
                        string text = readerText.ReadLine();
                        if (text == null)
                        {
                            break;
                        }
                        text = text.ToLower();
                        string[] arrayOfWords = text.Split(new[] { ',', ' ', '?', '!', '.','-' }, StringSplitOptions.RemoveEmptyEntries);
                        FindMatchingWords(arrayOfWords , word);
                    }
                 
                }
                PrintResult(writeResult);

            }
            finally
            {
                readerWords.Close();
                readerText.Close();
                writeResult.Close();
            }
        }

        private static void CheckIfWordExists(string word)
        {
            if (!result.ContainsKey(word))
            {
                result[word] = 0;       
            }
        }

        private static void FindMatchingWords(string[] array, string word)
        {
            foreach (var item in array)
            {
                Console.WriteLine(item);
                if (item == word)
                {
                    result[word] += 1;
                }
            }
        }

        private static void PrintResult(StreamWriter writeResult)
        {
            foreach (var item in result)
            {
                writeResult.WriteLine("{0} : {1}", item.Key, item.Value);
            }
        }

    }
}
