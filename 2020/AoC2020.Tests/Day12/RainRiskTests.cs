using AoC.AoC2020.Problems.Day12;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AoC.AoC2020.Tests.Day12
{

    public class RainRiskTests
    {
        [Fact]
        public void Solve_WithExampleData_ReturnsCorrectValues()
        {
            var sut = new RainRisk();;

            var actualResults = sut.Solve(ExampleData).ToList();

            actualResults.First().ShouldBe(ExampleResult1);
            actualResults.Last().ShouldBe(ExampleResult2);
        }

        [Theory]
        [MemberData(nameof(Solution))]
        public void Solve_WithInput_ReturnsCorrectValues(ISolution<int> sut, int result1, int result2)
        {
            var data = InputData.LoadSolutionInput(sut);
            var actualResults = sut.Solve(data).ToList();

            actualResults.First().ShouldBe(result1);
            actualResults.Last().ShouldBe(result2);
        }

        public static SolutionData<int> Solution => new SolutionData<int>(new RainRisk(), 445, 42495);

        private readonly int ExampleResult1 = 25;
        private readonly int ExampleResult2 = 286;
        private readonly IEnumerable<string> ExampleData = new List<string>()
        {
            "F10",
            "N3",
            "F7",
            "R90",
            "F11"
        };
    }
}
