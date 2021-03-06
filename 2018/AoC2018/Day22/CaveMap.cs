﻿using System;
using System.Linq;
using System.Text;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day22
{
    public class CaveMap : Map<CaveRegion>
    {
        private readonly Position _target;
        private readonly int _depth;
        private readonly Position _start = new Position(0, 0);

        public CaveMap(Position target, int depth) : base(null)
        {
            _target = target;
            _depth = depth;
            GenerateMap();
        }

        private void GenerateMap()
        {
            for (int y = 0; y <= _target.Y + 2; y++)
            {
                for (int x = 0; x <= _target.X + 2; x++)
                {
                    AddNewRegion(x, y);
                }
            }
        }

        private void AddNewRegion(int x, int y)
        {
            int geo = GetGeologicalIndex(x, y);
            int erosion = (geo + _depth) % 20183;
            var region =  new CaveRegion(x, y, erosion, geo);
            this.Add( region.Position, region);
        }

        // Adds a new unexplored region to the map
        // Since the GeologicalLevel relies on neighboring cells, we need to add new rows / columns to the map
        // The map can expand infinitely, so only add these when required.
        public void ExpandRegionMap(int x, int y)
        {
            if (this[x, y] != null) return;

            int maxX = MaxX;
            int maxY = MaxY;

            // add new rows
            for (int row = maxY + 1; row <=  y ; row++)
            {
                for (int col = 0; col <= maxX; col++)
                {
                    AddNewRegion(col, row);
                }
            }

            // recache values
            maxX = MaxX;
            maxY = MaxY;

            // add new columns
            for (int col = maxX + 1; col <= x; col++)
            {
                for (int row = 0; row <= maxY; row++)
                {
                    AddNewRegion(col, row);
                }
            }
        }

        public int GetRiskLevel()
        {
            return Map.Where(x => x.Key.X >= _start.X && x.Key.X <= _target.X && x.Key.Y >= _start.Y && x.Key.Y <= _target.Y)
                .Sum(x => x.Value.RiskLevel);
        }

        private int GetGeologicalIndex(int x, int y)
        {
            if (_target.X == x && _target.Y == y)
            {
                return 0;
            }

            if (x == 0 && y == 0)
            {
                return 0;
            }

            if (x == 0)
            {
                return y * 48271;
            }

            if (y == 0)
            {
                return x * 16807;
            }

            return this[x - 1, y].ErosionLevel * this[x, y - 1].ErosionLevel;
        }


        protected override char? ConvertValueToChar(Position position, CaveRegion value)
        {
            if (_target.Equals(position))
            {
                return 'T';
            }
            else if (_start.Equals(position))
            {
               return 'M';
            }
            else
            {
                return (char) value.RegionType;
            }
        }     
    }
}