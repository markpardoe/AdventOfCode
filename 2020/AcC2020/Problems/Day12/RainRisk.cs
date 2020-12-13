using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;
using AoC.Common.Mapping;

namespace AoC.AoC2020.Problems.Day12
{
    public enum ShipDirection
    {
        Forward = 'F',
        North = 'N',
        South = 'S',
        East = 'E',
        West = 'W',
        Left = 'L',
        Right = 'R'
    }

    public class RainRisk :AoCSolution<int>
    {
        public override int Year => 2020;
        public override int Day => 12;
        public override string Name => "Day 12: Rain Risk";
        public override string InputFileName => "Day12.txt";

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            var moves = GenerateMovements(input);
            var ship = new Ship();

            var result = RunShipSimulation(ship, moves);
            yield return result.DistanceFromOrigin();

            WaypointShip waypoint = new WaypointShip();
            var result2 = RunShipSimulation(waypoint, moves);
            yield return result2.DistanceFromOrigin();
            //throw new NotImplementedException();
        }
        
        // Convert input into a list of ship movements
        private IEnumerable<ShipMovement> GenerateMovements(IEnumerable<string> input)
        {
            List<ShipMovement> moves = new List<ShipMovement>();
            foreach (var line in input)
            {
                moves.Add(new ShipMovement(line));
            }

            return moves;
        }

        public Position RunShipSimulation(Ship ship, IEnumerable<ShipMovement> moves)
        {
            foreach (var move in moves)
            {
                ship.MoveShip(move);
            }

            return ship.ShipPosition;
        }
    }

    public class ShipMovement
    {
        public int Distance;
        public ShipDirection Direction;

        public ShipMovement(string input)
        {
            Direction = (ShipDirection) input[0];
            Distance = int.Parse(input.Substring(1));
        }

        public override string ToString()
        {
            return $"{(char) Direction}{Distance}";
        }
    }
}
