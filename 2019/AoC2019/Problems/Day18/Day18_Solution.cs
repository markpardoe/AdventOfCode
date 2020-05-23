using AoC.Common;
using System.Collections.Generic;

namespace Aoc.AoC2019.Problems.Day18
{
    public class Day18_Solution :AoCSolution
    {
        public override int Year => 2019;

        public override int Day => 18;

        public override string Name => "Day 18: Many-Worlds Interpretation";

        public override string InputFileName => "Day18.txt";

        protected bool IgnoreDoors = false;

        public override IEnumerable<string> Solve(IEnumerable<string> input)
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
    }
}
