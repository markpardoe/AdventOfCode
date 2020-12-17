using Aoc.Aoc2018.Day15;
using AoC.Common.TestHelpers;

namespace AoC.AoC2018.Tests.Day15
{
    public class BeverageBanditsTests : AocSolutionTest<long>
    {
        protected override SolutionData<long> Solution => new SolutionData<long>(new BeverageBandits(), 197025, 44423);
    }
}