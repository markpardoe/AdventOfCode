using System;
using System.Collections.Generic;
using System.IO;

namespace AoC.Common.Mapping.Hex
{
    /// <summary>
    /// Stores Hex positions as an x,y, z axial -co-ordinate.
    /// See https://www.redblobgames.com/grids/hexagons/#coordinates
    /// Basically think of it as a slice of a 3d cube - where x + y + z = 0
    /// </summary>
    public readonly struct AxialPosition : IEquatable<AxialPosition>
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public AxialPosition(int x, int y, int z)
        {
            if (x + y + z != 0) throw new InvalidDataException("Co-ordinates must add up to zero.");
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Fetches all 6 neighboring positions
        /// See https://www.redblobgames.com/grids/hexagons/#neighbors
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AxialPosition> GetNeighbors()
        {
            foreach (AxialDirection direction in Enum.GetValues(typeof(AxialDirection)))
            {
                yield return Move(direction);
            }
        }

        /// <summary>
        /// Finds the position in the given ordinal direction.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public AxialPosition Move(AxialDirection direction, int distance = 1)
        {
            return direction switch
            {
                // East & west: Move along X & Z axis.  Keep X + Y + Z = 0
                AxialDirection.East=> new AxialPosition(X + distance, Y, Z - distance),
                AxialDirection.West => new AxialPosition(X - distance, Y, Z + distance),

                // Move left to right downwards : \
                // Move along Y & Z axis
                AxialDirection.SouthEast => new AxialPosition(X, Y + distance, Z - distance),
                AxialDirection.NorthWest => new AxialPosition(X, Y - distance, Z + distance),

                // Move right to left downwards:  /
                // Move along X & Y axis.
                AxialDirection.NorthEast => new AxialPosition(X + distance, Y - distance, Z),
                AxialDirection.SouthWest => new AxialPosition(X - distance, Y + distance, Z),
                _ => throw new InvalidOperationException("Invalid Direction."),
            };
        }


        public int DistanceToOrigin()
        {
            return DistanceTo(new AxialPosition(0, 0, 0));
        }

        // Returns the distance between 2 HexPositions
        public int DistanceTo(AxialPosition target)
        {
            // Using Axial co-ordinates, each AxialPosition is '2' apart - so divide final answer by 2
            // https://www.redblobgames.com/grids/hexagons/#distances
            return (Math.Abs(this.X - target.X) + Math.Abs(this.Y - target.Y) + Math.Abs(this.Z - target.Z)) / 2;
        }

        public bool Equals(AxialPosition other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override bool Equals(object obj)
        {
            return obj is AxialPosition other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }
}