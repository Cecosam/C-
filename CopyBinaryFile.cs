using System;
using System.IO;


namespace Homework
{
    class CopyBinaryFile
    {
        /* Write a program that copies the contents of a binary file (e.g. image, video, etc.) to another
         * using FileStream. You are not allowed to use the File class or similar helper classes.
         */

        public static void CopyFile()
        {
            FileStream streamRead = new FileStream("song.mp3", FileMode.Open);
            FileStream streamWrite = new FileStream("CopyOfsong.mp3", FileMode.Create);

            using (streamRead)
            {
                using (streamWrite)
                {
                    byte[] buffer = new byte[4096];
                    while (true)
                    {
                        int bytes = streamRead.Read(buffer, 0, buffer.Length);
                        if (bytes == 0)
                        {
                            break;
                        }

                        streamWrite.Write(buffer, 0, bytes);
                    }
                }
            }
        }
    }
}
