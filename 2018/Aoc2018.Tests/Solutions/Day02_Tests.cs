using System.Linq;
using Aoc.Aoc2018.Day02;
using Xunit;

namespace AoC.AoC2018.Tests.Solutions
{
    public class Day02_Tests
    {
        [Theory]
        [InlineData(12, "abcdef", "bababc", "abbcde", "abcccd", "aabcdd", "abcdee", "ababab")]
        public void Test_CalculateCheckSum(int expectedResult, params string[] input)
        {
            InventoryManagementSystem sut = new InventoryManagementSystem();

            int result = sut.CalculateCheckSum(input);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("fgij", "abcde", "fghij", "klmno", "pqrst", "fguij", "axcye", "wvxyz")]
        public void Test_FindSimilarBoxIds(string expectedResult, params string[] input)
        {
            InventoryManagementSystem sut = new InventoryManagementSystem();

            string result = sut.FindSimilarBoxIds(input.ToList());

            Assert.Equal(expectedResult, result);
        }
    }
}
