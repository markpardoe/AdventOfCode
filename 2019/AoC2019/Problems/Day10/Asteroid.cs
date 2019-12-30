using System;
using System.Collections.Generic;
using System.Text;

namespace Aoc.AoC2019.Problems.Day10
{
    public enum VisibilityStatus
    {
        NotChecked = 0,
        Visible = 1,
        Hidden = 2,
        Asteroid = 3
    }
    public class Asteroid
    {
        public int X { get; }  // column
        public int Y { get; }  // row


        public Asteroid(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
