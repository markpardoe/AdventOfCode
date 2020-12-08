using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;

namespace Aoc.Aoc2018.Day09
{
    public class MarbleMania :AoCSolution<long>
    {
        public override IEnumerable<long> Solve(IEnumerable<string> input)
        {
            yield return PlayGame(473, 70904);
            yield return PlayGame(473, 7090400);
        }

        public override int Year => 2018;
        public override int Day => 9;
        public override string Name => "Day 9: Marble Mania";
        public override string InputFileName => null;


        public long PlayGame(int numPlayers, int maxNumber)
        {
            MarbleGame game = new MarbleGame();
            long[] scores = new long[numPlayers];
            int currentPlayer = 0;

            for (int number = 1; number <= maxNumber; number++)
            {
                int score = game.AddMarble(number);
                scores[currentPlayer] += score;

                // move to next player
                currentPlayer += 1;
                if (currentPlayer >= numPlayers)
                {
                    currentPlayer = 0;
                }
            }

            return scores.Max();
        }
    }
}