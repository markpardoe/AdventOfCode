using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;

namespace Aoc.AoC2019.Problems.Day23
{
    public class Day23_Solution : ISolution
    {
        public int Year => 2019;

        public int Day => 23;

        public string Name => "Day 23: Category Six";

        public string InputFileName => "Day23.txt";

        public IEnumerable<string> Solve(IEnumerable<string> data)
        {
            NicController nic = new NicController(50, data.First());
            yield return nic.Execute().ToString();

            NicControllerWithNat nat = new NicControllerWithNat(50, data.First());
            yield return nat.Execute().ToString();
        }
    }
}
