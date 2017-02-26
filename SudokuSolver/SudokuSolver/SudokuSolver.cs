using System;

namespace SudokuSolver
{
    public class SudokuSolver
    {
        private int[,] sudoku;
        private bool[,] fixedSudokuNumbers;
        private bool isSolved;

        public SudokuSolver()
        {
            this.sudoku = null;
            this.fixedSudokuNumbers = null;
        }

        public void GetSudoku(int[,] sudokuToSolve)
        {
            if (!CheckIfSudokuIsValid(sudokuToSolve))
            {
                Console.WriteLine("Invalid sudkou!");
                return;
            }
            Console.WriteLine("Valid sudoku inserted!");
        }

        public void PrintSolvedSudoku()
        {
            if (this.sudoku == null)
            {
                Console.WriteLine("There is no sudoku to print!");
                return;
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (j == 3 || j == 6)
                    {
                        Console.Write("| ");
                    }
                    Console.Write("{0} ", this.sudoku[i,j]);
                }

                Console.WriteLine();
                if (i == 2 || i == 5)
                {
                    Console.WriteLine(new String('-', 22));
                }
            }
        }
        public void SolveSudoku()
        {
            if (this.sudoku == null)
            {
                Console.WriteLine("Please first insert sudoku!");
                return;
            }

            this.isSolved = false;

            for (int i = 1; i < 10; i++)
            {
                if (this.isSolved == true)
                {
                    break;
                }
                SolveSudoku(0, 0, i);
            }
        }

        public void SolveSudoku(int row, int col, int number)
        {
            if (col == 9)
            {
                col = 0;
                row++;
                if (row == 9)
                {
                    this.isSolved = true;
                    return;
                }
            }

            if (this.fixedSudokuNumbers[row, col] == true)
            {
                SolveSudoku(row, col + 1, number);
                return;
            }

            if (CheckIfNumberIsInSubBox(row, col, number) || CheckIfSameNumberIsInColumn(row, col, number) || CheckIfSameNumberIsInRow(row, col, number))
            {
                return;
            }

            this.sudoku[row, col] = number;

            if (row == 8 && col == 8)
            {
                this.isSolved = true;
                return;
            }

            for (int i = 1; i < 10; i++)
            {
                SolveSudoku(row, col + 1, i);
                if (this.isSolved == true)
                {
                    return;
                }
            }           

            if (this.fixedSudokuNumbers[row,col] == false)
            {
                this.sudoku[row, col] = 0;
            }
        }
        private bool CheckIfSudokuIsValid(int[,] sudokuToSolve)
        {
            if (sudokuToSolve.GetLength(0) != 9 || sudokuToSolve.GetLength(1) != 9)
            {
                return false;
            }

            int minimumOfSudokuClues = 17;
            int countOfClues = 0;

            this.sudoku = new int[9, 9];
            this.fixedSudokuNumbers = new bool[9, 9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudokuToSolve[i, j] != 0)
                    {
                        this.sudoku[i, j] = sudokuToSolve[i, j];
                        this.fixedSudokuNumbers[i, j] = true;
                        countOfClues++;
                    }
                }
            }

            if (countOfClues < minimumOfSudokuClues)
            {
                this.sudoku = null;
                this.fixedSudokuNumbers = null;
                return false;
            }

            if (!ValidateSudokuNumbers())
            {
                this.sudoku = null;
                this.fixedSudokuNumbers = null;
                return false;
            }
            return true;
        }

        private bool ValidateSudokuNumbers()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (this.sudoku[i, j] == 0)
                    {
                        continue;
                    }
                    if (CheckIfNumberIsInSubBox(i, j, sudoku[i, j]) || CheckIfSameNumberIsInColumn(i, j, sudoku[i, j]) || CheckIfSameNumberIsInRow(i, j, sudoku[i, j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool CheckIfNumberIsInSubBox(int row, int col, int number)
        {
            int startRow = 0;
            int startCol = 0;

            if (row > 5)
            {
                startRow = 6;
            }
            else if (row > 2)
            {
                startRow = 3;
            }

            if (col > 5)
            {
                startCol = 6;
            }
            else if (col > 2)
            {
                startCol = 3;
            }

            for (int i = startRow; i < startRow + 3; i++)
            {
                for (int j = startCol; j < startCol + 3; j++)
                {
                    if (i == row && j == col)
                    {
                        continue;
                    }
                    if (this.sudoku[i,j] == number)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckIfSameNumberIsInRow(int row, int col, int number)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i == col)
                {
                    continue;
                }
                else if (this.sudoku[row,i] == number)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckIfSameNumberIsInColumn(int row, int col, int number)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i == row)
                {
                    continue;
                }
                else if (this.sudoku[i, col] == number)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
