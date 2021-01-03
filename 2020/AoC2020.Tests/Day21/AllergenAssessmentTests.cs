using AoC.AoC2020.Problems.Day21;
using AoC.Common.TestHelpers;

namespace AoC.AoC2020.Tests.Day21
{
    public class AllergenAssessmentTests : AocSolutionTest<string>
    {
        protected override SolutionData<string> Solution => new SolutionData<string>(new AllergenAssessment(), "2315", "cfzdnz,htxsjf,ttbrlvd,bbbl,lmds,cbmjz,cmbcm,dvnbh");
    }
}