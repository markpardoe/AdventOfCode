using AoC.Common;
using Aoc.AoC2019.Problems.Day12;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Aoc.AoC2019.Tests
{
    public class Day12_Tests
    {
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

            Assert.Equal(179, map.GetTotalEnergy());
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

            Assert.Equal(1940, map.GetTotalEnergy());
        }

    }
}
