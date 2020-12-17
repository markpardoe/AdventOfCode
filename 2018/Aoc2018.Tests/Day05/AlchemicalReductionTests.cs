using Aoc.Aoc2018.Day05;
using AoC.Common.TestHelpers;
using Xunit;

namespace AoC.AoC2018.Tests.Day05
{
    public class AlchemicalReductionTests :AocSolutionTest<string>
    {
        protected override SolutionData<string> Solution => new SolutionData<string>(new AlchemicalReduction(), "9154", "4556");

        [Theory]
        [InlineData("dabCBAcaDA", "dabAcCaCBAcCcaDA")]
        [InlineData("", "abBA")]
        [InlineData("AABBCC", "AABBCC")]
        [InlineData("aaBBcc", "aaBBcc")]
        public void Test_ReducePolymer(string expectedResult, string input)
        {
            AlchemicalReduction sut = new AlchemicalReduction();
            string actualResult = sut.ReducePolymer(input);
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(4, "dabAcCaCBAcCcaDA")]
        public void Test_FindShortestWithReduction(int expectedResult, string input)
        {
            AlchemicalReduction sut = new AlchemicalReduction();
            int actualResult = sut.FindShortestWithReduction(input);
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
