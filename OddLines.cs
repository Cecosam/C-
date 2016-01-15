using System;
using System.IO;



namespace Homework
{
    class OddLines
    {
        // Write a program that reads a text file and prints on the console its odd lines. Line numbers starts from 0.

        public static void ReadLines()
        {
            StreamReader streamReader = new StreamReader("file.txt");

            using (streamReader)
            {
                int count = 0;
                while (true)
                {
                    string line = streamReader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    Console.WriteLine("{0,3} {1}", count ,line);
                    count++;
                }
            }
        }
    }
}
