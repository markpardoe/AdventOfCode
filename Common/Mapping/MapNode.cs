using System;
using System.Collections.Generic;
using System.Text;

namespace AoC.Common.Mapping
{
    public class MapNode : Position, IEquatable<MapNode>
    {
        public MapNode Parent { get; set; }

        public MapNode(Position p) : base(p.X, p.Y) { }

        public int DistanceFromStart { get; set; }
        public int CalculatedDistanceToTarget { get; set; }
        public int TotalDistance => DistanceFromStart + CalculatedDistanceToTarget;

        public bool Equals(MapNode other)
        {
            if (other == null) return false;
            return this.X == other.X && this.Y == other.Y;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null) return false;
            if (obj is Position) return (Equals((Position)obj));
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.X, this.Y);
        }
    }
}