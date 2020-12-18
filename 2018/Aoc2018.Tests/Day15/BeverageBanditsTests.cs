using System.Collections.Generic;
using Aoc.Aoc2018.Day15;
using AoC.Common.TestHelpers;
// ReSharper disable UnusedMember.Local

namespace AoC.AoC2018.Tests.Day15
{
    public class BeverageBanditsTests : AocSolutionTest<long>
    {
        protected override SolutionData<long> Solution => new SolutionData<long>(new BeverageBandits(), 197025, 44423);

        // Elves win in 37 rounds
        // 982 hp = 36334
        private static readonly List<string> Example1 = new List<string>
        {
            "#######",
            "#G..#E#",
            "#E#E.E#",
            "#G.##.#",
            "#...#E#",
            "#...E.#",
            "#######"
        };


        // Elves win in 46 rounds. 859 hp = 39514
        private static readonly List<string> Example2 = new List<string>
        {
            "#######",
            "#E..EG#",
            "#.#G.E#",
            "#E.##E#",
            "#G..#.#",
            "#..E#.#",
            "#######"
        };

        // Goblins win in round 35.  793 hp.  = 27755
        private static readonly List<string> Example3 = new List<string>
        {
            "#######",
            "#E.G#.#",
            "#.#G..#",
            "#G.#.G#",
            "#G..#.#",
            "#...E.#",
            "#######"
        };

        // Goblins win in 54 rounds.  536 hp = 28944
        private static readonly List<string> Example4 = new List<string>
        {
            "#######",
            "#.E...#",
            "#.#..G#",
            "#.###.#",
            "#E#G#G#",
            "#...#G#",
            "#######"
        };

        // Goblins win in 20 rounds.  937 hp = 18740
        private static readonly List<string> Example5 = new List<string>
        {
            "#########",
            "#G......#",
            "#.E.#...#",
            "#..##..G#",
            "#...##..#",
            "#...#...#",
            "#.G...G.#",
            "#.....G.#",
            "#########"
        };
    }
}