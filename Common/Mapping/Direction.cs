using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace AoC.Common.Mapping
{
    public enum Direction
    {
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4
    }

    public static class Directions
    {
        public static readonly IReadOnlyDictionary<Direction, Direction> OppositeDirection = new Dictionary<Direction, Direction>()
        {
            {Direction.Left, Direction.Right},
            {Direction.Right, Direction.Left},
            {Direction.Up, Direction.Down},
            {Direction.Down, Direction.Up},
        };
    }
}
