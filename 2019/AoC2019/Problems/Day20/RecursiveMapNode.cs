using AoC.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aoc.AoC2019.Problems.Day20
{
    public class RecursiveMapNode :LayerPosition, IEquatable<RecursiveMapNode>
    {
        public RecursiveMapNode Parent { get; set; }

        public RecursiveMapNode(LayerPosition p) : base(p.X, p.Y, p.Layer) { }

        public Path PathTaken { get; set; }

        public int DistanceFromStart { get; set; }

        public bool Equals(RecursiveMapNode other)
        {
            if (other == null) return false;
            return this.X == other.X && this.Y == other.Y && this.Layer == other.Layer;
        }


        public override bool Equals(Object obj)
        {
            if (obj == null) return false;
            if (obj is RecursiveMapNode) return (Equals((RecursiveMapNode)obj));
            if (obj is LayerPosition) return base.Equals(obj as LayerPosition);
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.X, this.Y, this.Y);
        }
    }
}
