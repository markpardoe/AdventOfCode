using System;
using System.Collections.Generic;
using Aoc.Aoc2018.Day20;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Common
{
    internal static class PositionExtensions
    {
        public static Position Move(this Position position, CompassDirection direction, int distance = 1)
        {
            return direction switch
            {
                CompassDirection.North => position.Move(Direction.Up, distance),
                CompassDirection.South => position.Move(Direction.Down, distance),
                CompassDirection.West => position.Move(Direction.Left, distance),
                CompassDirection.East => position.Move(Direction.Right, distance),
                _ => throw new InvalidOperationException("Invalid Direction."),
            };
        }

        /// <summary>
        /// Gets neighboring positions in reading order.  Ie. from top row, then left to right
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Position> GetNeighboringPositionsInReadingOrder(this Position position)
        {
            yield return position.Move(Direction.Up);
            yield return position.Move(Direction.Left);
            yield return position.Move(Direction.Right);
            yield return position.Move(Direction.Down);
        }
    }
}
