using Aoc.Aoc2018.Day08;
using AoC.Common.TestHelpers;

namespace AoC.AoC2018.Tests.Day08
{
    public class MemoryManeuverTests : AocSolutionTest<int>
    {
        protected override SolutionData<int> Solution => new SolutionData<int>(new MemoryManeuver(), 46781, 21405);
    }
}