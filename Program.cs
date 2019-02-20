using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_2
{
    class Program
    {
        private static void Main()
        {
            int[,] test = new int[9, 9] {
            {0,0,0,   0,0,0,   0,0,0},
            {0,0,0,   0,0,0,   0,0,0},
            {0,0,0,   0,0,0,   0,0,0},

            {0,0,0,   0,0,0,   0,0,0},
            {0,0,0,   0,0,0,   0,0,0},
            {0,0,0,   0,0,0,   0,0,0},

            {0,0,0,   0,0,0,   0,0,0},
            {0,0,0,   0,0,0,   0,0,0},
            {0,0,0,   0,0,0,   0,0,0},
            };
            int[,] BlockBlock = new int[9, 9] {
            {0,0,0,   0,0,0,   0,0,0},
            {0,0,8,   0,0,0,   0,0,0},
            {0,0,0,   0,0,0,   0,0,0},

            {0,0,0,   0,0,0,   0,0,0},
            {2,9,0,   0,1,4,   0,0,0},
            {0,0,0,   0,0,0,   0,0,0},

            {0,0,0,   0,0,0,   0,0,0},
            {0,0,0,   8,0,0,   0,0,0},
            {0,0,0,   0,0,0,   0,0,0},
            };
            int[,] easyPuzzle = new int[9, 9] {
            {0,8,0,   0,2,9,   1,7,0},
            {0,0,3,   1,5,0,   0,8,0},
            {2,0,1,   0,0,4,   0,0,0},

            {0,1,0,   5,3,6,   7,0,9},
            {7,0,4,   0,0,8,   0,6,0},
            {6,3,9,   0,4,0,   8,5,2},

            {0,0,0,   6,0,2,   5,0,0},
            {3,9,0,   4,1,0,   0,2,0},
            {0,2,0,   0,0,0,   0,1,7},
            };
            int[,] normalPuzzle = new int[9, 9]
            {
                {2,0,0,   9,0,0,   0,0,0},
                {0,8,0,   5,0,2,   1,9,0},
                {0,0,0,   0,8,0,   3,0,0},

                {0,0,0,   0,0,0,   4,7,0},
                {4,0,6,   3,0,1,   8,5,9},
                {0,0,3,   8,4,0,   2,6,0},

                {0,0,0,   0,5,0,   7,0,4},
                {0,0,8,   0,0,6,   0,0,0},
                {5,1,0,   4,7,0,   0,0,0},
            };
            //Sudoku Sudoku = new Sudoku(BlockBlock);
            Sudoku Sudoku = new Sudoku();
            Sudoku.Write();
            DateTime StartTime = DateTime.Now;
            do
            {
                int originalNumberOfZeros = Sudoku.numberOfZeros;
                Sudoku.SoleCandidate();
                if (Sudoku.numberOfZeros > 0) Sudoku.UniqueCandidate();

                if (originalNumberOfZeros == Sudoku.numberOfZeros)
                {
                    Sudoku.BlockColumnRowInteraction();
                }
            } while (Sudoku.numberOfZeros != 0);
            Console.WriteLine($"Finished in {(DateTime.Now - StartTime).TotalMilliseconds}ms");
            Sudoku.Write();
            Console.ReadLine();
            Console.Clear();
            Main();
        }
        
    }
}
