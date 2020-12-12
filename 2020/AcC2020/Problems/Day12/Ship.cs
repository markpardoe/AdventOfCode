using System.Collections.Generic;
using AoC.Common.Mapping;

namespace AoC.AoC2020.Problems.Day12
{
    public class Ship
    {
        public Position ShipPosition { get; protected set; } = new Position(0, 0);

        protected readonly Dictionary<ShipDirection, Direction> _directionMap = new Dictionary<ShipDirection, Direction>()
        {
            {ShipDirection.East, Direction.Right},
            {ShipDirection.West, Direction.Left},
            {ShipDirection.North, Direction.Up},
            {ShipDirection.South, Direction.Down}
        };

        // Change facing of the ship after a left turn
        private readonly Dictionary<Direction, Direction> LeftTurn = new Dictionary<Direction, Direction>()
        {
            {Direction.Left, Direction.Down},
            {Direction.Down, Direction.Right},
            {Direction.Right, Direction.Up},
            {Direction.Up, Direction.Left}
        };

        // Change facing of the ship after a right turn
        private readonly Dictionary<Direction, Direction> RightTurn = new Dictionary<Direction, Direction>()
        {
            {Direction.Left, Direction.Up},
            {Direction.Down, Direction.Left},
            {Direction.Right, Direction.Down},
            {Direction.Up, Direction.Right}

        };
        
        private Direction _facing = Direction.Right;

        
        /// <summary>
        /// Moves the ship and returns the new Position
        /// </summary>
        /// <returns></returns>
        public virtual Position MoveShip(ShipMovement move)
        {
            // simple N, S, E or W move
            if (_directionMap.ContainsKey(move.Direction))
            {
                ShipPosition = ShipPosition.Move(_directionMap[move.Direction], move.Distance);
            }
            else if (move.Direction == ShipDirection.Forward)
            {
                ShipPosition = ShipPosition.Move(_facing, move.Distance);
            }
            else if (move.Direction == ShipDirection.Right)
            {
                int turn = move.Distance / 90; // presume 90 degree turns

                for (int i = 0; i < turn; i++)
                {
                    _facing = RightTurn[_facing];
                }
            }
            else if (move.Direction == ShipDirection.Left)
            {
                int turn = move.Distance / 90; // presume 90 degree turns
                for (int i = 0; i < turn; i++)
                {
                    _facing = LeftTurn[_facing];
                }
            }

            return ShipPosition;
        }

        public override string ToString()
        {
            return $"Ship {ShipPosition}";
        }
    }
}