﻿using AoC.Common;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Aoc.AoC2019.Problems.Day24
{
    public class Day24_Solution : AoCSolution<long>
    {
        public override int Year => 2019;

        public override int Day => 24;

        public override string Name => "Day 24: Planet of Discord";

        public override string InputFileName => "Day24.txt";

        public override IEnumerable<long> Solve(IEnumerable<string> data)
        {
            yield return FindReoccuringState(data);
        //    yield return RecusiveGame_CountBugs(Example1, 10);

            yield return RecusiveGame_CountBugs(data, 200);
        }              

        public long FindReoccuringState(IEnumerable<string> data)
        {
            HashSet<string> previousStates = new HashSet<string>();

            GameOfLifeMap game = new GameOfLifeMap(data.ToList());
            var initialState = game.DrawMap();
            previousStates.Add(initialState);

            while(true)
            {
                game.EvolveMap();
                string state = game.DrawMap();

                if (previousStates.Contains(state))
                {
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
