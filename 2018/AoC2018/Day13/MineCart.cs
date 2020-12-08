using System;
using System.Collections.Generic;
using System.IO;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day13
{
    public enum CartStatus
    {
        Running,
        Crashed
    }

    public class MineCart
    {
        // Directions we can turn at an intersection
        private enum TurnDirection
        {
            Left,
            Right,
            Straight
        }

        // Mapping of which direction to turn next at an intersection
        // <Last TurnDirection, Next TurnDirection>
        private static readonly Dictionary<TurnDirection, TurnDirection> TurnDirectionMapping =
            new Dictionary<TurnDirection, TurnDirection>
            {
                {TurnDirection.Left, TurnDirection.Straight},
                {TurnDirection.Straight, TurnDirection.Right},
                {TurnDirection.Right, TurnDirection.Left},
            };


        // Convert a direction into a MineTile for display.
        private static Dictionary<Direction, MineTile> _directionMappings = new Dictionary<Direction, MineTile>
        {
            {Direction.Down, MineTile.CartDown},
            {Direction.Up, MineTile.CartUp},
            {Direction.Left, MineTile.CartLeft},
            {Direction.Right, MineTile.CartRight}
        };

        // Mapping of directions when going round a left curve <CurrentDirection, NewDirection>
        private static Dictionary<Direction, Direction> LeftCurveMapping = new Dictionary<Direction, Direction>
        {
            {Direction.Left, Direction.Down},
            {Direction.Right, Direction.Up},
            {Direction.Up, Direction.Right},
            {Direction.Down, Direction.Left}
        };


        // Mapping of directions when going round a right curve <CurrentDirection, NewDirection>
        private static Dictionary<Direction, Direction> RightCurveMapping = new Dictionary<Direction, Direction>
        {
            {Direction.Left, Direction.Up},
            {Direction.Right, Direction.Down},
            {Direction.Up, Direction.Left},
            {Direction.Down, Direction.Right}
        };


        // Mapping of directions when turning left <CurrentDirection, NewDirection>
        private static Dictionary<Direction, Direction> TurnLeftMapping = new Dictionary<Direction, Direction>
        {
            {Direction.Left, Direction.Down},
            {Direction.Right, Direction.Up},
            {Direction.Up, Direction.Left},
            {Direction.Down, Direction.Right}
        };

        // Mapping of directions when turning right <CurrentDirection, NewDirection>
        private static Dictionary<Direction, Direction> TurnRightMapping = new Dictionary<Direction, Direction>
        {
            {Direction.Left, Direction.Up},
            {Direction.Right, Direction.Down},
            {Direction.Up, Direction.Right},
            {Direction.Down, Direction.Left}
        };
        
        public CartStatus Status { get; private set; } = CartStatus.Running;

        public int X { get; private set ; }
        public int Y { get; private set; }
        public  Position CurrentPosition => new Position(X, Y);

        public Direction Facing { get; private set; }
        private TurnDirection _nextTurnDirection = TurnDirection.Left;

        public MineCart(int x, int y, Direction facing)
        {
            X = x;
            Y = y;
            Facing = facing;
        }

        public void Crash()
        {
            Status = CartStatus.Crashed;
        }

        public char Character => (char) _directionMappings[Facing];

        public void MoveCart(MineTile tile)
        {
            // Check if crashed
            if (Status == CartStatus.Crashed || tile == MineTile.Crash)
            {
                Status = CartStatus.Crashed;
                return;
            }

            // Check for invalid characters
            if (tile == MineTile.CartDown || tile == MineTile.CartLeft || tile == MineTile.CartRight ||
                tile == MineTile.CartUp || tile == MineTile.Empty)
            {
                throw new InvalidDataException($"Invalid tile for cart at: ({this.X}, {this.Y})");
            }
            else  // must be horizontal, vertical or corner
            {
                // Change direction when going around corner
                if (tile == MineTile.LeftCurve)
                {
                    Facing = LeftCurveMapping[Facing];
                }
                else if (tile == MineTile.RightCurve)
                {
                    Facing = RightCurveMapping[Facing];
                }
                else if (tile == MineTile.Intersection)
                {
                    if (_nextTurnDirection == TurnDirection.Left)
                    {
                        Facing = TurnLeftMapping[Facing];
                    }
                    else if (_nextTurnDirection == TurnDirection.Right)
                    {
                        Facing = TurnRightMapping[Facing];
                    }

                    _nextTurnDirection = TurnDirectionMapping[_nextTurnDirection];  // set the next direction to move
                }

                switch (Facing)
                {
                    case Direction.Left:
                        this.X -= 1;
                        break;
                    case Direction.Right:
                        this.X += 1;
                        break;
                    case Direction.Up:
                        this.Y -= 1;
                        break;
                    case Direction.Down:
                        this.Y += 1;
                        break;

                    default:
                        throw new InvalidDataException();
                }
            }
        }
    }
}