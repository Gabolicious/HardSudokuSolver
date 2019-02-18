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
            int[,] SudokuPuzzle = MakeSudokuPuzzle();
           // writeSudoku(SudokuPuzzle);
         //   Console.WriteLine();
           // Console.WriteLine();
            bool finished = false;
            DateTime StartTime = DateTime.Now;
            while (!finished)
            {
                SudokuPuzzle = UniqueCandidate(SudokuPuzzle);
                SudokuPuzzle = SoleCandidate(SudokuPuzzle);
                finished = true;
                for (int x = 0; x < 9; x++)
                {
                    for (int y = 0; y < 9; y++)
                    {
                        if (SudokuPuzzle[x, y] == 0) finished = false;
                    }
                }
            }
            DateTime endDate = DateTime.Now;
            TimeSpan totalTime = endDate - StartTime;
            Console.WriteLine($"Finished in {totalTime.TotalMilliseconds}ms");
            writeSudoku(SudokuPuzzle);
            Console.ReadLine();
            Console.Clear();
            Main();
        }
        private static int[,] UniqueCandidate (int[,] SudokuPuzzle)
        {
            int solvedPositions = 0;
            //Row
            for (int x = 0; x < 9; x++)
            {
                bool[,] RowCandidates = new bool[9,2] { { false, false }, { false, false }, { false, false }, { false, false }, { false, false }, { false, false }, { false, false }, { false, false }, { false, false } };
                int[,] RowCandidateCoords = new int[9, 2];
                for (int y = 0; y < 9; y++)
                {
                    if (SudokuPuzzle[x, y] == 0)
                    {
                        int[] BlockNumbers = UsedBlockNumbers(SudokuPuzzle, x, y);
                        int[] RowNumbers = UsedRowNumbers(SudokuPuzzle, x);
                        int[] ColumnNumbers = UsedColumnNumbers(SudokuPuzzle, y);

                        for (int i = 1; i < 10; i++)
                        {
                            if (!BlockNumbers.Contains(i) && !RowNumbers.Contains(i) && !ColumnNumbers.Contains(i) && RowCandidates[i - 1, 1])
                            {
                                RowCandidates[i - 1, 0] = false;
                            }
                            else
                            if (!BlockNumbers.Contains(i) && !RowNumbers.Contains(i) && !ColumnNumbers.Contains(i) && !RowCandidates[i - 1, 1])
                            {
                                RowCandidates[i - 1, 0] = true;
                                RowCandidateCoords[i - 1, 0] = x;
                                RowCandidateCoords[i - 1, 1] = y;
                                RowCandidates[i - 1, 1] = true;
                            }
                        }
                        
                    }
                    
                }
                for (int i = 0; i < 9; i++)
                {
                    if (RowCandidates[i, 0])
                    {
                        SudokuPuzzle[RowCandidateCoords[i, 0], RowCandidateCoords[i, 1]] = i + 1;
                        solvedPositions++;
                    }
                }
            }
            //Column
            for (int y = 0; y < 9; y++)
            {
                bool[,] ColumnCandidates = new bool[9, 2] { { false, false }, { false, false }, { false, false }, { false, false }, { false, false }, { false, false }, { false, false }, { false, false }, { false, false } };
                int[,] ColumnCandidatesCoords = new int[9, 2];
                for (int x = 0; x < 9; x++)
                {
                    if (SudokuPuzzle[x, y] == 0)
                    {
                        int[] BlockNumbers = UsedBlockNumbers(SudokuPuzzle, x, y);
                        int[] RowNumbers = UsedRowNumbers(SudokuPuzzle, x);
                        int[] ColumnNumbers = UsedColumnNumbers(SudokuPuzzle, y);

                        for (int i = 1; i < 10; i++)
                        {
                            if (!BlockNumbers.Contains(i) && !RowNumbers.Contains(i) && !ColumnNumbers.Contains(i) && ColumnCandidates[i - 1, 1])
                            {
                                ColumnCandidates[i - 1, 0] = false;
                            }
                            else
                            if (!BlockNumbers.Contains(i) && !RowNumbers.Contains(i) && !ColumnNumbers.Contains(i) && !ColumnCandidates[i - 1, 1])
                            {
                                ColumnCandidates[i - 1, 0] = true;
                                ColumnCandidatesCoords[i - 1, 0] = x;
                                ColumnCandidatesCoords[i - 1, 1] = y;
                                ColumnCandidates[i - 1, 1] = true;
                            }
                        }

                    }

                }
                for (int i = 0; i < 9; i++)
                {
                    if (ColumnCandidates[i, 0])
                    {
                        SudokuPuzzle[ColumnCandidatesCoords[i, 0], ColumnCandidatesCoords[i, 1]] = i + 1;
                        solvedPositions++;
                    }
                }
            }
            //Block
            for (int i = 0; i < 7; i += 3)
            {
                for (int j = 0; j < 7; j += 3)
                {
                    bool[,] BlockCandidates = new bool[9, 2] { { false, false }, { false, false }, { false, false }, { false, false }, { false, false }, { false, false }, { false, false }, { false, false }, { false, false } };
                    int[,] BlockCandidatesCoords = new int[9, 2];
                    for (int x = j; x < j + 3; x++)
                    {
                        for (int y = i; y < i + 3; y++)
                        {

                            if (SudokuPuzzle[x, y] == 0)
                            {
                                int[] BlockNumbers = UsedBlockNumbers(SudokuPuzzle, x, y);
                                int[] RowNumbers = UsedRowNumbers(SudokuPuzzle, x);
                                int[] ColumnNumbers = UsedColumnNumbers(SudokuPuzzle, y);

                                for (int c = 1; c < 10; c++)
                                {
                                    if (!BlockNumbers.Contains(c) && !RowNumbers.Contains(c) && !ColumnNumbers.Contains(c) && BlockCandidates[c - 1, 1])
                                    {
                                        BlockCandidates[c - 1, 0] = false;
                                    }
                                    else
                                    if (!BlockNumbers.Contains(c) && !RowNumbers.Contains(c) && !ColumnNumbers.Contains(c) && !BlockCandidates[c - 1, 1])
                                    {
                                        BlockCandidates[c - 1, 0] = true;
                                        BlockCandidatesCoords[c - 1, 0] = x;
                                        BlockCandidatesCoords[c - 1, 1] = y;
                                        BlockCandidates[c - 1, 1] = true;
                                    }
                                }

                            }
                        }
                    }
                    for (int z = 0; z < 9; z++)
                    {
                        if (BlockCandidates[z, 0])
                        {
                            SudokuPuzzle[BlockCandidatesCoords[z, 0], BlockCandidatesCoords[z, 1]] = z + 1;
                            solvedPositions++;
                        }
                    }
                }
            }
            //Console.WriteLine("Unique Candidates solved " + solvedPositions + " positions.");
            return SudokuPuzzle;
        }
        private static int[,] SoleCandidate (int[,] SudokuPuzzle)
        {
            int SolvedPositions = 0;
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (SudokuPuzzle[x, y] == 0)
                    {
                        int NumberOfOptions = 0;
                        int PositionAnswer = 0;
                        int[] BlockNumbers = UsedBlockNumbers(SudokuPuzzle, x, y);
                        int[] RowNumbers = UsedRowNumbers(SudokuPuzzle, x);
                        int[] ColumnNumbers = UsedColumnNumbers(SudokuPuzzle, y);
                        for (int i = 1; i < 10; i++)
                        {
                            if (!BlockNumbers.Contains(i) && !RowNumbers.Contains(i) && !ColumnNumbers.Contains(i))
                            {
                                NumberOfOptions++;
                                PositionAnswer = i;
                            }
                        }
                        if (NumberOfOptions == 1)
                        {
                            SudokuPuzzle[x, y] = PositionAnswer;
                            SolvedPositions++;
                        }
                    }
                }
            }
           // Console.WriteLine($"Sole Candidate solved {SolvedPositions} positions.");
            return SudokuPuzzle;
        }
        static int[] UsedBlockNumbers(int[,] puzzle, int x, int y)
        {
            int[] section = new int[9];
            int startingX;
            int startingY;
            if (x < 3)
            {
                startingX = 0;
            }
            else if (x < 6)
            {
                startingX = 3;
            }
            else
            {
                startingX = 6;
            }

            if (y < 3)
            {
                startingY = 0;
            }
            else if (y < 6)
            {
                startingY = 3;
            }
            else
            {
                startingY = 6;
            }
            int position = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    section[position] = puzzle[startingX + i, startingY + j];
                    position++;
                }
            }
            return section;
        }
        private static int[] UsedColumnNumbers (int[,] puzzle, int y)
        {
            int[] column = new int[9];
            for (int x = 0; x < 9; x++)
            {
                column[x] = puzzle[x, y];
            }
            return column;
        }
        private static int[] UsedRowNumbers(int[,] puzzle, int x)
        {
            int[] row = new int[9]; 
            for (int i = 0; i < 9; i++)
            {
                row[i] = puzzle[x, i];
            }
            return row;
        }
        private static int[,] MakeSudokuPuzzle ()
        {
            int[,] sudokuPuzzle = new int[9, 9];
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    Console.Write($"Value for ({x + 1}:{y + 1}): ");
                    int answer = Convert.ToInt32(Console.ReadKey().KeyChar);
                    if (answer == 13)
                    {
                        sudokuPuzzle[x, y] = 0;
                    } else
                    {
                        answer -= 48;
                        sudokuPuzzle[x, y] = answer;
                    }
                    Console.WriteLine();
                }
            }
            return sudokuPuzzle;
        }
        private static void writeSudoku(int[,] sudokuPuzzle)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0) Console.WriteLine("------------------------");
                for (int j = 0; j < 9; j++)
                {
                    if (j % 3 == 0) Console.Write("| ");
                    Console.Write(string.Format($"{sudokuPuzzle[i, j]} "));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }
    }
}
