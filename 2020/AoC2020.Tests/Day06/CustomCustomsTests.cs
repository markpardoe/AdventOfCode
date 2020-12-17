using System.Collections.Generic;
using System.Linq;
using AoC.AoC2020.Problems.Day06;

using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using Xunit;

namespace AoC.AoC2020.Tests.Day06
{
    public class CustomCustomsTests : AocSolutionTest<int>
    {
        [Fact]
        public void Solve_WithExampleData_ReturnsCorrectValues()
        {
            var sut = new CustomCustoms();

            var actualResults = sut.Solve(ExampleData).ToList();

            actualResults.First().ShouldBe(ExampleResult1);
            actualResults.Last().ShouldBe(ExampleResult2);
        }

        protected override SolutionData<int> Solution => new SolutionData<int>(new CustomCustoms(), 6534, 3402);

        private readonly int ExampleResult1 = 11;
        private readonly int ExampleResult2 = 6;
        private readonly IEnumerable<string> ExampleData = new List<string>()
        {
            "abc",
            "",
            "a",
            "b",
            "c",
            "",
            "ab",
            "ac",
            "",
            "a",
            "a",
            "a",
            "a",
            "",
            "b"
        };
    }
}
