using System;
using System.Collections.Generic;
using System.Text;
using AoC.Common.Mapping;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day15
{
    public enum ShipTile
    {
        Unknown = 0,
        Empty = 1,
        Wall = 2,
        OxygenSystem = 3,
        Start = 4,
    }

    public class ShipMap : Map<ShipTile>
    {
        public Position Droid { get; private set; }

        public ShipMap() : base(ShipTile.Unknown) { }

        public void MoveDroid(Position p)
        {
            Droid = p;
            AddSurroundingLocations(p);
        }

        // Once we check somewhere - add neighbours to list of unexplored locations
        private void AddSurroundingLocations(Position p)
        {
            AddUnexploredLocation(p.Move(Direction.Up));
            AddUnexploredLocation(p.Move(Direction.Down));
            AddUnexploredLocation(p.Move(Direction.Left));
            AddUnexploredLocation(p.Move(Direction.Right));
        }

        private void AddUnexploredLocation(Position position)
        {
            if (!Map.ContainsKey(position))
            { 
                Add(position, ShipTile.Unknown);
            }
        }

        public Position Oxygen
        {
            get
            {
                return Map.First(x => x.Value == ShipTile.OxygenSystem).Key;
            }
        }

        protected override IEnumerable<Position> GetAvailableNeighbors(Position position)
        {
            return position.GetNeighboringPositions().Where(p => base[p] != ShipTile.Wall);
        }

        public IEnumerable<Position> GetUnExploredLocations()
        {
            return Map.Keys.Where(k => this[k] == ShipTile.Unknown);
        }

        protected override char? ConvertValueToChar(Position position, ShipTile value)
        {
            if (Droid.X == position.X && Droid.Y == position.Y)
            {
                return 'D';
            }
            else if (value == ShipTile.Empty)
            {
                return ' ';
            }
            else if (value == ShipTile.Unknown)
            {
                return '?';
            }
            else if (value == ShipTile.Wall)
            {
                return '#';
            }
            else if (value == ShipTile.OxygenSystem)
            {
                return 'X';
            }
            else if (value == ShipTile.Start)
            {
                return 'S';
            }

            return null;
        }
    }
}
