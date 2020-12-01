using AoC.Common;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day23
{
    public class Day23_Solution : AoCSolution<long>
    {
        public override int Year => 2019;

        public override int Day => 23;

        public override string Name => "Day 23: Category Six";

        public override string InputFileName => "Day23.txt";

        public override IEnumerable<long> Solve(IEnumerable<string> data)
        {
            NicController nic = new NicController(50, data.First());
            yield return nic.Execute();

            NicControllerWithNat nat = new NicControllerWithNat(50, data.First());
            yield return nat.Execute();
        }
    }
}
