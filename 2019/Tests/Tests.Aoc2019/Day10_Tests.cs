using System;
using System.Collections;
using System.Collections.Generic;
using Aoc.AoC2019.Problems.Day10;
using AoC.Common.Mapping;
using Xunit;

namespace AoC.AoC2019.Tests
{
    public class Day10_Tests
    {
        [Theory]
        [ClassData(typeof(Day10TestData))]
        public void Test_AsteroidMap_MaxAsteroidsDetected(List<string> data, int expectedResult, Position expectedPosition)
        {
            AsteroidMap map = new AsteroidMap(data);
            int max = Int32.MinValue;

            foreach (Position p in map.Asteroids)
            {
                int total = map.CountVisibleAsteroids(p);
                if (total > max)
                {
                    max = total;
                }
            }

            Assert.Equal(expectedResult, max);
        }

        [Theory]
        [ClassData(typeof(Day10TestData))]
        public void Test_AsteroidMap_BestPositionFound(List<string> data, int expectedResult, Position expectedPosition)
        {
            AsteroidMap map = new AsteroidMap(data);
            int max = Int32.MinValue;
            Position best = new Position(-1, -1);
            foreach (Position p in map.Asteroids)
            {
                int total = map.CountVisibleAsteroids(p);
                if (total > max)
                {
                    max = total;
                    best = p;
                }
            }

            Assert.Equal(expectedPosition.X, best.X);
            Assert.Equal(expectedPosition.Y, best.Y);
        }


        [Fact]
        public void Test_Day10_Lasers()
        {
            List<string> data = Day10TestData.Example5;
            Position expectedResult = new Position(8, 2);  // 200th asteroid


            AsteroidMap map = new AsteroidMap(data);
            Laser laser = new Laser(map, new Position(11, 13));
            Position p = new Position(0, 0);
            for (int i = 1; i < 201; i++)
            {
                p = laser.DestroyNextAsteroid();
            }

            Assert.Equal(expectedResult.X, p.X);
            Assert.Equal(expectedResult.Y, p.Y);
        }

        public class Day10TestData : IEnumerable<object[]>
        {

            public readonly static List<string> Example1 = new List<string>()
            {
                ".#..#",
                ".....",
                "#####",
                "....#",
                "...##"
            };

            public readonly static List<string> Example2 = new List<string>()
            {
                "......#.#.",
                "#..#.#....",
                "..#######.",
                ".#.#.###..",
                ".#..#.....",
                "..#....#.#",
                "#..#....#.",
                ".##.#..###",
                "##...#..#.",
                ".#....####"
            };

            public readonly static List<string> Example3 = new List<string>()
            {
                "#.#...#.#.",
                ".###....#.",
                ".#....#...",
                "##.#.#.#.#",
                "....#.#.#.",
                ".##..###.#",
                "..#...##..",
                "..##....##",
                "......#...",
                ".####.###."
            };

            public readonly static List<string> Example4 = new List<string>()
            {
                ".#..#..###",
                "####.###.#",
                "....###.#.",
                "..###.##.#",
                "##.##.#.#.",
                "....###..#",
                "..#.#..#.#",
                "#..#.#.###",
                ".##...##.#",
                ".....#.#.."
            };

            public readonly static List<string> Example5 = new List<string>()
            {
                ".#..##.###...#######",
                "##.############..##.",
                ".#.######.########.#",
                ".###.#######.####.#.",
                "#####.##.#.##.###.##",
                "..#####..#.#########",
                "####################",
                "#.####....###.#.#.##",
                "##.#################",
                "#####.##.###..####..",
                "..######..##.#######",
                "####.##.####...##..#",
                ".#####..#.######.###",
                "##...#.##########...",
                "#.##########.#######",
                ".####.#.###.###.#.##",
                "....##.##.###..#####",
                ".#.#.###########.###",
                "#.#.#.#####.####.###",
                "###.##.####.##.#..##"
            };

            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { Example1, 8 , new Position(3,4)};
                yield return new object[] { Example2, 33, new Position(5,8)};
                yield return new object[] { Example3, 35, new Position(1, 2)};
                yield return new object[] { Example4, 41, new Position(6, 3)};
                yield return new object[] { Example5, 210, new Position(11, 13)};
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
