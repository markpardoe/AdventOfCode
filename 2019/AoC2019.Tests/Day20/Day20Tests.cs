using Aoc.AoC2019.Problems.Day20;
using AoC.Common.TestHelpers;

namespace AoC.AoC2019.Tests.Day20
{
    public class Day20Tests : AocSolutionTest<int>
    {
        protected override SolutionData<int> Solution => new SolutionData<int>(new Day20_Solution(), 516, 5966);
    }
}