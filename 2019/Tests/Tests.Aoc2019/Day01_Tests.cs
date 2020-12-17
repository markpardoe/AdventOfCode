using System.Collections.Generic;
using Aoc.AoC2019.Problems.Day01;
using Xunit;

namespace AoC.AoC2019.Tests
{
    public class Day01_Tests
    {
        private readonly Day01_Solution sut = new Day01_Solution();

        [Theory]
        [InlineData(12, 2)]
        [InlineData(14, 2)]
        [InlineData(1969, 654)]
        [InlineData(100756, 33583)]
        public void Test_Day01_CalculateFuel(int value, int result)
        {
            var input = new List<string>() { value.ToString() };
            int actualResult = sut.CalculateFuelRequirements(input, false);
            Assert.Equal(result, actualResult);
        }

        [Theory]
        [InlineData(14, 2)]
        [InlineData(1969, 966)]
        [InlineData(100756, 50346)]
        public void Test_Day01_CalculateFuelResursive(int value, int result)
        {
            var input = new List<string>() { value.ToString() };
            int actualResult = sut.CalculateFuelRequirements(input, true);
            Assert.Equal(result, actualResult);
        }   
    }
}
