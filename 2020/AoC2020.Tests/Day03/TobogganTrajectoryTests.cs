using AoC.AoC2020.Problems.Day03;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AoC.AoC2020.Tests.Day03
{
    public class TobogganTrajectoryTests
    {
        [Theory]
        [MemberData(nameof(Solution))]
        public void Solve_WithInput_ReturnsCorrectValues(ISolution<long> sut, long result1, long result2)
        {
            var data = InputData.LoadSolutionInput(sut);
            var actualResults = sut.Solve(data).ToList();

            actualResults.First().ShouldBe(result1);
            actualResults.Last().ShouldBe(result2);
        }


        [Fact]
        public void Solve_WithExampleData_ReturnsCorrectValues()
        {
            var sut = new TobogganTrajectory();

            var actualResults = sut.Solve(ExampleData).ToList();

            actualResults.First().ShouldBe(ExampleResult1);
            actualResults.Last().ShouldBe(ExampleResult2);
        }

        public static SolutionData<long> Solution => new SolutionData<long>(new TobogganTrajectory(), 214, 8336352024);

        private readonly long ExampleResult1 = 7;
        private readonly long ExampleResult2 = 336;
        private readonly IEnumerable<string> ExampleData = new List<string>()
        {
            "..##.......",
            "#...#...#..",
            ".#....#..#.",
            "..#.#...#.#",
            ".#...##..#.",
            "..#.##.....",
            ".#.#.#....#",
            ".#........#",
            "#.##...#...",
            "#...##....#",
            ".#..#...#.#"
        };
    }
}
