using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using AoC2018;
using System.Linq;

namespace Tests_Aoc2018.Solutions
{
    public class Day02_Tests
    {
        [Theory]
        [InlineData(12, "abcdef", "bababc", "abbcde", "abcccd", "aabcdd", "abcdee", "ababab")]
        public void Test_CalculateCheckSum(int expectedResult, params string[] input)
        {
            Day02_Solution sut = new Day02_Solution();

            int result = sut.CalculateCheckSum(input);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("fgij", "abcde", "fghij", "klmno", "pqrst", "fguij", "axcye", "wvxyz")]
        public void Test_FindSimilarBoxIds(string expectedResult, params string[] input)
        {
            Day02_Solution sut = new Day02_Solution();

            string result = sut.FindSimilarBoxIds(input.ToList());

            Assert.Equal(expectedResult, result);
        }
    }
}
