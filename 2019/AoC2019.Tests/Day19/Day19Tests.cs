using Aoc.AoC2019.Problems.Day19;
using AoC.Common.TestHelpers;

namespace AoC.AoC2019.Tests.Day19
{
    public class Day19Tests : AocSolutionTest<int>
    {
        protected override SolutionData<int> Solution => new SolutionData<int>(new Day19_Solution(), 201, 6610984);
    }
}