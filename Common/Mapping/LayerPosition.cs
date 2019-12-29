using System;
using System.Collections.Generic;
using System.Text;

namespace AoC.Common.Mapping
{
    /// <summary>
    /// 3D position class.  Z co-ordinate is represented by an additional 'Layer' property.
    /// </summary>
    public class LayerPosition : IEquatable<LayerPosition>
    {
        public int X { get; }
        public int Y { get; }

        public int Layer { get; }

        public Position Position { get; }

        public LayerPosition(int x, int y, int layer)
        {
            X = x;
            Y = y;
            Position = new Position(x, y);
            Layer = layer;
            if (layer < 0)
            {
                throw new InvalidOperationException("Layer must be greater than zero!");
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is LayerPosition)) { return false; }
            return Equals((LayerPosition)obj);
        }

        public override string ToString()
        {
            return $"Layer {Layer}:({X}, {Y})";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Layer);
        }

        public int DistanceTo(LayerPosition p)
        {
            return Math.Abs(this.X - p.X) + Math.Abs(this.Y - p.Y) + Math.Abs(this.Layer - p.Layer);
        }

        public LayerPosition Move(Direction direction)
        {
            return direction switch
            {
                Direction.Up => new LayerPosition(X, Y - 1, Layer),
                Direction.Down => new LayerPosition(X, Y + 1, Layer),
                Direction.Left => new LayerPosition(X - 1, Y, Layer),
                Direction.Right => new LayerPosition(X + 1, Y, Layer),
                _ => throw new InvalidOperationException("Invalid Direction."),
            };
        }

        public IEnumerable<LayerPosition> GetNeighbours()
        {
            return new List<LayerPosition>() { Move(Direction.Up), this.Move(Direction.Down), this.Move(Direction.Left), this.Move(Direction.Right) };
        }

        public bool Equals(LayerPosition other)
        {
            if (other == null) return false;
            return ((other.X == this.X) && (other.Y == this.Y) && (other.Layer == this.Layer));
        }

        public LayerPosition Copy()
        {
            return new LayerPosition(this.X, this.Y, this.Layer);
        }
    }
}
