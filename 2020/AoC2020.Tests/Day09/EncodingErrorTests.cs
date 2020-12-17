using System.Linq;
using AoC.AoC2020.Problems.Day09;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using Xunit;

namespace AoC.AoC2020.Tests.Day09
{
    public class EncodingErrorTests
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

        public static SolutionData<long> Solution => new SolutionData<long>(new EncodingError(), 14360655, 1962331);
    }
}
