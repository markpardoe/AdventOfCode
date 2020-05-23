using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day23
{
    public class Day23_Solution : AoC2019Solution
    {
        public override string URL => @"https://adventofcode.com/2019/day/23";

        public override int Day => 23;

        public override string Name => "Day 23: Category Six";

        public override string InputFileName => "Day23.txt";

        public override IEnumerable<string> Solve(IEnumerable<string> data)
        {
            NicController nic = new NicController(50, data.First());
            yield return nic.Execute().ToString();

            NicControllerWithNat nat = new NicControllerWithNat(50, data.First());
            yield return nat.Execute().ToString();
        }
    }
}
