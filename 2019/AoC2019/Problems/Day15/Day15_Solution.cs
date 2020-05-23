using System;
using System.Collections.Generic;
using System.Text;
using Aoc.AoC2019.IntCode;
using System.Linq;
using AoC.Common.Mapping;

using AoC.Common;

namespace Aoc.AoC2019.Problems.Day15
{
    public class Day15_Solution : ISolution
    {
        public string URL => @"https://adventofcode.com/2019/day/15";
        public int Year => 2019;

        public int Day => 15;

        public string Name => "Day 15: Oxygen System";

        public string InputFileName => "Day15.txt";


        public IEnumerable<string> Solve(IEnumerable<string> input)
        {
            IVirtualMachine vm = new IntCodeVM(input.First());
            Robot robot = new Robot(vm);
            robot.ExploreShip();

            yield return robot.GetPathToOxygen().Count().ToString();
            yield return robot.FindTimeToFillWithOxxygen().ToString();
        }
    }
}
