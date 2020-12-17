using Aoc.Aoc2018.Day21;
using AoC.Common.TestHelpers;

namespace AoC.AoC2018.Tests.Day21
{
    public class ChronalConversionTests : AocSolutionTest<long>
    {
        protected override SolutionData<long> Solution => new SolutionData<long>(new ChronalConversion(), 11592302, 313035);
    }
}