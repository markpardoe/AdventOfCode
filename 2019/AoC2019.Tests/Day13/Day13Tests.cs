using Aoc.AoC2019.Problems.Day13;
using AoC.Common.TestHelpers;

namespace AoC.AoC2019.Tests.Day13
{
    public class Day13Tests : AocSolutionTest<long>
    {
        protected override SolutionData<long> Solution => new SolutionData<long>(new Day13_Solution(), 270, 12535);
    }
}