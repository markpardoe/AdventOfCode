using AoC.AoC2020.Problems.Day02;
using AoC.Common;
using AoC.Common.TestHelpers;
using System.Linq;
using Xunit;

namespace AoC.AoC2020.Tests.Day02
{
    public class PasswordPhilosophyTests
    {
        [Theory]
        [MemberData(nameof(Solution))]
        public void Solve_WithInput_ReturnsCorrectValues(ISolution<int> sut, int result1, int? result2)
        {
            var data = InputData.LoadSolutionInput(sut);
            var actualResults = sut.Solve(data).ToList();

            Assert.Equal(result1, actualResults.First());

            if (result2.HasValue)
            {
                Assert.Equal(result2.Value, actualResults.Last());
            }
        }

        public static SolutionData<int> Solution => new SolutionData<int>(new PasswordPhilosophy(), 638, 699);
    }
}
