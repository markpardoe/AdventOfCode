using AoC.Common.Mapping;

namespace Aoc.AoC2019.Problems.Day03
{
    public enum LineAxis
    {
        Horizontal,
        Vertical
    }
    public class Line
    {
        public Position StartPoint { get;  }
        public Position EndPoint { get; }
        public int Distance => _vector.Distance;
        public Direction Direction => _vector.Direction;
        public LineAxis Axis { get; }

        private readonly Vector _vector;

        public Line(Position start, Vector vector)
        {
            this.StartPoint = start;
            this.EndPoint = start.Move(vector.Direction, vector.Distance);
            _vector = vector;

            if (Direction == Direction.Right || Direction == Direction.Left)
            {
                this.Axis = LineAxis.Horizontal;
            }
            else
            {
                this.Axis = LineAxis.Vertical;
            }
        }

        public Position GetCollision(Line other)
        {
            if (this.Axis == other.Axis) return null;  // we assume no collisions if running in the same direction

            if (this.Axis == LineAxis.Horizontal)
            {
                if ((HasOverlap(this.StartPoint.Y, other.StartPoint.Y, other.EndPoint.Y)) && HasOverlap(other.StartPoint.X, this.StartPoint.X, this.EndPoint.X))
                {
                    return new Position(other.StartPoint.X, this.StartPoint.Y);
                }
            }
            else
            {
                if ((HasOverlap(this.StartPoint.X, other.StartPoint.X, other.EndPoint.X)) && HasOverlap(other.StartPoint.Y, this.StartPoint.Y, this.EndPoint.Y))
                {
                    return new Position(this.StartPoint.X, other.StartPoint.Y);
                }
            }

            return null;
        }

        // Checks if a point is overlapping a continuous line v1 <---> v2
        private bool HasOverlap(int v, int v1, int v2)
        {
            if (v == v1) return true;
            if (v1 == v2) return false;

            return (v >= v1 && v <= v2) || (v <= v1 && v >= v2);
        }
    }
}
