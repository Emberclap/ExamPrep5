using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Tiles_Master
{
    internal class Program
    {
        static void Main()
        {
            int[] whiteTilesSequence = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToArray();
            int[] greyTilesSequence = Console.ReadLine()
                 .Split(" ")
                 .Select(int.Parse)
                 .ToArray();
            Queue<int> greyTiles = new Queue<int>(greyTilesSequence);
            Stack<int> whiteTiles = new Stack<int>(whiteTilesSequence);
            Dictionary<string, int> areas = new Dictionary<string, int>
            {
                { "Sink", 0 },
                { "Oven", 0 },
                { "Countertop", 0},
                { "Wall", 0 },
                { "Floor", 0 }
            };

            while (greyTiles.Any() && whiteTiles.Any())
            {
                if (greyTiles.Peek().Equals(whiteTiles.Peek()))
                {
                    int largerTile = greyTiles.Peek() + whiteTiles.Peek();
                    greyTiles.Dequeue();
                    whiteTiles.Pop();
                    if (largerTile == 40)
                    {
                        areas["Sink"]++;
                    }
                    else if (largerTile == 50)
                    {
                        areas["Oven"]++;

                    }
                    else if (largerTile == 60)
                    {
                        areas["Countertop"]++;

                    }
                    else if (largerTile == 70)
                    {
                        areas["Wall"]++;

                    }
                    else
                    {
                        areas["Floor"]++;
                    }
                }
                else
                {
                    int tempWhiteTile = whiteTiles.Pop() / 2;
                    whiteTiles.Push(tempWhiteTile);
                    int tempGreyTile = greyTiles.Dequeue();
                    greyTiles.Enqueue(tempGreyTile);
                }
            }
            if (whiteTiles.Any())
            {
                Console.WriteLine($"White tiles left: {string.Join(", ", whiteTiles)}");
            }
            else
            {
                Console.WriteLine("White tiles left: none");
            }
            if (greyTiles.Any())
            {
                Console.WriteLine($"Grey tiles left: {string.Join(", ", greyTiles)}");
            }
            else
            {
                Console.WriteLine("Grey tiles left: none");
            }
            foreach ( var area in areas.OrderByDescending(x=> x.Value).ThenBy(y => y.Key))
            {
                if(area.Value > 0)
                {
                    Console.WriteLine($"{area.Key}: {area.Value}");
                }
            }
        }
    }
}
