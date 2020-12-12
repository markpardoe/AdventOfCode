using AoC.Common.Mapping;

namespace AoC.AoC2020.Problems.Day12
{
    public class WaypointShip : Ship
    {
        private Position _waypoint = new Position(10, -1);      // way point is a position RELATIVE to the ship (ie. not absolute)

        public override Position MoveShip(ShipMovement move)
        {
            
            // simple N, S, E or W move - so move waypoint
            if (_directionMap.ContainsKey(move.Direction))
            {
                _waypoint = _waypoint.Move(_directionMap[move.Direction], move.Distance);
            }
            else if (move.Direction == ShipDirection.Forward)
            {
                // move ship towards the waypoint
                ShipPosition = new Position(ShipPosition.X + (_waypoint.X * move.Distance), ShipPosition.Y + (_waypoint.Y * move.Distance));
            }
            else if (move.Direction == ShipDirection.Right)
            {

                // Rotate waypoint around ship
                int turn = move.Distance / 90; // presume 90 degree turns

                for (int i = 0; i < turn; i++)
                {
                    _waypoint = _waypoint.RotateRight();
                }
            }
            else if (move.Direction == ShipDirection.Left)
            {
                int turn = move.Distance / 90; // presume 90 degree turns
                for (int i = 0; i < turn; i++)
                {
                    _waypoint = _waypoint.RotateLeft();
                }
            }
            //  Console.WriteLine($"Move: {move}.  Ship {_shipPosition} : Waypoint {_waypoint}");
            return ShipPosition;
        }

        public override string ToString()
        {
            return $"Ship {ShipPosition} : Waypoint {_waypoint}";
        }
    }
}