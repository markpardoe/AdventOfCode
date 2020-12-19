using Aoc.Aoc2018.Day20;
using AoC.Common.TestHelpers;

namespace AoC.AoC2018.Tests.Day20
{
    public class RegularMapTests : AocSolutionTest<int>
    {
        protected override SolutionData<int> Solution => new SolutionData<int>(new RegularMap(), 3930, 8240);
    }
}