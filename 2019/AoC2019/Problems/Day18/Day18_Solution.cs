using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using AoC.Common;

namespace AoC2019.Problems.Day18
{
    public class Day18_Solution :ISolution
    {
        protected bool IgnoreDoors = false;

        public int Year => 2019;

        public int Day => 18;

        public virtual string Name => "Day 18: Many-Worlds Interpretation";

        public virtual string InputFileName => "Day18.txt";


        public virtual IEnumerable<string> Solve(IEnumerable<string> input)
        {
            yield return FindShortestPath(input, IgnoreDoors).ToString();
        }


        public int FindShortestPath(IEnumerable<string> input, bool ignoreDoors)
        {
            Maze maze = new Maze(input);

            List<MazeRobot> robots = new List<MazeRobot>();
            List<Path> paths = new List<Path>(robots.Count);
            int total = 0;
            foreach (MazeTile m in maze.StartPositions)
            {
                MazeRobot robot = new MazeRobot(maze, m, ignoreDoors);
                Path p = robot.FindShortestPath();
                paths.Add(p);
                total += p.TotalDistance;
            }
            return total;
        }


        private static readonly List<string> Example1 = new List<string>()
        {
            "#########",
            "#b.A.@.a#",
            "#########",
        };

        private static readonly List<string> Example2 = new List<string>()
        {
            "########################",
            "#f.D.E.e.C.b.A.@.a.B.c.#",
            "######################.#",
            "#d.....................#",
            "########################"
        };

        private static readonly List<string> Example3 = new List<string>()
        {
            "########################",
            "#...............b.C.D.f#",
            "#.######################",
            "#.....@.a.B.c.d.A.e.F.g#",
            "########################"
        };

        private static readonly List<string> Example4 = new List<string>()
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

        private static readonly List<string> Example5 = new List<string>()
        {
            "########################",
            "#@..............ac.GI.b#",
            "###d#e#f################",
            "###A#B#C################",
            "###g#h#i################",
            "########################"
        };
    }
}
