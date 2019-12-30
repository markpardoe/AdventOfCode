using System;

namespace Aoc.AoC2019.Problems.Day12
{
    public class Position3d : IEquatable<Position3d>
    {
        public int X { get; internal set; }
        public int Y { get; internal set; }
        public int Z { get; internal set; }

        public Position3d(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public bool Equals(Position3d other)
        {
            if (other == null) return false;
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Position3d)
            {
                return Equals(obj as Position3d);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.X, this.Y, this.Z);
        }
    }
}
