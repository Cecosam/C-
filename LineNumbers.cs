using System;
using System.IO;

namespace Homework
{
    class LineNumbers
    {
        /* Write a program that reads a text file and inserts line numbers in front of each of its lines.
         * The result should be written to another text file. Use StreamReader in combination with StreamWriter.
         */
        private static string filename = String.Empty;
        public static void CopyFile(string name = "File")
        {
            filename = name;
            try
            {
                StreamReader streamReader = new StreamReader(filename + ".txt");
                StreamWriter streamWriter = new StreamWriter("CopyOf" + filename + ".txt");

                using (streamReader)
                {
                    using (streamWriter)
                    {
                        int count = 0;
                        while (true)
                        {
                            string line = streamReader.ReadLine();
                            if (line == null)
                            {
                                break;
                            }
                            streamWriter.WriteLine("{0,3} {1}", count, line);
                            count++;
                        }
                        Console.WriteLine("Done!");
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("File Not Found!");
                return;
            }
        }
    }
}
