using System;
using System.Data;
using System.Drawing;
using System.Linq;

namespace _02._Truffle_Hunter
{
    public class Program
    {
        static void Main(string[] args)
        {
            int fieldSize = int.Parse(Console.ReadLine());

            string[,] field = new string[fieldSize, fieldSize];


            int blackTruffle = 0;
            int summerTruffle = 0;
            int whiteTruffle = 0;
            int boarEatenTruffles = 0;
            for (int rows = 0; rows < field.GetLength(0); rows++)
            {
                string[] col = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);


                for (int cols = 0; cols < field.GetLength(1); cols++)
                {
                    field[rows, cols] = col[cols];
                }
            }

            string command = string.Empty;

            while ((command = Console.ReadLine()) != "Stop the hunt")
            {
                string[] tokens = command
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                int row = int.Parse(tokens[1]);
                int col = int.Parse(tokens[2]);
                if (tokens[0] == "Collect")
                {
                    
                    if (BoundsCheck(row, col, field))
                    {
                        (blackTruffle, summerTruffle, whiteTruffle, field) = PositionActions(row, col, field, blackTruffle, summerTruffle, whiteTruffle);
                    }
                }
                else if (tokens[0] == "Wild_Boar")
                {
                   
                    string direction = tokens[3];

                    switch (direction)
                    {
                        case "up":
                            for (int rows = row; rows >= 0; rows-=2)
                            {
                                (boarEatenTruffles, field) = WildBoar(boarEatenTruffles, field, rows, col);
                            }
                            break;
                        case "down":
                            for (int rows = row; rows < field.GetLength(0); rows += 2)
                            {
                                (boarEatenTruffles, field) = WildBoar(boarEatenTruffles, field, rows, col);
                            }
                            break;
                        case "left":
                            for (int cols = col; cols >= 0; cols -= 2)
                            {
                                (boarEatenTruffles, field) = WildBoar(boarEatenTruffles, field, row, cols);
                            }
                            break;
                        case "right":
                            for (int cols = col; cols < field.GetLength(0); cols += 2)
                            {
                                (boarEatenTruffles, field) = WildBoar(boarEatenTruffles, field, row, cols);
                            }
                            break;
                    }
                }
            }
            Console.WriteLine($"Peter manages to harvest {blackTruffle} black, {summerTruffle} summer, and {whiteTruffle} white truffles.");
            Console.WriteLine($"The wild boar has eaten {boarEatenTruffles} truffles.");
            for (int row = 0; row < field.GetLength(0); row++)
            {
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    Console.Write($"{field[row, col]} ");
                }
                Console.WriteLine();
            }
        }

        public static Tuple<int, string[,]> WildBoar(int trufflesEaten, string[,] field, int row, int col)
        {
            if (field[row, col] == "B" || field[row, col] == "S" || field[row, col] == "W")
            {
                trufflesEaten++;
                field[row, col] = "-";
            }

            return Tuple.Create(trufflesEaten, field);
        }

        public static Tuple<int, int, int, string[,]> 
        PositionActions(int row, int col, string[,] field, int blackTruffle, int summerTruffle , int whiteTruffle)
         {
        if (field[row, col] == "B")
        {
            blackTruffle++;
            field[row, col] = "-";
        }
        else if (field[row, col] == "S")
        {
            summerTruffle++;
            field[row, col] = "-";
        }
        else if (field[row, col] == "W")
        {
            whiteTruffle++;
            field[row, col] = "-";
        }
        return Tuple.Create(blackTruffle, summerTruffle, whiteTruffle, field);
         }
        public static bool BoundsCheck(int rowIndex, int colIndex, string[,] matrix)
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
