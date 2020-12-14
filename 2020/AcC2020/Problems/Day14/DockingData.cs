using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;

namespace AoC.AoC2020.Problems.Day14
{
    public class DockingData :AoCSolution<long>
    {
        public override int Year => 2020;
        public override int Day => 14;
        public override string Name => "Day 14: Docking Data";
        public override string InputFileName => "Day14.txt";

        public override IEnumerable<long> Solve(IEnumerable<string> input)
        {
            DockingController c = new DockingController();

            yield return c.UpdateValues(input.ToList());

            yield return c.UpdateAddresses(input.ToList());
        }

    }
}