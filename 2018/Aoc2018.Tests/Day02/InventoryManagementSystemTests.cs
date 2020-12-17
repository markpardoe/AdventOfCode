using System.Linq;
using Aoc.Aoc2018.Day02;
using AoC.Common.TestHelpers;
using Xunit;

namespace AoC.AoC2018.Tests.Day02
{
    public class InventoryManagementSystemTests : AocSolutionTest<string>
    {
        protected override SolutionData<string> Solution => new SolutionData<string>(new InventoryManagementSystem(), "4712", "lufjygedpvfbhftxiwnaorzmq");

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
