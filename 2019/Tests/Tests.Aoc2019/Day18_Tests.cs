using Aoc.AoC2019.Problems.Day18;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Aoc.AoC2019.Tests
{
    public class Day18_Tests
    {
        [Theory]
        [ClassData(typeof(Day18_TestData))]
        public void Test_MazeRobot_Finds_ShortestPath(List<string> data, int expectedResult)
        {
            Maze maze = new Maze(data);
            MazeRobot robot = new MazeRobot(maze, maze.StartPositions.First(), false);
            Assert.Equal(expectedResult, robot.FindShortestPath().TotalDistance);
        }


        private class Day18_TestData : IEnumerable<object[]>
        {

            private readonly List<string> Example1 = new List<string>()
            {
                "#########",
                "#b.A.@.a#",
                "#########",
            };

            private readonly List<string> Example2 = new List<string>()
            {
                "########################",
                "#f.D.E.e.C.b.A.@.a.B.c.#",
                "######################.#",
                "#d.....................#",
                "########################"
            };

            private readonly List<string> Example3 = new List<string>()
            {
                "########################",
                "#...............b.C.D.f#",
                "#.######################",
                "#.....@.a.B.c.d.A.e.F.g#",
                "########################"
            };

            private readonly List<string> Example4 = new List<string>()
            {
                "#################",
                "#i.G..c...e..H.p#",
                "########.########",
                "#j.A..b...f..D.o#",
                "########@########",
                "#k.E..a...g..B.n#",
                "########.########",
                "#l.F..d...h..C.m#",
                "#################"
            };

            private readonly List<string> Example5 = new List<string>()
            {
                "########################",
                "#@..............ac.GI.b#",
                "###d#e#f################",
                "###A#B#C################",
                "###g#h#i################",
                "########################"
            };

            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { Example1, 8 };
                yield return new object[] { Example2, 86 };
                yield return new object[] { Example3, 132 };
                yield return new object[] { Example4, 136 };
                yield return new object[] { Example5, 81 };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    }
}
