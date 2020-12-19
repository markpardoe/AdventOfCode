using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using AoC.Common.Mapping;

namespace Aoc.AoC2019.Problems.Day11
{
    public enum TurnDirection
    {
        Left = 0,
        Right = 1
    }

    public class DirectionalPosition : IPosition
    {
        private readonly Direction _direction;
        public Position Position { get; set; }

        public int X => Position.X;
        public int Y => Position.Y;

        public DirectionalPosition(int x, int y, Direction direction)
        {
            Position = new Position(x, y);
            _direction = direction;
        }

        public DirectionalPosition Turn(TurnDirection turn)
        {
            if (turn == TurnDirection.Left)
            {
                return _direction switch
                {
                    Direction.Up => new DirectionalPosition(X-1, Y, Direction.Left),
                    Direction.Down => new DirectionalPosition(X+1, Y, Direction.Right),
                    Direction.Right => new DirectionalPosition(X, Y-1, Direction.Up),
                    Direction.Left => new DirectionalPosition(X, Y+1, Direction.Down),
                    _ => throw new ArgumentException("Invalid direction."),
                };
            }
            else if (turn == TurnDirection.Right)
            {
                return _direction switch
                {
                    Direction.Up => new DirectionalPosition(X + 1, Y, Direction.Right),
                    Direction.Down => new DirectionalPosition(X - 1, Y, Direction.Left),
                    Direction.Right => new DirectionalPosition(X, Y + 1, Direction.Down),
                    Direction.Left => new DirectionalPosition(X, Y -1, Direction.Up),
                    _ => throw new ArgumentException("Invalid direction."),
                };
            }
            else
            {
                throw new ArgumentException("Invalid turn direction.");
            }
        }

        public bool Equals(DirectionalPosition other)
        {
            return base.Equals(other);
        }
    }
}
