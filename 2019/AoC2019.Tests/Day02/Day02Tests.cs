using Aoc.AoC2019.Problems.Day02;
using AoC.Common.TestHelpers;

namespace AoC.AoC2019.Tests.Day02
{
    public class Day02Tests : AocSolutionTest<long>
    {
        protected override SolutionData<long> Solution => new SolutionData<long>(new Day02_Solution(), 10566835, 2347);
    }
}
