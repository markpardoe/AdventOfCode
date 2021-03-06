﻿using System;
using System.Collections.Generic;
using System.Text;
using AoC.AoC2020.Problems.Day17;
using AoC.Common;
using AoC.Common.Mapping;

namespace AoC.AoC2020.Problems.Day20
{
    public enum WaterPixel
    {
        Water ='#',
        Empty = '.',
        Monster = 'O'
    }

    public class ImageMap :  FixedSizeMap<WaterPixel>
    {
        public int TileId { get; }
        public bool IsFlipped { get; private set; } = false;

        // Cache a string representing one side of the map
        private readonly Dictionary<Direction, string> _edges = new Dictionary<Direction, string>();

        public string GetSide(Direction direction)
        {
            if (_edges.Count == 0)
            {
                CacheEdges();
            }
            return _edges[direction];
        }

        public string GetRowState(int row)
        {
            StringBuilder sb = new StringBuilder();
            for (int x = 0; x <= MaxX; x++)
            {
                sb.Append((char) this[x, row]);
            }

            return sb.ToString();
        }

        protected override char? ConvertValueToChar(Position position, WaterPixel value)
        {
            return (char) value;
        }

        // Fixed size grid of 9x9
        public ImageMap(int tileId, IEnumerable<string> rawData) : base(WaterPixel.Empty, new Position(0,0))
        {
            TileId = tileId;
            var y = 0;
            foreach (var line in rawData)
            {
                for (var x = 0; x < line.Length; x++)
                {
                    var value = (WaterPixel) line[x];
                    Position pos = new Position(x, y);

                    Add(pos, value);
                }
                y++;
            }

            CacheEdges();
        }

        public ImageMap(int tileId) : base(WaterPixel.Empty, new Position(0, 0))
        {
            TileId = tileId;
        }

        // Cache the state of each edge.
        // They shouldn't change - and can be used to check if 2 edges will join correctly
        // ie. they match
        private void CacheEdges()
        {
            string up, down, left, right;
            up = down = left = right = string.Empty;

            for (int i = 0; i <= MaxX; ++i)
            {
                up = up +  (char) this[i, 0];
                down = down + (char) this[i, MaxY];
                right = right + (char) this[MaxX, i];
                left = left + (char) this[0, i];
            }

            _edges.Add(Direction.Up, up);
            _edges.Add(Direction.Down, down);
            _edges.Add(Direction.Left, left);
            _edges.Add(Direction.Right, right);
        }

        public ImageMap RotateRight()
        {
            ImageMap rotated = new ImageMap(TileId);

            for (int i = 0; i <= MaxX; ++i)
            {
                for (int j = 0; j <= MaxY; ++j)
                {
                    rotated.Add(i, j, this[j, MaxX - i]);
                }
            }

            return rotated;
        }

        public ImageMap Flip()
        {
            ImageMap flipped = new ImageMap(TileId);

            for (int i = 0; i <= MaxX; i++)
            {
                for (int j = 0; j <= MaxY; j++)
                {
                    flipped.Add(new Position(i, j), this[MaxX - i, j]);
                }
            }

            flipped.IsFlipped = true;
            return flipped;
        }

        public override string ToString()
        {
            return $"ImgMap: {TileId}  [{MaxX}x{MaxY}]";
        }
    }
}
