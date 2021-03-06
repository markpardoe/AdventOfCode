﻿using Aoc.AoC2019.IntCode;
using AoC.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day25
{
    public class Day25 : AoCSolution<string>
    {
        public override int Year => 2019;

        public override int Day => 25;

        public override string Name => "Day 25: Cryostasis";

        public override string InputFileName => "Day25.txt";

        public override IEnumerable<string> Solve(IEnumerable<string> data)
        {
            yield return "Day 25 needs to be run manually!  Please call RunGame to run the text adventure";
        }

        public void RunGame(IEnumerable<string> data)
        {

            IVirtualMachine computer = new IntCodeVM(data.First());
            AdventureGame game = new AdventureGame(computer);
            game.RunGame();
            Console.WriteLine("***** GAME FINISHED *****");
            Console.ReadLine();
        }
    }
}
