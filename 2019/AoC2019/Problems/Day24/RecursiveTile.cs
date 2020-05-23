using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day24
{
    public class RecursiveTile : IEquatable<RecursiveTile>
    {
        public int X { get; }
        public int Y { get; }

        public List<RecursiveTile> Neighbours { get; } = new List<RecursiveTile>();

        public int Layer { get; }

        public bool IsBug { get; private set; }

        public RecursiveTile(int x, int y, int layer, bool isBug)
        {
            X = x;
            Y = y;
            Layer = layer;
            IsBug = isBug;
            if (x >=5  || x < 0 || y >=5 || y < 0)
            {
                throw new InvalidOperationException("Invalid co-ordinates");
            }
        }

        public override string ToString()
        {
            return $"({Layer}:[{X}, {Y}]) = {IsBug}";
        }

        private bool _buffer = false;
        public void UpdateBuffer()
        {
            int neghbouringBugs = Neighbours.Count(p => p.IsBug);

            if (this.IsBug && neghbouringBugs != 1)
            {
                // Bug dies
                _buffer = false;
            }
            else if (neghbouringBugs == 1 || neghbouringBugs == 2)
            {
                // Add a bug
                _buffer = true;
            }
            else
            {
                // otherwise stays the same
                _buffer = IsBug;
            }
        }


        public void UpdateFromBuffer()
        {
            IsBug = _buffer;
        }

        #region IEquatable implementation
        public override bool Equals(object obj)
        {
            if (!(obj is RecursiveTile)) { return false; }
            return Equals((RecursiveTile)obj);
        }

        public bool Equals(RecursiveTile other)
        {
            if (other == null) return false;
            return ((other.X == this.X) && (other.Y == this.Y) && (other.Layer == this.Layer));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Layer);
        }
        #endregion
    }
}
