using AoC.Common.Mapping._3d;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc.AoC2019.Problems.Day24
{
    public class RecursiveGameOfLifeMap : Map3d<BugTile>
    {
        // Set fixed grid sizes
        public override int MinX => 0;
        public override int MaxX => 4;
        public override int MinY => 0;
        public override int MaxY => 4;

        private int _minZ = 0;
        private int _maxZ = 0;
        public override int MaxZ => _maxZ;
        public override int MinZ => _minZ;


        public RecursiveGameOfLifeMap(List<string> data) : base(BugTile.Empty)
        {
            int y = 0;
            foreach (string line in data)
            {
                for (int x = 0; x < line.Length; x++)
                {
                    Position3d pos = new Position3d(x, y, 0);
                    BugTile tile = (BugTile)line[x];

                    // Only add bug tiles - empty tiles will be returned automatically
                    if (tile == BugTile.Bug)
                    {
                        this.Add(pos, tile);
                    }
                }
                y++;
            }

            this[new Position3d(2, 2, 0)] = BugTile.Centre;
        }

        public int TotalBugs()
        {
            return CountValue(BugTile.Bug);
        }

        protected override char? ConvertValueToChar(Position3d position, BugTile value)
        {
            return (char) value;
        }

        private int CountBugsInColumn(int column, int layer)
        {
            return Map.Keys.Where(p => p.X == column && p.Z == layer).Count(p => this[p] == BugTile.Bug);
        }

        private int CountBugsInRow(int row, int layer)
        {
            return Map.Keys.Where(p => p.Y == row && p.Z == layer).Count(p => this[p] == BugTile.Bug);
        }

        // Get the 4 positions around the inner square for a z-layer
        private int CountBugsOnInnerEdge(int z)
        {
            return GetNeighbors(new Position3d(2, 2, z)).Count(x => this[x] == BugTile.Bug);
        }

        public void EvolveMap()
        {
            // Check if we need to add inner or outer levels
            AddInnerMap();
            AddOuterMap();

            foreach (var location in GetBoundedEnumerator())
            {
                UpdatePosition(location.Key, location.Value);
            }

            //// check one layer above / below the current grid
            //// If new laters are added - the Z Axis will update automatically
            //// X and Y axis should stay the same
            //for (int z = MinZ - 1; z <= MaxZ + 1; z++)
            //{
            //    for (int y = MinY; y <= MaxY; y++)
            //    {
            //        for (int x = MinX; x <= MaxX; x++)
            //        {
            //            var position = new Position3d(x, y, z);
            //            UpdatePosition(position);
            //        }
            //    }
            //}


            UpdateFromBuffer();

            // Grow the map if needed
            _minZ = Map.Keys.Min(p => p.Z);
            _maxZ = Map.Keys.Max(p => p.Z);
        }

        // Gets all 8 neighbors on the same layer (x, y)
        private IEnumerable<Position3d> GetNeighbors(Position3d pos)
        {
            yield return new Position3d(pos.X - 1, pos.Y, pos.Z);
            yield return new Position3d(pos.X + 1, pos.Y, pos.Z);
            yield return new Position3d(pos.X, pos.Y - 1, pos.Z);
            yield return new Position3d(pos.X, pos.Y + 1, pos.Z);
        }

        private void UpdatePosition(Position3d position, BugTile tile)
        {

            if (position.X == 2 && position.Y == 2)
            {
                AddToBuffer(position, BugTile.Centre);
            }
            
            int bugCount = GetNeighbors(position).Count(p => this[p] == BugTile.Bug);

            // Get inner squares
            if (position.X == 2)
            {
                if (position.Y == 1)
                {
                    bugCount += CountBugsInRow(0, position.Z + 1);
                }
                else if (position.Y == 3)
                {
                    bugCount += CountBugsInRow(4, position.Z + 1);
                }
            }
            else if (position.Y == 2)
            {
                if (position.X == 1)
                {
                    bugCount += CountBugsInColumn(0, position.Z + 1);
                }
                else if (position.X == 3)
                {
                    bugCount += CountBugsInColumn(4, position.Z + 1);
                }
            }

            // Get counts from next layer up if on outer edge of square
            if (position.X == 0)
            {
                bugCount += this[1, 2, position.Z - 1] == BugTile.Bug ? 1 : 0;
            }
            else if (position.X == 4)
            {
                bugCount += this[3, 2, position.Z - 1] == BugTile.Bug ? 1 : 0;
            }
        
            if (position.Y == 0)
            {
                bugCount += this[2, 1, position.Z - 1] == BugTile.Bug ? 1 : 0;
            }
            else if (position.Y == 4)
            {
                bugCount += this[2, 3, position.Z - 1] == BugTile.Bug ? 1 : 0;
            }

            if (tile == BugTile.Bug && bugCount == 1)
            {
                AddToBuffer(position, BugTile.Bug);
            }
            else if (tile == BugTile.Empty && (bugCount == 1 || bugCount == 2))
            {
                AddToBuffer(position, BugTile.Bug);
            }
        }

        private void AddOuterMap()
        {
            
            if (CountBugsInColumn(0, MinZ) > 0 || CountBugsInColumn(4, MinZ) >0 || CountBugsInRow(0, MinZ) > 0 || CountBugsInRow(4, MinZ) > 0)
            {
                _minZ--;
            }
        }

        private void AddInnerMap()
        {
            int bugs = CountBugsOnInnerEdge(MaxZ);

            // if any bugs in inner circle - we need to add a new inner map
            if (bugs > 0)
            {
                // have to add a new lower level
                _maxZ++;
            }
        }
    }
}
