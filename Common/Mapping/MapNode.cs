using System;
using System.Collections.Generic;
using System.Text;

namespace AoC.Common.Mapping
{
    public class MapNode : Position, IEquatable<MapNode>
    {
        public MapNode Parent { get; set; }

        public MapNode(Position p, MapNode parent = null, int distanceFromStart = 0 ) : base(p.X, p.Y)
        {
            DistanceFromStart = distanceFromStart;
            Parent = parent;
        }

        public int DistanceFromStart { get; set; } = 0;
        public int CalculatedDistanceToTarget { get; set; } = 0;
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


        // Returns the first non-null Parent node recursively.
        // if no parent node - returns itself.
        public MapNode GetFirstNode()
        {
            MapNode node = this;

            while (node.Parent != null)
            {
                node = node.Parent;
            }

            return node;
        }
    }
}