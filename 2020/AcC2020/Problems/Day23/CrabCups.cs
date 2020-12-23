using System;
using System.Collections.Generic;
using System.Linq;
using AoC.Common;

namespace AoC.AoC2020.Problems.Day23
{
    public class CrabCups :AoCSolution<long>
    {
        public override int Year => 2020;
        public override int Day => 23;
        public override string Name => "Day 23: Crab Cups";
        public override string InputFileName => null;
        

        public override IEnumerable<long> Solve(IEnumerable<string> input)
        {
            string gameInput = puzzleInput;

            yield return RunGame(gameInput, gameInput.Length, 100).CupOrderValue();
            yield return RunGame(gameInput, 1000000, 10000000).GetCalculatedValue();
        }

        private readonly string puzzleInput = "253149867";

        private CrabGame RunGame(string input, int numCups, int numTurns)
        {
            var game = new CrabGame(input, numCups);
            for (int i = 0; i < numTurns; i++)
            {
                game.Move();
            }

            return game;
        }
    }
}
