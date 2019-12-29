using System;
using System.Collections.Generic;
using System.Text;
using AoC.Common.Mapping;
using System.Linq;

namespace AoC2019.Problems.Day24
{
    public class RecursiveGameOfLifeMap
    {
        private readonly Dictionary<int, GameOfLifeMap> layers = new Dictionary<int, GameOfLifeMap>();
      
        public RecursiveGameOfLifeMap(List<string> data)
        {
            GameOfLifeMap map = new GameOfLifeMap(data, true);
            layers.Add(0, map);  // add the initial layer

        }

        public int TotalBugs()
        {
            int total = 0;
            foreach(GameOfLifeMap map in layers.Values)
            {
                total += map.NumberOfBugs;
            }

            return total;
        }

        public string DrawMap()
        {
            StringBuilder sb = new StringBuilder();
            var keys = layers.Keys.OrderBy(p => p);
            foreach (int layer in keys)
            {
                sb.Append("Layer: ");
                sb.Append(layer);
                sb.Append(Environment.NewLine);
                sb.Append(layers[layer].DrawMap());
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        public void EvolveMap()
        {
            // check if we need a new layer
            AddInnerMap();
            AddOuterMaps();

            foreach (GameOfLifeMap map in layers.Values)
            {
                map.UpdateBuffer();
            }

            foreach (GameOfLifeMap map in layers.Values)
            {
                map.UpdateFromBuffer();
            }
        }

        private void AddInnerMap()
        {
            int innerLayer = layers.Keys.Max();
            GameOfLifeMap map = layers[innerLayer];

            // if any bugs in inner circle - we need to add a new inner map
            if (map.GetInnerEdge().Sum(t => t.NumberOfBugs) > 0)
            {
                GameOfLifeMap innerMap = new GameOfLifeMap();
                layers.Add(innerLayer + 1, innerMap);

                UpdateNeighbours(map[1, 2], innerMap.GetColumn(0));  // left
                UpdateNeighbours(map[2, 1], innerMap.GetRow(0));  // upper
                UpdateNeighbours(map[2, 3], innerMap.GetRow(4));  // bottom
                UpdateNeighbours(map[3, 2], innerMap.GetColumn(4));  // right
            }
        }

        private void AddOuterMaps()
        {
            int outerLayer = layers.Keys.Min();
            GameOfLifeMap map = layers[outerLayer];

            // Get the number of bugs in each of the outside columns.  If any of them are 1 or 2 - then we need to expand the map outwards.
            List<int> bugCounts = new List<int>() { CountBugs(map.GetColumn(0)), CountBugs(map.GetColumn(4)), CountBugs(map.GetRow(0)), CountBugs(map.GetRow(4)) };
            if (bugCounts.Contains(1) || bugCounts.Contains(2))
            {
                GameOfLifeMap outermap = new GameOfLifeMap();
                layers.Add(outerLayer - 1, outermap);

                UpdateNeighbours(outermap[1, 2], map.GetColumn(0));  // left
                UpdateNeighbours(outermap[2, 1], map.GetRow(0));  // upper
                UpdateNeighbours(outermap[2, 3], map.GetRow(4));  // bottom
                UpdateNeighbours(outermap[3, 2], map.GetColumn(4));  // right
            }
        }

        private int CountBugs(IEnumerable<GameTile> tiles)
        {
            return tiles.Sum(t => t.NumberOfBugs);
        }

        private void UpdateNeighbours(GameTile tile, List<GameTile> newTiles)
        {
            foreach (GameTile t in newTiles)
            {
                tile.Neighbours.Add(t);
                t.Neighbours.Add(tile);
            }
        }
    }
      
}
