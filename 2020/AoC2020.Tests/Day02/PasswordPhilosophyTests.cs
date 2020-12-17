using AoC.AoC2020.Problems.Day02;
using AoC.Common;
using AoC.Common.TestHelpers;
using System.Linq;
using Xunit;

namespace AoC.AoC2020.Tests.Day02
{
    public class PasswordPhilosophyTests : AocSolutionTest<int>
    {
      protected override SolutionData<int> Solution => new SolutionData<int>(new PasswordPhilosophy(), 638, 699);
    }
}
