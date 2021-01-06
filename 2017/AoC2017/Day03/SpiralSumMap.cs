using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC.Common.Mapping;

namespace AoC2017.Day03
{
    public class SpiralSumMap : SpiralMap
    {
        protected override int GetCurrentValue(int value, Position position)
        {
            return position.GetNeighboringPositionsIncludingDiagonals().Select(x => this[x]).Sum();
        }

        public int MaxValue => Map.Values.Max();

    }
}
