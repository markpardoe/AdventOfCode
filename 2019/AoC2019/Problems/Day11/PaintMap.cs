﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AoC.Common.Mapping;

namespace Aoc.AoC2019.Problems.Day11
{
    public class PaintMap : Map<PaintColor>
    {
        public PaintMap() : base(PaintColor.Black)
        {
            DrawPadding = 2;
        }

        protected override char? ConvertValueToChar(Position position, PaintColor value)
        {
            if (value == PaintColor.Black)
            {
                return 'X';
            }

            return ' ';
        }

        public int PanelCount() => _map.Count;
    }
}