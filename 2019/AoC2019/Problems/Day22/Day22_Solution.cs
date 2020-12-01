using AoC.Common;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day22
{
    public class Day22_Solution :AoCSolution<string>
    {
        public override int Year => 2019;

        public const int SPACE_DECK_SIZE = 10007;

        public override int Day => 22;

        public override string Name => "Day 22: Slam Shuffle";

        public override string InputFileName => "Day22.txt";

        public override IEnumerable<string> Solve(IEnumerable<string> data)
        {
            Deck deck = new Deck(SPACE_DECK_SIZE);
            deck.Shuffle(data);
            yield return deck.IndexOf(2019).ToString();

            // Solution for part 2 shamelessly stolen from reddit.
            yield return BigShuffle.Part2(data.ToList());
        }
    }
}
