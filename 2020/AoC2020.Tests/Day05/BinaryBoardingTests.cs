using AoC.AoC2020.Problems.Day05;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using System.Linq;
using Xunit;

namespace AoC.AoC2020.Tests.Day05
{

    public class BinaryBoardingTests
    {

        [Theory]
        [InlineData("FBFBBFFRLR", 357)]
        [InlineData("BFFFBBFRRR", 567)]
        [InlineData("FFFBBBFRRR", 119)]
        [InlineData("BBFFBBFRLL", 820)]
        public void Test_GetSeatId_ReturnsCorrectId(string input, int expectedValue)
        {
            var sut = new BinaryBoarding();
            var actual = sut.GetSeatId(input);

            actual.ShouldBe(expectedValue);
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

        public static SolutionData<int> Solution => new SolutionData<int>(new BinaryBoarding(), 885, 623);
    }
}
