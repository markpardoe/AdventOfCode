using Aoc.Aoc2018.Day18;
using AoC.Common.TestHelpers;

namespace AoC.AoC2018.Tests.Day18
{
    public class SettlersTests : AocSolutionTest<long>
    {
        protected override SolutionData<long> Solution => new SolutionData<long>(new SettlersOfNorthPole(), 483840, 219919);
    }
}