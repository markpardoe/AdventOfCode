using System;
using System.Collections.Generic;
using System.Text;
using Aoc.Aoc2018.Day05;
using Xunit;

namespace Tests_Aoc2018.Solutions
{
    public class Day05_Tests
    {
        [Theory]
        [InlineData("dabCBAcaDA", "dabAcCaCBAcCcaDA")]
        [InlineData("", "abBA")]
        [InlineData("AABBCC", "AABBCC")]
        [InlineData("aaBBcc", "aaBBcc")]
        public void Test_ReducePolymer(string expectedResult, string input)
        {
            Day05_Solution sut = new Day05_Solution();
            string actualResult = sut.ReducePolymer(input);
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(4, "dabAcCaCBAcCcaDA")]
        public void Test_FindShortestWithReduction(int expectedResult, string input)
        {
            Day05_Solution sut = new Day05_Solution();
            int actualResult = sut.FindShortestWithReduction(input);
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
