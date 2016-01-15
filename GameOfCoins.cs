using System;
using System.Text.RegularExpressions;


namespace Homework
{
    class GameOfCoins
    {
        /* (You receive the layout of a board from the console.)(skipped) Assume it will always have 4 rows which you'll get as strings, each on a separate line.
             * Each character in the strings will represent a cell on the board. Note that the strings may be of different length. 
             * You are the player and start at the top-left corner (that would be position [0, 0] on the board). On the fifth line of input you'll receive 
             * a string with movement commands which tell you where to go next, it will contain only these four characters – '>' (move right), '<' (move left),
             * '^' (move up) and 'v' (move down). You need to keep track of two types of events – collecting coins (represented by the symbol '$', of course) 
             * and hitting the walls of the board (when the player tries to move off the board to invalid coordinates). When all moves are over, print the amount
             * of money collected and the number of walls hit.
             */
        private static int row = 0;
        private static int col = 0;
        private static int hitAWallCounter = 0;
        private static int foundedCoins = 0;
        private static char[][] arrayOfStrings = {
                                            new char[] {'s','$','$','d','d'},
                                            new char[] {'d','d','d','$','d','d','d'},
                                            new char[] {'d','d','d','d','d','d','$'},
                                            new char[] {'d','$','d','d','d','d',},
                                            new char[] {'d','d','d','$'},
                                            new char[] {'d','d','d','d','d','$','$'},
                                        };
        public static void CollectTheCoins()
        {
            char[] directions = GetInput().ToCharArray();
            Console.WriteLine("Starting form (0,0)!");
            for (int index = 0; index < directions.Length; index++)
            {
                char nextMove = directions[index];
                if (nextMove == '<')
                {
                    MoveLeft();
                }
                else if (nextMove == '>')
                {
                    MoveRight();
                } 
                else if (nextMove == 'v')
                {
                    MoveDown();
                } 
                else if (nextMove == '^')
                {
                    MoveUp();
                }
            }
            Console.WriteLine("Good game! You have collected {0} coins and hited {1} walls!", foundedCoins, hitAWallCounter);
        }

        private static string GetInput()
        {
            bool isTrue = true;
            string directions = "";
            Console.WriteLine("Please enter a string! Only four characters are allowed:\r\n '>' (move right), '<' (move left), '^' (move up) and 'v' (move down).");
            while (isTrue)
            {
                directions = Console.ReadLine();
                Regex reg = new Regex(@"([v<>^])$");
                if (!reg.IsMatch(directions))
                {
                    Console.WriteLine("You have typed unallowed characters! Please try again!");
                }
                else
                {
                    return directions;
                }
            }
            return directions;
        }

        private static void MoveLeft()
        {
            col--;
            if (col < 0)
            {
                Console.WriteLine("You tried to go left but you hit a wall!");
                col++;
                hitAWallCounter++;
                return;
            }
            else if (arrayOfStrings[row][col] == '$')
            {
                arrayOfStrings[row][col] = 'd';
                Console.WriteLine("Hurrraayy you have found a coin!");
                foundedCoins++;
                return;
            }
            Console.Write("You go left! ");
            Console.WriteLine();
        }

        private static void MoveRight()
        {      
            col++;
            if (arrayOfStrings[row].Length == col)
            {
                Console.WriteLine("You tried to go right but you hit a wall!");
                col--;
                hitAWallCounter++;
                return;
            }
            else if (arrayOfStrings[row][col] == '$')
            {
                arrayOfStrings[row][col] = 'd';
                Console.WriteLine("Hurrraayy you have found a coin!");
                foundedCoins++;
                return;
            }
            Console.Write("You go right! ");
            Console.WriteLine();
        }

        private static void MoveUp()
        {           
            row--;
            if ((row < 0) || (arrayOfStrings[row].Length < arrayOfStrings[row + 1].Length))
            {
                Console.WriteLine("You tried to go up but you hit a wall!");
                row++;
                hitAWallCounter++;
                return;
            }
            else if (arrayOfStrings[row][col] == '$')
            {
                arrayOfStrings[row][col] = 'd';
                Console.WriteLine("Hurrraayy you have found a coin!");
                foundedCoins++;
                return;
            }
            Console.Write("You go up! ");
            Console.WriteLine();
        }

        private static void MoveDown()
        {           
            row++;
            if ((arrayOfStrings.Length == row) || (arrayOfStrings[row].Length < arrayOfStrings[row-1].Length))
            {
                Console.WriteLine("You tried to go down but you hit a wall!");
                row--;
                hitAWallCounter++;
                return;
            }
            else if (arrayOfStrings[row][col] == '$')
            {
                arrayOfStrings[row][col] = 'd';
                Console.WriteLine("Hurrraayy you have found a coin!");
                foundedCoins++;
                return;
            }
            Console.Write("You go down! ");
            Console.WriteLine();
        }
    }
}
