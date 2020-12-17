using Aoc.AoC2019.Problems.Day23;
using AoC.Common.TestHelpers;

namespace AoC.AoC2019.Tests.Day23
{
    public class Day23Tests : AocSolutionTest<long>
    {
        protected override SolutionData<long> Solution => new SolutionData<long>(new Day23_Solution(), 23213, 17874);
    }
}