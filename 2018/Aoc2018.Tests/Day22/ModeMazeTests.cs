using Aoc.Aoc2018.Day22;
using AoC.Common.TestHelpers;

namespace AoC.AoC2018.Tests.Day22
{
    public class ModeMazeTests : AocSolutionTest<int>
    {
        protected override SolutionData<int> Solution => new SolutionData<int>(new ModeMaze(), 7915, 980);
    }
}