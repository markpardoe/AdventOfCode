using Aoc.AoC2019.Problems.Day17;
using AoC.Common.TestHelpers;

namespace AoC.AoC2019.Tests.Day17
{
    public class Day17Tests : AocSolutionTest<long>
    {
        protected override SolutionData<long> Solution => new SolutionData<long>(new Day17_Solution(), 8084, 1119775);
    }
}