using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class SlicingFile
    {
        // Write a program that takes any file and slices it to n parts.

        public static void Slice(string sourceFile, string destinationDirectory, int parts = 5)
        {
            FileStream inputStream = new FileStream(sourceFile, FileMode.Open);
            FileStream outputStream;

            long partLength = (long)Math.Ceiling((double)inputStream.Length / parts);
            int loopsPerPart = (int)Math.Ceiling((double)partLength / 4096);

            byte[] buffer = new byte[4096];

            int offset = 0;

            for (int i = 1; i <= parts; i++)
            {
                outputStream = new FileStream("Part-" + i + ".mp3", FileMode.Create);
                for (int index = 0; index < loopsPerPart; index++)
			    {
			        int bytes = inputStream.Read(buffer, 0 ,buffer.Length);
                    Console.WriteLine(index);
                    if (bytes == 0)
                    {
                        break;
                    }
                    outputStream.Write(buffer, 0, bytes);
			    }
                offset += (int)partLength;
            }
         
        }

        public static void Assemble()

    }
}
