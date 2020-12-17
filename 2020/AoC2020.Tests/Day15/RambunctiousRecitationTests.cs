using AoC.AoC2020.Problems.Day15;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using System.Linq;
using Xunit;

namespace AoC.AoC2020.Tests.Day15
{
    public class RambunctiousRecitationTests
    {
        [Theory]
        [MemberData(nameof(Solution))]
        public void Solve_WithInput_ReturnsCorrectValues(ISolution<int> sut, int result1, int result2)
        {
            var data = InputData.LoadSolutionInput(sut);
            var actualResults = sut.Solve(data).ToList();

            actualResults.First().ShouldBe(result1);
            actualResults.Last().ShouldBe(result2);
        }

        public static SolutionData<int> Solution => new SolutionData<int>(new RambunctiousRecitation(), 257, 8546398);
    }
}