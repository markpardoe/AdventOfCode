using System;
using System.Collections.Generic;

namespace AoC.Common.Mapping._3d
{
    public readonly struct Position3d : IEquatable<Position3d>
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        
        public Position3d(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Fetches all 26 neighboring positions
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Position3d> GetNeighbors()
        {
            var n = new HashSet<Position3d>();

            for (int horizontal = X - 1; horizontal <= X + 1; horizontal++)
            {
                for (int vertical = Y - 1; vertical <= Y + 1; vertical++)
                {
                    for (int layer = Z - 1; layer <= Z + 1; layer++)
                    {
                        n.Add(new Position3d(horizontal, vertical, layer));
                    }
                }
            }

            n.Remove((this));  // remove current position
            return n;
        }

        // Gets all the neighbors on the same layer (x, y)
        public IEnumerable<Position3d> GetNeighboursOnLayer()
        {
            var n = new HashSet<Position3d>();

            for (int horizontal = X - 1; horizontal <= X + 1; horizontal++)
            {
                for (int vertical = Y - 1; vertical <= Y + 1; vertical++)
                {
                    n.Add(new Position3d(horizontal, vertical, Z));

                }
            }

            n.Remove((this));  // remove current position
            return n;
        }

        public int DistanceTo(Position3d target)
        {
            return Math.Abs(this.X - target.X) + Math.Abs(this.Y - target.Y) + Math.Abs(this.Z - target.Z);
        }
           
        public bool Equals(Position3d other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override bool Equals(object obj)
        {
            return obj is Position3d other && Equals(other);
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
