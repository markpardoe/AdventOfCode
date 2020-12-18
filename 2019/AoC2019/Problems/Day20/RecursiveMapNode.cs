using AoC.Common.Mapping;
using System;

namespace Aoc.AoC2019.Problems.Day20
{
    public class RecursiveMapNode : IEquatable<RecursiveMapNode>
    {
        public RecursiveMapNode Parent { get; set; }
        public Position3d Position { get; private set; }

        public int X => Position.X;
        public int Y => Position.Y;
        public int Z => Position.Z;

        public RecursiveMapNode(Position3d p)
        {
            Position = p;
        }

        public Path PathTaken { get; set; }

        public int DistanceFromStart { get; set; }

        public bool Equals(RecursiveMapNode other)
        {
            if (other == null) return false;
            return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null) return false;
            if (obj is RecursiveMapNode node) return (Equals(node));
            if (obj is Position3d position3d) return base.Equals(position3d);
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.X, this.Y, this.Y);
        }
    }
}
