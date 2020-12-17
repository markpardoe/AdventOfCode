using Aoc.Aoc2018.Day17;
using AoC.Common.TestHelpers;

namespace AoC.AoC2018.Tests.Day17
{
    public class ReservoirResearchTests : AocSolutionTest<int>
    {
        protected override SolutionData<int> Solution => new SolutionData<int>(new ReservoirResearch(), 28246, 23107);
    }
}