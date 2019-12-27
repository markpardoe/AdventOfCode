using System;
using System.Collections.Generic;
using System.Text;

namespace AoC.Common.Mapping
{
    public class Position : IEquatable<Position>
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Position)) { return false; }
            return Equals((Position)obj);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public int DistanceTo(Position p)
        {
            return Math.Abs(this.X - p.X) + Math.Abs(this.Y - p.Y);
        }

        public Position Move(Direction direction)
        {
            return direction switch
            {
                Direction.Up => new Position(X, Y - 1),
                Direction.Down => new Position(X, Y + 1),
                Direction.Left => new Position(X - 1, Y),
                Direction.Right => new Position(X + 1, Y),
                _ => throw new InvalidOperationException("Invalid Direction."),
            };
        }

        public Direction FindDirection(Position target)
        {
            Console.WriteLine("");
            if (target.Y < this.Y) return Direction.Up;
            if (target.Y > this.Y) return Direction.Down;
            if (target.X > this.X) return Direction.Right;
            if (target.X < this.X) return Direction.Left;

            throw new InvalidOperationException("Invalid target for find direction!");
        }

        public IEnumerable<Position> GetNeighbours()
        {
            return new List<Position>() { Move(Direction.Up), this.Move(Direction.Down), this.Move(Direction.Left), this.Move(Direction.Right) };
        }

        public bool Equals(Position other)
        {
            if (other == null) return false;
            return ((other.X == this.X) && (other.Y == this.Y));
        }

        public Position Copy()
        {
            return new Position(this.X, Y);
        }
    }
}
