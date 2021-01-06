using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC.Common;

namespace AoC2017.Day03
{
    public class SpiralMemory : AoCSolution<int>
    {
        public override int Year => 2017;
        public override int Day => 3;
        public override string Name => "Day 3: Spiral Memory";
        public override string InputFileName => null;

        private int puzzleInput = 368078;

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
           SpiralMap map = new SpiralMap();
          
           map.BuildMap(puzzleInput);
           yield return map.FindValueDistanceFromOrigin(puzzleInput).DistanceFromOrigin();

           SpiralSumMap map2 = new SpiralSumMap();
           map2.BuildMap(puzzleInput);
           yield return map2.MaxValue;
        }
    }
}