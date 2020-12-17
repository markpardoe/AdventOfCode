using System.Collections.Generic;
using Aoc.AoC2019.Problems.Day03;
using Xunit;

namespace AoC.AoC2019.Tests
{
    public class Day03_Tests
    {
        private readonly Day03_Solution sut = new Day03_Solution();

        [Theory]
        [InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", 159)]
        [InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 135)]
        public void Day3_SolveA_Returns_Correct_Point(string wire1, string wire2, int expectedDistance)
        {
            List<string> data = new List<string>() { wire1, wire2 };

            int result = sut.CalculateDistanceFromOriginAtNearestIntersection(data);
            Assert.Equal(expectedDistance, result);
        }

        [Theory]
        [InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", 610)]
        [InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 410)]
        public void Day3_SolveB_Returns_Correct_Point(string wire1, string wire2, int expectedSteps)
        {
            List<string> data = new List<string>() { wire1, wire2 };

            int result = sut.CalculateCumulativeWireLengthAtFirstIntersection(data);
            Assert.Equal(expectedSteps, result);
        }
    }
}
