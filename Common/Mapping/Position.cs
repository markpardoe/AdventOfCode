﻿using System;
using System.Collections.Generic;


namespace AoC.Common.Mapping
{
    /// <summary>
    /// Represents a Position in a 2D space.
    /// Assumes a co-ordinate system with 0,0 in top left corner - and Y increments in a downwards direction
    /// </summary>
    public readonly struct Position : IEquatable<Position>
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Returns the manhattan distance to another position.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int DistanceTo(Position p)
        {
            return Math.Abs(this.X - p.X) + Math.Abs(this.Y - p.Y);
        }

        /// <summary>
        /// Returns the manhattan distance to another position
        /// represented as a pair of X, Y co-ordinates
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int DistanceTo(int x, int y)
        {
            return Math.Abs(this.X - x) + Math.Abs(this.Y - y);
        }

        /// <summary>
        /// Return the distance from the position (0,0)
        /// </summary>
        /// <returns></returns>
        public int DistanceFromOrigin()
        {
            return Math.Abs(this.X) + Math.Abs(this.Y);
        }

        /// <summary>
        /// Finds the position in the given ordinal direction.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public Position Move(Direction direction, int distance = 1)
        {
            return direction switch
            {
                Direction.Up => new Position(X, Y - distance),
                Direction.Down => new Position(X, Y + distance),
                Direction.Left => new Position(X - distance, Y),
                Direction.Right => new Position(X + distance, Y),
                _ => throw new InvalidOperationException("Invalid Direction."),
            };
        }

        /// <summary>
        /// Returns the direction of a specified Position relative to itself
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public Direction FindDirection(Position target)
        {
            if (target.Y < this.Y) return Direction.Up;
            if (target.Y > this.Y) return Direction.Down;
            if (target.X > this.X) return Direction.Right;
            if (target.X < this.X) return Direction.Left;

            throw new InvalidOperationException("Invalid target for find direction!");
        }

        public IEnumerable<Position> GetNeighboringPositions()
        {
            yield return Move(Direction.Up);
            yield return Move(Direction.Down);
            yield return Move(Direction.Left);
            yield return Move(Direction.Right);
        }

        /// <summary>
        /// Gets all 8 neighboring positions - includes diagonals.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Position> GetNeighboringPositionsIncludingDiagonals()
        {

            yield return new Position(this.X, this.Y + 1);
            yield return new Position(this.X, this.Y - 1);
            yield return new Position(this.X + 1, this.Y);
            yield return new Position(this.X + 1, this.Y + 1);
            yield return new Position(this.X + 1, this.Y - 1);
            yield return new Position(this.X - 1, this.Y);
            yield return new Position(this.X - 1, this.Y + 1);
            yield return new Position(this.X - 1, this.Y - 1);
        }

        public bool Equals(Position other)
        {
            return ((other.X == this.X) && (other.Y == this.Y));
        }

        public Position Copy()
        {
            return new Position(X, Y);
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
    }
}