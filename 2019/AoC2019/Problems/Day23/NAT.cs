using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Aoc.AoC2019.Problems.Day23
{
    public class NAT
    {
        public long X { get; private set; }
        public long Y { get; private set; }

        public void SetCache(long x, long y)
        {
            this.X = x;
            this.Y = y;            
        }
    }
}
