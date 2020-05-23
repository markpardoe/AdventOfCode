using Aoc.AoC2019.IntCode;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day15
{
    public class Day15_Solution : AoC2019Solution
    {
        public override string URL => @"https://adventofcode.com/2019/day/15";

        public override int Day => 15;

        public override string Name => "Day 15: Oxygen System";

        public override string InputFileName => "Day15.txt";


        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {
            IVirtualMachine vm = new IntCodeVM(input.First());
            Robot robot = new Robot(vm);
            robot.ExploreShip();

            yield return robot.GetPathToOxygen().Count().ToString();
            yield return robot.FindTimeToFillWithOxxygen().ToString();
        }
    }
}
