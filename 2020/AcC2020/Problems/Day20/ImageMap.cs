using System;
using System.Collections.Generic;
using System.Text;
using AoC.AoC2020.Problems.Day17;
using AoC.Common.Mapping;

namespace AoC.AoC2020.Problems.Day20
{

    public class ImageMap :  FixedSizeMap<bool>
    {
        public int TileId { get; }

        protected override char? ConvertValueToChar(Position position, bool value)
        {
            return value ? '#' : '.';
        }

        // Fixed size grid of 9x9
        public ImageMap(int tileId, IEnumerable<string> rawData) : base(false, new Position(0,0))
        {
            TileId = tileId;
            var y = 0;
            foreach (var line in rawData)
            {
                for (var x = 0; x < line.Length; x++)
                {

                    var value = line[x] == '#';
                    Position pos = new Position(x, y);

                    Add(pos, value);
                }
                y++;
            }
        }

        private ImageMap(int tileId) : base(false, new Position(0, 0))
        {
            TileId = tileId;
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


            return flipped;
        }

        public override string ToString()
        {
            return $"ImgMap: {TileId}  [{MaxX}x{MaxY}]";
        }
    }
}
