using System.Collections.Generic;
using System.Linq;
using AoC.AoC2020.Problems.Day08;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using Xunit;

namespace AoC.AoC2020.Tests.Day08
{
    
    public class HandholdHaltingTests :AocSolutionTest<int>
    {
        [Fact]
        public void Solve_WithExampleData_ReturnsCorrectValues()
        {
            var sut = new HandheldHalting();

            var actualResults = sut.Solve(ExampleData).ToList();

            actualResults.First().ShouldBe(ExampleResult1);
            actualResults.Last().ShouldBe(ExampleResult2);
        }
        
        protected override SolutionData<int> Solution => new SolutionData<int>(new HandheldHalting(), 1087, 780);

        private readonly int ExampleResult1 = 5;
        private readonly int ExampleResult2 = 8;
        private readonly IEnumerable<string> ExampleData = new List<string>()
        {
            "nop +0",
            "acc +1",
            "jmp +4",
            "acc +3",
            "jmp -3",
            "acc -99",
            "acc +1",
            "jmp -4",
            "acc +6"
        };
    }
}
