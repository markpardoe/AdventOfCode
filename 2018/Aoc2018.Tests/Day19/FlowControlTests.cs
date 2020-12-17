using Aoc.Aoc2018.Day19;
using AoC.Common.TestHelpers;

namespace AoC.AoC2018.Tests.Day19
{
    public class FlowControlTests : AocSolutionTest<long>
    {
        protected override SolutionData<long> Solution => new SolutionData<long>(new FlowControl(), 2040, 25165632);
    }
}