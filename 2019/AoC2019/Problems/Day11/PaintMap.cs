using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AoC.Common.Mapping;

namespace Aoc.AoC2019.Problems.Day11
{
    public class PaintMap : Map<PaintColor>
    {
        public PaintMap() :base(PaintColor.Black) { }

        public override string DrawMap()
        {
            int min_X = MinX;
            int min_Y = MinY;
            int max_X = MaxX;
            int max_Y = MaxY;

            StringBuilder map = new StringBuilder();
            for (int y = min_Y - 2; y <= max_Y + 2; y++)
            { 
                map.Append(Environment.NewLine);
                
                for (int x = min_X - 2; x <= max_X + 2; x++)
                {
                    PaintColor color = this[new Position(x, y)];
                    if (color == PaintColor.Black)
                    {
                        map.Append("X");
                    }
                    else
                    {
                        map.Append(" ");
                    }
                }
            }
            return map.ToString();
        }
    }
}
