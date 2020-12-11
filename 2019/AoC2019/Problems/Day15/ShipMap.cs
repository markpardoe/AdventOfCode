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

        public ShipMap() : base(ShipTile.Unknown) 
        {
        }

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
            if (!base.ContainsKey(position))
            {
                base.Add(position, ShipTile.Unknown);
            }
        }

        public Position Oxygen
        {
            get
            {
                var b = base.Keys.Where(k => base[k] == ShipTile.OxygenSystem).ToList();
                if (b.Count == 0) return null;
                return b.First();
            }
        }

        public override IEnumerable<Position> GetAvailableNeighbours(Position position)
        {
            return position.GetNeighboringPositions().Where(p => base[p] != ShipTile.Wall);
        }

        public IEnumerable<Position> GetUnExploredLocations()
        {
            return base.Keys.Where(k => this[k] == ShipTile.Unknown);
        }

        public override string DrawMap()
        {
            int min_X = MinX;
            int min_Y = MinY;
            int max_X = MaxX;
            int max_Y = MaxY;
            int maxSize = 0;

            min_X = Math.Min(-maxSize, min_X);
            max_X = Math.Max(maxSize, max_X);
            min_Y = Math.Min(-maxSize, min_Y);
            max_Y = Math.Max(maxSize, max_Y);

            StringBuilder map = new StringBuilder();
            for (int y = min_Y; y <= max_Y; y++)
            {
                map.Append(Environment.NewLine);

                for (int x = min_X ; x <= max_X; x++)
                {                  
                    ShipTile tile = this[new Position(x, y)];
                    if (Droid.X == x  && Droid.Y == y)
                    {
                        map.Append("D");
                    }
                    else if (tile == ShipTile.Empty)
                    {
                        map.Append(" ");
                    }
                    else if (tile == ShipTile.Unknown)
                    {
                        map.Append("?");
                    }
                    else if (tile == ShipTile.Wall)
                    {
                        map.Append("#");
                    }
                    else if (tile == ShipTile.OxygenSystem)
                    {
                        map.Append("X");
                    }
                    else if (tile == ShipTile.Start)
                    {
                        map.Append("S");
                    }
                }
            }

            return map.ToString();
        }
    }
}
