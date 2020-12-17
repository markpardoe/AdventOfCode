using Aoc.Aoc2018.Day16;
using AoC.Common.TestHelpers;

namespace AoC.AoC2018.Tests.Day16
{
    public class ChronalClassificationTests : AocSolutionTest<long>
    {
        protected override SolutionData<long> Solution => new SolutionData<long>(new ChronalClassification(), 624, 584);
    }
}