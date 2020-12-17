using Aoc.AoC2019.Problems.Day12;
using AoC.Common.TestHelpers;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace AoC.AoC2019.Tests.Day12
{
    public class Day12Tests : AocSolutionTest<string>
    {
        protected override SolutionData<string> Solution => new SolutionData<string>(new Day12_Solution(), "12773", "306798770391636");

        [Fact]
        public void Test_Day12_Example1( )
        {
            List<Position3d> positions = new List<Position3d>
            {
                new Position3d(-1, 0, 2),
                new Position3d(2, -10, -7),
                new Position3d(4, -8, 8),
                new Position3d(3, 5, -1),
            };

            MoonMap map = new MoonMap(positions);

            for (int i = 0; i < 10; i++)
            {               
                map.Move();
            }

            map.GetTotalEnergy().ShouldBe(179);
        }

        [Fact]
        public void Test_Day12_Example2()
        {
            List<Position3d> positions = new List<Position3d>
            {
                new Position3d(-8, -10, 0),
                new Position3d(5, 5, 10),
                new Position3d(2, -7, 3),
                new Position3d(9, -8, -3),
            };

            MoonMap map = new MoonMap(positions);

            for (int i = 0; i < 100; i++)
            {
                map.Move();
            }

            map.GetTotalEnergy().ShouldBe(1940);
        }
    }
}