using Aoc.Aoc2018.Day06;
using AoC.Common.TestHelpers;

namespace AoC.AoC2018.Tests.Day06
{
    public class ChronalCoordinatesTests : AocSolutionTest<long>
    {
        protected override SolutionData<long> Solution => new SolutionData<long>(new ChronalCoordinates(), 4186, 45509);
    }
}