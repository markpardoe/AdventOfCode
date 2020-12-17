using Aoc.Aoc2018.Day11;
using AoC.Common.TestHelpers;

namespace AoC.AoC2018.Tests.Day11
{
    public class ChronalChargeTests : AocSolutionTest<string>
    {
        protected override SolutionData<string> Solution => new SolutionData<string>(new ChronalCharge(), "235,20", "237,223,14");
    }
}