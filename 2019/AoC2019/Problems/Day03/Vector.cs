using System;
using System.Collections.Generic;
using System.Text;
using AoC.Common.Mapping;

namespace AoC2019.Problems.Day03
{
    public struct Vector
    {
        public Direction Direction { get; }
        public int Distance { get;  }

        public Vector(Direction direction, int dist)
        {
            this.Direction = direction;
            this.Distance = dist;
        }

        public Vector(char direction, int dist)
        {            
            this.Distance = dist;

            Direction = direction switch
            {
                'U' => Direction.Up,
                'D' => Direction.Down,
                'L' => Direction.Left,
                'R' => Direction.Right,
                _ => throw new ArgumentException(nameof(direction)),
            };
        }

        public Vector(string vector) : this(vector[0], vector.Substring(1)) { }

        public Vector(char direction, string dist) : this(direction, Int32.Parse(dist)) { }
    }
}
