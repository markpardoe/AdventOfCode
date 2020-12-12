using System;
using System.Collections.Generic;
using System.Text;
using AoC.Common.Mapping;

namespace AoC.AoC2020.Problems.Day12
{
    /// <summary>
    /// Extension methods for rotating a Position around the origin (0,0)
    /// Remember, the Y co-ordinate is in a downwards direction.  Eg.  -5 = 5 UPWARDS from 0
    /// </summary>
    public static class PositionExtensions
    {

        public static Position RotateLeft(this Position position)
        {
            return new Position(position.Y, position.X * -1);
            
        }

        public static Position RotateRight(this Position position)
        {
            return new Position(position.Y * -1, position.X);
        }
    }
}
