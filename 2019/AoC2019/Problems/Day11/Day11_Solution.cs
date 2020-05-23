using System;
using System.Collections.Generic;
using System.Text;
using AoC.Common;
using Aoc.AoC2019.IntCode;
using System.Linq;
using Aoc.AoC2019;

namespace Aoc.AoC2019.Problems.Day11
{
    public class Day11_Solution :AoC2019Solution
    {
        public override string URL => @"https://adventofcode.com/2019/day/11";

        public override int Day => 11;

        public override string Name => "Day 11: Space Police";

        public override string InputFileName => "Day11.txt";

        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {
            yield return PaintShip(input.First(), PaintColor.Black).CountPanels().ToString();
            yield return PaintShip(input.First(), PaintColor.White).Map.DrawMap();
        }

        public PaintRobot PaintShip(string inputData, PaintColor initialColor)
        {
            var vm = new IntCodeVM(inputData);
            PaintRobot robot = new PaintRobot(vm, initialColor);
            robot.Paint();

            return robot;
        }      
    }
}
