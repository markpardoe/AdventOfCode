using Aoc.Aoc2018.Day09;
using Shouldly;
using Xunit;

namespace AoC.AoC2018.Tests.Solutions
{
    public class Day09_Tests
    {
        [Theory]
        [InlineData(10, 1618, 8317)]
        [InlineData(13, 7999, 146373)]
        [InlineData(17, 1104, 2764)]
        [InlineData(21, 6111, 54718)]
        [InlineData(30, 5807, 37305)]
        public void Test_MarbleGame_ReturnsCorrectValue(int numPlayers, int maxNumber, long expectedScore)
        {
            var sut = new MarbleMania();
            sut.PlayGame(numPlayers, maxNumber).ShouldBe(expectedScore);
        }
    }
}