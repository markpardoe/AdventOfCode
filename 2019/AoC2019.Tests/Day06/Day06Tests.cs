using System.Collections.Generic;
using System.Linq;
using Aoc.AoC2019.Problems.Day06;
using AoC.Common.TestHelpers;
using Shouldly;
using Xunit;

namespace AoC.AoC2019.Tests.Day06
{
    public class Day06Tests : AocSolutionTest<int>
    {
        [Theory]
        [InlineData(42, "COM)B,B)C,C)D,D)E,E)F,B)G,G)H,D)I,E)J,J)K,K)L")]
        public void Test_OrbitMap_CountOrbits(int expectedResult, string inputData)
        {
            List<string> orbits = inputData.Split(',').ToList();
            OrbitMap map = new OrbitMap(orbits);
           
            int actualResult = map.CountOrbits();
            actualResult.ShouldBe(expectedResult);

        }

        [Theory]
        [InlineData(4, "COM)B,B)C,C)D,D)E,E)F,B)G,G)H,D)I,E)J,J)K,K)L,K)YOU,I)SAN")]
        public void Test_OrbitMap_FindPath(int expectedResult, string inputData)
        {
            List<string> orbits = inputData.Split(',').ToList();
            OrbitMap map = new OrbitMap(orbits);

            int actualResult = map.FindPath("YOU", "SAN");
            actualResult.ShouldBe(expectedResult);
        }

        protected override SolutionData<int> Solution => new SolutionData<int>(new Day06_Solution(), 154386, 346);
    }
}