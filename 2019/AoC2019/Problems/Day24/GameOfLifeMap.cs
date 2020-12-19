using AoC.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc.AoC2019.Problems.Day24
{
    public enum BugTile
    {
        Bug = '#',
        Empty = '.',
        Centre = '?',
    }

    public class GameOfLifeMap : FixedSizeMap<BugTile>
    {
        public int NumberOfBugs => CountValue(BugTile.Bug);

        public GameOfLifeMap(List<string> map) : base(BugTile.Empty, new Position(0,0), new Position(4,4))
        {        
            int y = 0;
            foreach (string line in map)
            {
                for (int x = 0; x< line.Length; x++)
                {
                    Position pos = new Position(x, y);
                    BugTile tile = (BugTile) line[x];

                    // Only add bug tiles - empty tiles will be returned automatically
                    if (tile == BugTile.Bug)
                    {
                        this.Add(pos, tile);
                    }
                }
                y++;
            }
        }

        // Create a new empty map
        public GameOfLifeMap() : base(BugTile.Empty)
        {
            this[new Position(2, 2)] =  BugTile.Centre;
        }

        protected override char? ConvertValueToChar(Position position, BugTile value)
        {
            return (char)value;
        }


        public long GetBioDiversityScore()
        {
            int score = 1;
            int total = 0;
              
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (this[x,y] == BugTile.Bug)
                    {
                        total += score;
                    }
                    score *= 2;
                }
            }
            return total;
        }

        public void EvolveMap()
        {
            foreach (var tile in GetBoundedEnumerator())
            {
                var bugs = tile.Key.GetNeighboringPositions().Count(x => this[x] == BugTile.Bug);


                if (tile.Value == BugTile.Bug && bugs == 1)
                {
                    AddToBuffer(tile.Key, BugTile.Bug);
                }
                else if (tile.Value == BugTile.Empty && (bugs == 1 || bugs == 2))
                {
                    AddToBuffer(tile.Key, BugTile.Bug);
                }
            }
            UpdateFromBuffer();
        }             
    }
}