using AoC.AoC2020.Problems.Day22;
using AoC.Common.TestHelpers;

namespace AoC.AoC2020.Tests.Day22
{
    public class CrabCombatTests :AocSolutionTest<int>
    {
        protected override SolutionData<int> Solution => new SolutionData<int>(new CrabCombat(), 35818, 34771);
    }
}