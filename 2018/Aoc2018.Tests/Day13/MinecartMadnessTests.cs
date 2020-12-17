using Aoc.Aoc2018.Day13;
using AoC.Common.TestHelpers;

namespace AoC.AoC2018.Tests.Day13
{
    public class MinecartMadnessTests : AocSolutionTest<string>
    {
        protected override SolutionData<string> Solution => new SolutionData<string>(new MineCartMadness(), "(74, 87)", "(29, 74)");
    }
}