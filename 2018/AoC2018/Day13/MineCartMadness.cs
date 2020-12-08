using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day13
{
    public class MineCartMadness :AoCSolution<string> 
    {
        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {

           // Position crash = RunMineCartSimulation(example1, true);
            Position crash = RunMineCartSimulation(input);
            yield return crash.ToString();
        }

        public override int Year => 2018;
        public override int Day => 13;
        public override string Name => "Day 13: Mine Cart Madness";
        public override string InputFileName => "Day13.txt";


        private Position RunMineCartSimulation(IEnumerable<string> input, bool drawScreen = false)
        {
            MineMap map = new MineMap(input);
            if (drawScreen)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(map.DrawMap());
            }

            while (map.Crashes.Count == 0)
            {
                map.MoveCarts();
                
                if (drawScreen)
                {
                    Console.ReadLine();
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine(map.DrawMap());
                }
            }

            return map.Crashes.First();
        }

        private static List<string> example1 = new List<string>
        {
            @"/->-\        ",
            @"|   |  /----\",
            @"| /-+--+-\  |",
            @"| | |  | v  |",
            @"\-+-/  \-+--/",
            @"  \------/   ",
        };

        private static List<string> example2 = new List<string>
        {
            @"/>-<\  ",
            @"|   |  ",
            @"| /<+-\",
            @"| | | v",
            @"\>+</ |",
            @"  |   ^",
            @"  \<->/"
        };
    }
}
