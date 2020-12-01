using Aoc.AoC2019.IntCode;
using AoC.Common;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day15
{
    public class Day15_Solution : AoCSolution<int>
    {
        public override int Year => 2019;

        public override int Day => 15;

        public override string Name => "Day 15: Oxygen System";

        public override string InputFileName => "Day15.txt";


        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            IVirtualMachine vm = new IntCodeVM(input.First());
            Robot robot = new Robot(vm);
            robot.ExploreShip();

            yield return robot.GetPathToOxygen().Count();
            yield return robot.FindTimeToFillWithOxxygen();
        }
    }
}
