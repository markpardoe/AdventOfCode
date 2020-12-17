using Aoc.AoC2019.Problems.Day24;
using AoC.Common.TestHelpers;

namespace AoC.AoC2019.Tests.Day24
{
    public class Day24Tests : AocSolutionTest<long>
    {
        protected override SolutionData<long> Solution => new SolutionData<long>(new Day24_Solution(), 26840049, 1995);
    }
}