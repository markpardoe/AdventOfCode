using System.Collections.Generic;
using System.Linq;
using Aoc.AoC2019.Problems.Day06;
using Xunit;

namespace AoC.AoC2019.Tests
{
    public class Day06_Tests
    {
        [Theory]
        [InlineData(42, "COM)B,B)C,C)D,D)E,E)F,B)G,G)H,D)I,E)J,J)K,K)L")]
        public void Test_OrbitMap_CountOrbits(int expectedResult, string inputData)
        {
            List<string> orbits = inputData.Split(',').ToList();
            OrbitMap map = new OrbitMap(orbits);
           
            int actualResult = map.CountOrbits();
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(4, "COM)B,B)C,C)D,D)E,E)F,B)G,G)H,D)I,E)J,J)K,K)L,K)YOU,I)SAN")]
        public void Test_OrbitMap_FindPath(int expectedResult, string inputData)
        {
            List<string> orbits = inputData.Split(',').ToList();
            OrbitMap map = new OrbitMap(orbits);

            int actualResult = map.FindPath("YOU", "SAN");
            Assert.Equal(expectedResult, actualResult);
        }
    }
}