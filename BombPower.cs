using System;
using System.Text;


namespace Homework
{
    class BombPower
    {
        /* On de_dust2 terrorists have planted a bomb (or possibly several of them)! Write a program that sets those bombs off! 
         * A bomb is a string in the format |...|. When set off, the bomb destroys all characters inside. The bomb should also
         * destroy n characters to the left and right of the bomb. n is determined by the bomb power (the last digit of the ASCII
         * sum of the characters inside the bomb). Destroyed characters should be replaced by '.' (dot). For example, we are given
         * the following text:
                prepare|yo|dong
           The bomb is |yo|. We get the bomb power by calculating the last digit of the sum: y (121) + o (111) = 232. The bomb 
         * explodes and destroys itself and 2 characters to the left and 2 characters to the right. The result is:
                prepa........ng
         */

        public static void Bombing()
        {
            char[] input = GetInput();
            DetonateBombs(input);
            PrintResult(input);
        }

        private static char[] GetInput()
        {
            while (true)
            {
                Console.WriteLine("Please enter a string:");
                char[] input = Console.ReadLine().ToCharArray();
                if (CheckIfThereAreABombs(input))
                {
                    return input;
                }
            }
        }

        private static bool CheckIfThereAreABombs(char[] input)
        {
            int count = 0;
            int bombsCount = 0;
            for (int index = 0; index < input.Length; index++)
            {
                if (input[index] == '|')
                {
                    count++;
                    for (int i = index + 2; i < input.Length; i++)
                    {
                        if (input[i] == '|')
                        {
                            count++;
                            bombsCount++;
                            index = i + 1;
                            break;
                        }
                    }
                }
            }
            Console.WriteLine(bombsCount + " " + count);
            if (bombsCount != 0 && count % 2 == 0)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Invalid bomb(s)!");
                return false;
            }
        }
        
        private static void DetonateBombs(char[] input) 
        {
            int bombPower;
            for (int index = 0; index < input.Length; index++)
            {
                if (input[index] == '|')
                {
                    for (int i = index + 1; i < input.Length; i++)
                    {
                        if (input[i] == '|')
                        {
                            bombPower = FindPowerOfTheBomb(input, index + 1, i-1);
                            DamageDone(input, index - bombPower, i + 1 + bombPower, i + 1);
                            index = i;
                            break;
                        }
                    }
                }
            }
        }

        private static int FindPowerOfTheBomb(char[] input, int start, int end)
        {
            string temp = "";
            int power = 0;
            for (int index = start; index <= end; index++)
            {
                temp += input[index];      
            }
            byte[] powerArray = Encoding.ASCII.GetBytes(temp);
            foreach (var item in powerArray)
            {
                power += item; 
            }
            return power % 10;
        }

        private static void DamageDone(char[] input, int start, int end, int i) {
            if (start < 0)
            {
                start = 0;
            }
            if (end > input.Length)
            {
                end = input.Length;
            }
            for (int index = i; index < end; index++)
			{
			    if(input[index] == '|'){
                    end = index;
                }
			}     
            for (int index = start; index < end; index++)
            {
                input[index] = '.';
            }
        }

        private static void PrintResult(char[] input)
        {
            Console.WriteLine("Here is what is left of the bombing site!");
            foreach (var item in input)
            {
                Console.Write("{0}", item);
            }
            Console.WriteLine();
        }
    }
}
