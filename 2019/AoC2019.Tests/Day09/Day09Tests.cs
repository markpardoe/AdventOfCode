using Aoc.AoC2019.Problems.Day09;
using AoC.Common.TestHelpers;

namespace AoC.AoC2019.Tests.Day09
{
    public class Day09Tests : AocSolutionTest<long>
    {
        protected override SolutionData<long> Solution => new SolutionData<long>(new Day09_Solution(), 2316632620, 78869);
    }
}