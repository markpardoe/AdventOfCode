namespace Aoc.AoC2019.Problems.Day18
{
    /// <summary>
    /// Override Day18 Solution file to use a separate input file due to changes in maze format.
    /// The trick here is that we can simply calculate the fastest path per robot - ignoring doors.  
    /// If a robot is stuck we can simply pretend we're moving a different robot until the door is unlocked.
    /// So the total time is Sum(TimePerRobot).
    /// </summary>
    public class Day18_Solution_ParallelRobots : Day18_Solution
    {
       public Day18_Solution_ParallelRobots()
        {
            IgnoreDoors = true;
        }

        public override string Name => "Day 18: Many-Worlds Interpretation - Multiple Robots";

        public override string InputFileName => "Day18_b.txt";      
    }
}
