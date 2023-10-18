using System;
using System.Linq;

namespace _02._Wall_Destroyer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int fieldSize = int.Parse(Console.ReadLine());

            char[,] mineField = new char[fieldSize, fieldSize];

            int vankoRowIndex = 0;
            int vankoColIndex = 0;
            int rodsHited = 0;
            int holesDone = 0;

            for (int rows = 0; rows < mineField.GetLength(0); rows++)
            {
                char[] col = Console.ReadLine().ToArray();


                for (int cols = 0; cols < mineField.GetLength(1); cols++)
                {
                    mineField[rows, cols] = col[cols];
                    if (col[cols] == 'V')
                    {
                        vankoRowIndex = rows;
                        vankoColIndex = cols;
                        mineField[rows, cols] = '*';
                        holesDone++;
                    }
                }
            }
            string command = string.Empty;
            bool electocuted = false;
            while (!electocuted && (command = Console.ReadLine()) != "End")
            {
                switch (command)
                {
                    case "up":
                        if (BoundsCheck(vankoRowIndex - 1, vankoColIndex, mineField) )
                        {
                            if (!IsRod(vankoRowIndex - 1, vankoColIndex, mineField))
                            {
                                vankoRowIndex--;
                                (holesDone, electocuted, mineField) = PositionActions(vankoRowIndex, vankoColIndex, mineField, holesDone, electocuted);

                            }
                            else { rodsHited++; Console.WriteLine("Vanko hit a rod!"); }
                        }
                        break;
                    case "down":
                        if (BoundsCheck(vankoRowIndex + 1, vankoColIndex, mineField))
                        {
                            if (!IsRod(vankoRowIndex + 1, vankoColIndex, mineField))
                            {
                                vankoRowIndex++;
                                (holesDone, electocuted, mineField) = PositionActions(vankoRowIndex, vankoColIndex, mineField, holesDone, electocuted);

                            }
                            else { rodsHited++; Console.WriteLine("Vanko hit a rod!"); }
                        }
                        
                        break;
                    case "right":
                        if (BoundsCheck(vankoRowIndex, vankoColIndex + 1, mineField))
                        {
                            if (!IsRod(vankoRowIndex, vankoColIndex + 1, mineField))
                            {
                                 vankoColIndex++;
                                 (holesDone, electocuted, mineField) = PositionActions(vankoRowIndex, vankoColIndex, mineField, holesDone, electocuted);
                            }
                            else { rodsHited++; Console.WriteLine("Vanko hit a rod!"); }
                        }
                        
                        break;
                    case "left":
                        if (BoundsCheck(vankoRowIndex, vankoColIndex - 1, mineField))
                        {
                            if (!IsRod(vankoRowIndex, vankoColIndex - 1, mineField))
                            {
                                vankoColIndex--;
                                (holesDone, electocuted, mineField) = PositionActions(vankoRowIndex, vankoColIndex, mineField, holesDone, electocuted);
                            }
                            else { rodsHited++; Console.WriteLine("Vanko hit a rod!"); }
                        }
                        
                        break;
                }
            }
            if (!electocuted)
            {
                mineField[vankoRowIndex, vankoColIndex] = 'V';
                Console.WriteLine($"Vanko managed to make {holesDone} hole(s) and he hit only {rodsHited} rod(s).");
            }
            else
            {
                Console.WriteLine($"Vanko got electrocuted, but he managed to make {holesDone} hole(s).");
            }
            for (int row = 0; row < mineField.GetLength(0); row++)
            {
                for (int col = col = 0; col < mineField.GetLength(1); col++)
                {
                    Console.Write($"{mineField[row, col]}");
                }
                Console.WriteLine();
            }
        }
        public static Tuple<int, bool, char[,]> PositionActions(int row, int col, char[,] mineField, int holes, bool electrocuted)
        {
            if (mineField[row, col] == '-')
            {
                holes++;
                mineField[row, col] = '*';
            }
            else if (mineField[row, col] == 'C')
            {
                holes++;
                mineField[row, col] = 'E';
                electrocuted = true;
            }
            else if (mineField[row, col] == '*')
            {
                Console.WriteLine($"The wall is already destroyed at position [{row}, {col}]!");
            }
            return Tuple.Create(holes, electrocuted, mineField);


        }
        public static bool IsRod(int rowIndex, int colIndex, char[,] matrix)
        {
            if (matrix[rowIndex,colIndex] == 'R')
            {
                return true;
            }
            return false;

        }

        public static bool BoundsCheck(int rowIndex, int colIndex, char[,] matrix)
        {
            if (rowIndex >= 0 && colIndex >= 0 && rowIndex < matrix.GetLength(0) && colIndex < matrix.GetLength(1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
