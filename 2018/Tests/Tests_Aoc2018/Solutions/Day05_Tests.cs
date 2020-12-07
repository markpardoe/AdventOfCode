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
