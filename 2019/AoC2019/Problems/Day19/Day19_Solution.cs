using Aoc.AoC2019.IntCode;
using AoC.Common;
using AoC.Common.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day19
{
    public class Day19_Solution : AoCSolution
    {
        public override int Year => 2019;

        public override int Day => 19;

        public override string Name => "Day 19: Tractor Beam";

        public override string InputFileName => "Day19.txt";

        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {
            TractorBeamMap map = new TractorBeamMap();
            var data = IntCodeVM.ParseStringData(input.First());
            Probe probe = new Probe(map, data);
            probe.ScanMap(50);
            
            yield return map.CountTiles(BeamStatus.Pulling).ToString();


            Position p = probe.FindShip(100);
            int result = (p.X * 10000) + p.Y;
            yield return result.ToString();
        }      
    }
}
