using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using AoC.Common;

namespace Aoc.AoC2019.Problems.Day22
{
    public class Day22_Solution :ISolution
    {
        public const int SPACE_DECK_SIZE = 10007;
        
        public int Year => 2019;

        public int Day => 22;

        public string Name => "Day 22: Slam Shuffle";

        public string InputFileName => "Day22.txt";

        public IEnumerable<string> Solve(IEnumerable<string> data)
        {
            Deck deck = new Deck(SPACE_DECK_SIZE);
            deck.Shuffle(data);
            yield return deck.IndexOf(2019).ToString();

            // Solution for part 2 shamelessly stolen from reddit.
            yield return BigShuffle.Part2(data.ToList());
        }
    }
}
