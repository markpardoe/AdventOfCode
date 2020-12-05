using AoC.Common;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day18
{
    public class Day18_Solution :AoCSolution<int>
    {
        public override int Year => 2019;

        public override int Day => 18;

        public override string Name => "Day 18: Many-Worlds Interpretation";

        public override string InputFileName => "Day18.txt";

        protected bool IgnoreDoors = false;

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            yield return FindShortestPath(input, IgnoreDoors);
        }

        /// <summary>
        /// Finds the shortest path through the maze to collect all keys; using one or more robots.
        /// Path finding per MazeTile takes too long - so instead:
        ///  1.  Find shortest path between each key to every-other key (including doors that need opening)
        ///      using breadth-first search.
        ///  2.  Find shortest path from each robot to each key.  (again ignoring doors).
        ///      Once a key is found, then use the KeyDistance mapping to move between keys.
        /// 3.  We can then do a breadth first search over Paths (a path is a collection of moves (start --> key or key --> key) with the same final location and keys collected)
        ///     to find the shortest path that collects all the keys.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="ignoreDoors"></param>
        /// <returns></returns>
        public int FindShortestPath(IEnumerable<string> input, bool ignoreDoors)
        {
            Maze maze = new Maze(input);

            // May have 1 - or more robots
            List<MazeRobot> robots = new List<MazeRobot>();

            // Contains the path for each robot.
            List<Path> paths = new List<Path>(robots.Count);

            // For each robot (based on start position) 
            // get the total distance it travels.  
            // If only 1 robot -there will be only one path = TOTAL DISTANCE.
            //
            // For the multiple robot example - we can assume that there's no waiting around (ie no robot can move).
            // So if a robot can't move - another one can.  We don't care when or where they wait - only the total distance moved.
            // So total distance = sum(distance per robot).
            foreach (MazeTile m in maze.StartPositions)
            {
                MazeRobot robot = new MazeRobot(maze, m, ignoreDoors);
                Path p = robot.FindShortestPath();
                paths.Add(p);
            }
            return paths.Sum(p => p.TotalDistance);
        }       
    }
}
