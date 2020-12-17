using AoC.AoC2020.Problems.Day05;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using System.Linq;
using Xunit;

namespace AoC.AoC2020.Tests.Day05
{

    public class BinaryBoardingTests :AocSolutionTest<int>
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
        
        protected override SolutionData<int> Solution => new SolutionData<int>(new BinaryBoarding(), 885, 623);
    }
}
