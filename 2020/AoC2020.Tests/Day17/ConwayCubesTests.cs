using AoC.AoC2020.Problems.Day17;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AoC.AoC2020.Tests.Day17
{
    public class ConwayCubesTests
    {
        [Fact]
        public void Solve_WithExampleData_ReturnsCorrectValues()
        {
            var sut = new ConwayCubes();

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

        public static SolutionData<int> Solution => new SolutionData<int>(new ConwayCubes(), 310, 2056);

        private readonly int ExampleResult1 = 112;
        private readonly int ExampleResult2 = 848;
        private readonly IEnumerable<string> ExampleData = new List<string>()
        {
            ".#.",
            "..#",
            "###"
        };
    }
}