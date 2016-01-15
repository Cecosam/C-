using System;


namespace Homework
{
    class Sort
    {
        /* Write a program to read an array of numbers from the console, sort them and print them back on the console.
         * The numbers should be entered from the console on a single line, separated by a space.
         */
        public static void SimpleSort()
        {
            Console.WriteLine("Please enter the numbers with space between them: ");
            string temp = Console.ReadLine();
            int[] numbers = new int[temp.Split(' ').Length];
            for (int index = 0; index < numbers.Length; index++)
            {
                try
                {
                    numbers[index] = Int32.Parse(temp.Split(' ')[index]);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }

            Array.Sort(numbers);

            Console.WriteLine("Sorted array:");
            for (int index = 0; index < numbers.Length; index++)
            {
                if ((index + 1) == numbers.Length)
                {
                    Console.WriteLine("{0}!", numbers[index]);
                    break;
                }
                Console.Write("{0},", numbers[index]);
            }
        }


        /* Write a program to sort an array of numbers and then print them back on the console.
         * The numbers should be entered from the console on a single line, separated by a space.
         * Do not use the built-in sorting method, you should write your own. Use the selection sort algorithm. 
         */
        public static void SelectionSort()
        {
            Console.WriteLine("Please enter the numbers with space between them: ");
            string temp = Console.ReadLine();
            int[] numbers = new int[temp.Split(' ').Length];
            for (int index = 0; index < numbers.Length; index++)
            {
                try
                {
                    numbers[index] = Int32.Parse(temp.Split(' ')[index]);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }

            for (int index = 0; index < numbers.Length; index++)
            {
                for (int index2 = index + 1; index2 < numbers.Length; index2++)
                {
                    if (numbers[index2] < numbers[index])
                    {
                        temp = numbers[index2].ToString();
                        numbers[index2] = numbers[index];
                        numbers[index] = Int32.Parse(temp);
                    }
                }
            }

            Console.WriteLine("Sorted array:");
            for (int index = 0; index < numbers.Length; index++)
            {
                if ((index + 1) == numbers.Length)
                {
                    Console.WriteLine("{0}!", numbers[index]);
                    break;
                }
                Console.Write("{0},", numbers[index]);
            }

        }
    }
}
