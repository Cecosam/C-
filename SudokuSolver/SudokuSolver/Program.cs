using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class Program
    {
        static void Main(string[] args)
        {
            var sudokuToSolve = new int[,]
            {
                { 6,1,0,8,0,0,0,0,4},
                { 0,0,4,0,0,0,0,0,5},
                { 0,0,5,0,0,0,0,2,0},
                { 0,0,0,0,9,0,0,0,7},
                { 0,5,0,3,0,4,0,1,0},
                { 9,0,0,0,8,0,0,0,0},
                { 0,9,2,0,0,0,8,0,0},
                { 8,0,0,0,0,0,2,0,0},
                { 4,0,0,0,0,8,0,9,1}
            };
            SudokuSolver sudoku = new SudokuSolver();
            sudoku.GetSudoku(sudokuToSolve);
            sudoku.SolveSudoku();
            sudoku.PrintSolvedSudoku();
        }
    }
}
