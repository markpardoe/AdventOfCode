using AoC.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc.AoC2019.Problems.Day24
{
    public class GameOfLifeMap : Map<GameTile>
    {
        private readonly HashSet<GameTile> _allTiles = new HashSet<GameTile>();

        public int NumberOfBugs
        {
            get
            {
                return _allTiles.Sum(t => t.NumberOfBugs);
            }
        }

        public GameOfLifeMap(List<string> map, bool isRecursive = false) : base(null)
        {        
            for (int y = 0; y < 5; y++)
            {
                char[] row = map[y].ToCharArray();

                for (int x = 0; x < 5; x++)
                {
                    bool isBug = (row[x] == '#');
                    GameTile t = new GameTile(x, y, isBug);
                    base.Add(t.Position, t);
                    _allTiles.Add(t);
                }
            }

            if (isRecursive)
            {
                base[new Position(2,2)] = new NullTile(2,2);
            }

            AddNeighbors();
        }

        // Create a new empty map
        public GameOfLifeMap() : base(null)
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    GameTile t = new GameTile(x, y, false);
                    base.Add(t.Position, t);
                    _allTiles.Add(t);
                }
            }
            base[new Position(2, 2)] = new NullTile(2, 2);
            AddNeighbors();
        }

        private void AddNeighbors()
        {
            // Add neighbours
            foreach (GameTile tile in Map.Values)
            {
                var neighbours = tile.Position.GetNeighboringPositions();
                foreach (Position pos in neighbours)
                {
                    GameTile t = base[pos];
                    if (t != null)
                    {
                        tile.Neighbours.Add(t);
                    }
                }
            }
        }

        // Only draw region of 5 squares
        public override string DrawMap()
        {
            StringBuilder sb = new StringBuilder();

            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    sb.Append(base[x, y].Code);
                }
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        public long GetBioDiversityScore()
        {
            int score = 1;
            int total = 0;
              
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (base[x,y].IsBug)
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
            foreach(GameTile tile in _allTiles)
            {
                tile.UpdateBuffer();
            }

            foreach (GameTile tile in _allTiles)
            {
                tile.UpdateFromBuffer();
            }
        }

        public List<GameTile> GetColumn(int column)
        {
            return _allTiles.Where(t => t.X == column).ToList();
        }

        public List<GameTile> GetRow(int row)
        {
            return _allTiles.Where(t => t.Y == row).ToList();
        }

        public List<GameTile> GetInnerEdge()
        {
            return new List<GameTile>() { base[2, 1], base[2, 3], base[1, 2], base[3, 2] };
        }

        public void UpdateBuffer()
        {
            foreach (GameTile tile in _allTiles)
            {
                tile.UpdateBuffer();
            }
        }

        public void UpdateFromBuffer()
        {
            foreach (GameTile tile in _allTiles)
            {
                tile.UpdateFromBuffer();
            }
        }
    }
}
