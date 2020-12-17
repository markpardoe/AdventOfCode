using System;
using System.Collections.Generic;
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

    /// <summary>
    ///  Holds compass directions - some problems give inputs as N, S, E or W
    /// </summary>
    public enum CompassDirection
    {
        North = 'N',
        South = 'S',
        West = 'W',
        East = 'E'
    }

    /// <summary>
    /// Helper class to map Compass direction (N,S,E,W) to cardinal direction (Up, Down, Left, Right)
    /// </summary>
   public static class Directions
   {
       public static Dictionary<CompassDirection, Direction> MapCompassDirectionToDirection = new Dictionary<CompassDirection, Direction>()
       {
           {CompassDirection.North, Direction.Up},
           {CompassDirection.South, Direction.Down},
           {CompassDirection.East, Direction.Right},
           {CompassDirection.West, Direction.Left}
       };
    }
}
