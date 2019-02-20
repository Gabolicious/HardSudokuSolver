using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_2
{
    class Sudoku
    {
        public int[,] Puzzle;
        public int numberOfZeros = 0;
        private bool[,,] Candidates = new bool[9, 9, 10];
        public Sudoku(int[,] newPuzzle)
        {
            Puzzle = newPuzzle;
            for (int x = 0; x < Puzzle.GetLength(0); x++)
            {
                int[] InvalidRowNumbers = UsedRowNumbers(x);
                for (int y = 0; y < Puzzle.GetLength(0); y++)
                {
                    if (Puzzle[x, y] == 0) numberOfZeros++;
                    else Candidates[x, y, 0] = true;
                    if (!Candidates[x, y, 0])
                    {
                        int[] InvalidColumnNumbers = UsedColumnNumbers(y);
                        int[] InvalidBlockNumbers = UsedBlockNumbers(x, y);
                        for (int i = 1; i < 10; i++)
                        {
                            if (!InvalidBlockNumbers.Contains(i) && !InvalidColumnNumbers.Contains(i) && !InvalidRowNumbers.Contains(i))
                            {
                                Candidates[x, y, i] = true;
                            }
                        }
                    }
                }
            }
        }
        public Sudoku()
        {
            Puzzle = new int[9, 9];
            for (int x = 0; x < Puzzle.GetLength(0); x++)
            {
                for (int y = 0; y < Puzzle.GetLength(0); y++)
                {
                    Console.Write($"Value for ({x + 1}:{y + 1}): ");
                    int answer = Convert.ToInt32(Console.ReadKey().KeyChar);
                    if (answer == 13 || answer - 48 == 0)
                    {
                        Puzzle[x, y] = 0;
                        numberOfZeros++;
                    }
                    else if (answer == 8)
                    {
                        if (y == 0)
                        {
                            x--;
                            y = 7;
                        } else
                        {
                            y -= 2;
                        }
                    }
                    else
                    {
                        answer -= 48;
                        Puzzle[x, y] = answer;
                        Candidates[x, y, 0] = true; //solved
                    }
                    Console.WriteLine();
                }
            }
            for(int x = 0; x < Puzzle.GetLength(0); x++)
            {
                int[] InvalidRowNumbers = UsedRowNumbers(x);
                for (int y = 0; y < Puzzle.GetLength(0); y++)
                {
                    if (!Candidates[x, y, 0])
                    {
                        int[] InvalidColumnNumbers = UsedColumnNumbers(y);
                        int[] InvalidBlockNumbers = UsedBlockNumbers(x, y);
                        for (int i = 1; i < 10; i++)
                        {
                            if (!InvalidBlockNumbers.Contains(i) && !InvalidColumnNumbers.Contains(i) && !InvalidRowNumbers.Contains(i))
                            {
                                Candidates[x, y, i] = true;
                            }
                        }
                    }
                }
            }
        }
        private void InsertAnswer(int x, int y, int number)
        {
            Puzzle[x, y] = number;
            Candidates[x, y, 0] = true;
            numberOfZeros--;

            int startingX = 0;
            int startingY = 0;
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

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Candidates[startingX + i, startingY + j, number] = false;
                }
            }

            for (int x2 = 0; x2 < Puzzle.GetLength(0); x2++)
            {
                Candidates[x2, y, number] = false;
            }
            for (int y2 = 0; y2 < Puzzle.GetLength(0); y2++)
            {
                Candidates[x, y2, number] = false;
            }
        }
        public void SoleCandidate()
        {
            int SolvedPositions = 0;
            for (int x = 0; x < Puzzle.GetLength(0); x++)
            {
                for (int y = 0; y < Puzzle.GetLength(0); y++)
                {
                    if (!Candidates[x, y, 0])
                    {
                        int NumberOfOptions = 0;
                        int PositionAnswer = 0;
                        for (int i = 1; i < 10; i++)
                        {
                            if (Candidates[x, y, i])
                            {
                                NumberOfOptions++;
                                if (NumberOfOptions > 1) break;
                                PositionAnswer = i;
                            }
                        }
                        if (NumberOfOptions == 1)
                        {
                            InsertAnswer(x, y, PositionAnswer);
                            SolvedPositions++;
                        }
                    }
                }
            }
            Console.WriteLine($"Sole candidate solved {SolvedPositions} positions.");
        }
        public void BlockColumnRowInteraction()
        {
            //By Row
            for (int i = 0; i < Puzzle.GetLength(0) - 2; i += 3)
            {
                for (int j = 0; j < Puzzle.GetLength(0) - 2; j += 3)
                {
                    for (int c = 1; c < 10; c++)
                    {
                        
                        int numberOfRows = 0;
                        int row = 0;
                        for (int x = i; x < i + 3; x++)
                        {
                            bool foundOneForRow = false;
                            for (int y = j; y < j + 3; y++)
                            {
                                if (!Candidates[x, y, 0])
                                {
                                    if (Candidates[x, y, c])
                                    {
                                        if (!foundOneForRow)
                                    {
                                        numberOfRows++;
                                        row = x;
                                    }
                                         
                                    }
                                }
                            }
                            if (numberOfRows > 1) break;
                        }
                        if (numberOfRows == 1)
                        {
                            for (int y = 0; y < Puzzle.GetLength(0); y++)
                            {
                                if (y < j || y > j + 2)
                                {
                                    Candidates[row, y, c] = false;
                                    Console.WriteLine($"Changed ({row + 1}, {y + 1}) for number {c} to false.");
                                }
                            }
                        }
                    }
                }
            }
            
            //By Column
            for (int i = 0; i < Puzzle.GetLength(0) - 2; i += 3)
            {
                for (int j = 0; j < Puzzle.GetLength(0) - 2; j += 3)
                {
                    for (int c = 1; c < 10; c++)
                    {

                        int numberOfRows = 0;
                        int column = 0;
                        for (int y = i; y < i + 3; y++)
                        {
                            bool foundOneForRow = false;
                            for (int x = j; x < j + 3; x++)
                            {
                                if (!Candidates[x, y, 0])
                                {
                                    if (Candidates[x, y, c])
                                    {
                                        if (!foundOneForRow)
                                        {
                                            numberOfRows++;
                                            column = y;
                                        }

                                    }
                                }
                            }
                            if (numberOfRows > 1) break;
                        }
                        if (numberOfRows == 1)
                        {
                            for (int x = 0; x < Puzzle.GetLength(0); x++)
                            {
                                if (x < j || x > j + 2)
                                {
                                    Candidates[x, column, c] = false;
                                    Console.WriteLine($"Changed ({x + 1}, {column + 1}) for number {c} to false.");
                                }
                            }
                        }
                    }
                }
            }
        }
        
        public void UniqueCandidate()
        {
            int solvedPositions = 0;
            //Row
            for (int x = 0; x < Puzzle.GetLength(0); x++)
            {
                for (int i = 1; i < 10; i++)
                {
                    int numberOfCandidates = 0;
                    int[] position = new int[2];
                    for (int y = 0; y < Puzzle.GetLength(0); y++)
                    {
                        if (!Candidates[x, y, 0])
                        {
                            if (Candidates[x, y, i])
                            {
                                numberOfCandidates++;
                                position[0] = x;
                                position[1] = y;
                            }
                            if (numberOfCandidates > 1) break;
                        }

                    }
                    if (numberOfCandidates == 1)
                    {
                        InsertAnswer(position[0], position[1], i);
                        solvedPositions++;
                        

                    }
                }
                
                
            }
            //Column
            for (int y = 0; y < Puzzle.GetLength(0); y++)
            {
                for (int i = 1; i < 10; i++)
                {
                    int numberOfCandidates = 0;
                    int[] position = new int[2];
                    for (int x = 0; x < Puzzle.GetLength(0); x++)
                    {
                        if (!Candidates[x, y, 0])
                        {
                            if (Candidates[x, y, i])
                            {
                                numberOfCandidates++;
                                position[0] = x;
                                position[1] = y;
                            }
                            if (numberOfCandidates > 1) break;
                        }

                    }
                    if (numberOfCandidates == 1)
                    {
                        InsertAnswer(position[0], position[1], i);
                        solvedPositions++;
                        
                    }
                }


            }
            //Block
            for (int i = 0; i < Puzzle.GetLength(0) - 2; i += 3)
            {
                for (int j = 0; j < Puzzle.GetLength(0) - 2; j += 3)
                {
                    for (int c = 1; c < 10; c++)
                    {
                        int numberOfCandidates = 0;
                        int[] position = new int[2];
                        for (int x = j; x < j + 3; x++)
                        {
                            for (int y = i; y < i + 3; y++)
                            {
                                if (!Candidates[x, y, 0])
                                {
                                    if (Candidates[x, y, c])
                                    {
                                        numberOfCandidates++;
                                        position[0] = x;
                                        position[1] = y;
                                    }
                                    if (numberOfCandidates > 1) break;

                                }
                            }
                        }
                        if (numberOfCandidates == 1)
                        {
                            InsertAnswer(position[0], position[1], c);
                            solvedPositions++;
                        }
                    }
                }
            }
            Console.WriteLine($"Unique solved {solvedPositions} positions");
        }
        private int[] UsedBlockNumbers(int x, int y)
        {
            int[] section = new int[Puzzle.GetLength(0)];
            int startingX = 0;
            int startingY = 0;
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
                    section[position] = Puzzle[startingX + i, startingY + j];
                    position++;
                }
            }
            return section;
        }
        private int[] UsedColumnNumbers(int y)
        {
            int[] column = new int[Puzzle.GetLength(0)];
            for (int x = 0; x < Puzzle.GetLength(0); x++)
            {
                column[x] = Puzzle[x, y];
            }
            return column;
        }
        private int[] UsedRowNumbers(int x)
        {
            int[] row = new int[Puzzle.GetLength(0)];
            for (int i = 0; i < Puzzle.GetLength(0); i++)
            {
                row[i] = Puzzle[x, i];
            }
            return row;
        }
        public void Write()
        {
            for (int i = 0; i < Puzzle.GetLength(0); i++)
            {
                if (i % 3 == 0) Console.WriteLine("------------------------");
                for (int j = 0; j < Puzzle.GetLength(0); j++)
                {
                    if (j % 3 == 0) Console.Write("| ");
                    Console.Write(string.Format($"{Puzzle[i, j]} "));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }
    }
}
