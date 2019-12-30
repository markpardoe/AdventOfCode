using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;
using Aoc.AoC2019.IntCode;

namespace Aoc.AoC2019.Problems.Day25
{
    public class Day25 : ISolution
    {
        public int Year => 2019;

        public int Day => 25;

        public string Name => "Day 25: Cryostasis";

        public string InputFileName => "Day25.txt";

        public IEnumerable<string> Solve(IEnumerable<string> data)
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
