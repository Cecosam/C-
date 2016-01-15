using System;
using System.Collections.Generic;

namespace Homework
{
    class SequenceInMatrix
    {
        /* We are given a matrix of strings of size N x M. Sequences in the matrix we define as sets of 
         * several neighbour elements located on the same line, column or diagonal. Write a program that
         * finds the longest sequence of equal strings in the matrix.
         */
        private static string word = "";
        public static void FindSequenceInMatrix()
        {
            string[,] arrayOfStrings = {
                                           {"abc","def","def","def","def","pqr"},
                                           {"stw","def","abc","def","ghi","mn"},
                                           {"nos","def","abc","xyz","abc","def"},
                                           {"ghi","def","mno","abc","stw","xyz"},
                                           {"abc","dsf","zno","jkl","mno","pqr"}
                                       };

            if (FindLongestSeqInRow(arrayOfStrings) >= FindLongestSeqInCol(arrayOfStrings))
            {
                if (FindLongestSeqInDiagonal(arrayOfStrings) <= FindLongestSeqInRow(arrayOfStrings))
                {
                    Console.WriteLine("The longest sequence of equal strings is on a row and it occurs {0} times!\r\n" +
                    "The word is {1}!", FindLongestSeqInRow(arrayOfStrings), word);
                }
                else
                {
                    Console.WriteLine("The longest sequence of equal strings is on a diagonal and it occurs {0} times!\r\n" +
                    "The word is {1}!", FindLongestSeqInDiagonal(arrayOfStrings), word);
                }
            }
            else if (FindLongestSeqInRow(arrayOfStrings) < FindLongestSeqInCol(arrayOfStrings))
            {
                if (FindLongestSeqInDiagonal(arrayOfStrings) <= FindLongestSeqInCol(arrayOfStrings))
                {
                    Console.WriteLine("The longest sequence of equal strings is on a column and it occurs {0} times!\r\n" +
                    "The word is {1}!", FindLongestSeqInCol(arrayOfStrings), word);
                }
                else
                {
                    Console.WriteLine("The longest sequence of equal strings is on a diagonal and it occurs {0} times!\r\n" +
                    "The word is {1}!", FindLongestSeqInDiagonal(arrayOfStrings), word);
                }
            }

        }


        private static int FindLongestSeqInDiagonal(string[,] array)
        {
            int count;
            int largestSeq = 0;
            bool foundMatch = false;
            for (int row = 0; row < array.GetLongLength(0); row++)
            {
                for (int col = 0; col < array.GetLongLength(1); col++)
                {
                    count = 0;
                    for (int index = 0; index < GetSize(array,row,col); index++)
                    {
                        if (array[row + index, col + index].Equals(array[row + index + 1, col + index + 1]))
                        {
                            count++;
                        }
                        else
                        {
                            count = 0;
                        }

                        if (count > largestSeq)
                        {
                            foundMatch = true;
                            largestSeq = count;
                            word = array[row + index, col + index];
                        }
                    }
                }
            }
            if (foundMatch)
            {
                largestSeq++;
            }
            return largestSeq;
        }


        private static int GetSize(string[,] array, int row, int col)
        {
            int difference;
            if (array.GetLength(0) > array.GetLength(1))
            {
                difference = array.GetLength(0) - array.GetLength(1);
                if (row - difference - col <= 0)
                {
                    return array.GetLength(1) - 1 - col;
                }
                else
                {
                    return array.GetLength(1) - 1 - (row - difference);
                }
            }
            else if (array.GetLength(0) < array.GetLength(1))
            {   
                difference = array.GetLength(1) - array.GetLength(0);
                if(col - difference - row <= 0) 
                {
                    return array.GetLength(0) - 1 - row;
                }
                else
                {
                    return array.GetLength(0) - 1 - (col- difference);
                }
            }
            else
            {
                if (col - row <= 0)
                {
                    return array.GetLength(0) - 1 - row;
                }
                else
                {
                    return array.GetLength(0) - 1 - col;
                }
            }
        }



        private static int FindLongestSeqInRow(string[,] array) 
        {
            int count;
            int largestSeq = 0;
            bool foundMatch = false;
            for (int row = 0; row < array.GetLength(0); row++)
			{
                count = 0;
	            for (int col = 0; col < array.GetLength(1) - 1; col++) 
                {
                    if(array[row,col].Equals(array[row,col+1])) 
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                    }

                    if (count > largestSeq)
                    {
                        foundMatch = true;
                        largestSeq = count;
                        word = array[row, col];
                    }
                }

            }
            if (foundMatch)
            {
                largestSeq++;
            }
            return largestSeq;
        }



        private static int FindLongestSeqInCol(string[,] array) 
        {
            int count;
            int largestSeq = 0;
            bool foundMatch = false;
            for (int col = 0; col < array.GetLength(1); col++)
			{
                count = 0;
			    for (int row = 0; row < array.GetLength(0) - 1; row++)
			    {
			        if(array[row,col].Equals(array[row+1,col])) 
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                    }

                    if (count > largestSeq)
                    {
                        foundMatch = true;
                        largestSeq = count;
                        word = array[row, col];
                    }
			    }
			}
            if (foundMatch)
            {
                largestSeq++;
            }
            return largestSeq;
        }
    }
}
