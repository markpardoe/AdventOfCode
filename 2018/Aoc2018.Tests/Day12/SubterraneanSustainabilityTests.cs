using Aoc.Aoc2018.Day12;
using AoC.Common.TestHelpers;

namespace AoC.AoC2018.Tests.Day12
{
    public class SubterraneanSustainabilityTests : AocSolutionTest<long>
    {
        protected override SolutionData<long> Solution => new SolutionData<long>(new SubterraneanSustainability(), 2823, 2900000001856);
    }
}