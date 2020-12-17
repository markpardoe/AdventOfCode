using System.Collections.Generic;
using System.Linq;
using AoC.AoC2020.Problems.Day11;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using Xunit;

namespace AoC.AoC2020.Tests.Day11
{
    
    public class SeatingSystemTests
    {
        [Fact]
        public void Solve_WithExampleData_ReturnsCorrectValues()
        {
            var sut = new SeatingSystem();

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

        public static SolutionData<int> Solution => new SolutionData<int>(new SeatingSystem(), 2468, 2214);

        private readonly int ExampleResult1 = 37;
        private readonly int ExampleResult2 = 26;
        private readonly IEnumerable<string> ExampleData = new List<string>()
        {
            "L.LL.LL.LL",
            "LLLLLLL.LL",
            "L.L.L..L..",
            "LLLL.LL.LL",
            "L.LL.LL.LL",
            "L.LLLLL.LL",
            "..L.L.....",
            "LLLLLLLLLL",
            "L.LLLLLL.L",
            "L.LLLLL.LL"
        };
    }
}
