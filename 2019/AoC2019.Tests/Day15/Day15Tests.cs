using Aoc.AoC2019.Problems.Day15;
using AoC.Common.TestHelpers;

namespace AoC.AoC2019.Tests.Day15
{
    public class Day15Tests : AocSolutionTest<int>
    {
        protected override SolutionData<int> Solution => new SolutionData<int>(new Day15_Solution(), 308, 328);
    }
}
