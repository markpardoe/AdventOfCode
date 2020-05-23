using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;

namespace Aoc.AoC2019.Problems.Day24
{
    public class Day24_Solution : ISolution
    {
        public string URL => @"https://adventofcode.com/2019/day/24";

        public int Year => 2019;

        public int Day => 24;

        public string Name => "Day 24: Planet of Discord";

        public string InputFileName => "Day24.txt";

        public IEnumerable<string> Solve(IEnumerable<string> data)
        {
            yield return FindReoccuringState(data).ToString();
            yield return RecusiveGame_CountBugs(data, 200).ToString();
        }              

        public long FindReoccuringState(IEnumerable<string> data)
        {
            HashSet<string> previousStates = new HashSet<string>();

            GameOfLifeMap game = new GameOfLifeMap(data.ToList());
            previousStates.Add(game.DrawMap());
         //   Console.WriteLine(game.DrawMap());
          //  Console.WriteLine();

            while(true)
            {
                game.EvolveMap();
                string state = game.DrawMap();

                if (previousStates.Contains(state))
                {
                   // Console.WriteLine(game.DrawMap());
                    return game.GetBioDiversityScore();
                }
                else
                {
                    previousStates.Add(state);
                }
            }
        }

        public long RecusiveGame_CountBugs(IEnumerable<string> data, int numGenerations)
        {
            RecursiveGameOfLifeMap game = new RecursiveGameOfLifeMap(data.ToList());
            //Console.WriteLine(game.DrawMap());

            for (int i = 0; i < numGenerations; i++)
            {
               game.EvolveMap();
            }

            return game.TotalBugs();
        }


        private static readonly List<string> Example1 = new List<string>()
        {
            "....#",
            "#..#.",
            "#..##",
            "..#..",
            "#...."
        };
    }
}
