using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common.Mapping;

namespace Aoc.AoC2019.Problems.Day13
{
    public class GameMap :Map<TileType>
    {
        public GameMap() : base(TileType.Empty) { }

        public Position Ball { get; set; }
        public Position Paddle { get; private set; }

        public override void Add(Position key, TileType value)
        {
            this.AddOrReplace(key, value);
        }

        public override TileType this[Position position]
        {
            get
            {
                if (!base.ContainsKey(position))
                {
                    return TileType.Empty;
                }
                else
                {
                    return base[position];
                }
            }
            set
            {
                this.AddOrReplace(position, value);
            }
        }

        private void AddOrReplace(Position key, TileType value)
        {
            if (base.ContainsKey(key))
            {
                base[key] = value;
            }
            else
            {
                base.Add(key, value);
            }

            if (value == TileType.Ball)
            {
                this.Ball = key;
            }
            if (value == TileType.Paddle)
            {
                this.Paddle = key;
            }
        }

        public override string DrawMap()
        {
            int min_X = MinX;
            int min_Y = MinY;
            int max_X = MaxX;
            int max_Y = MaxY;

            StringBuilder map = new StringBuilder();
            for (int y = min_Y - 2; y <= max_Y + 2; y++)
            {
                map.Append(Environment.NewLine);

                for (int x = min_X - 2; x <= max_X + 2; x++)
                {
                    TileType tile = this[new Position(x, y)];
                    if (tile == TileType.Empty)
                    {
                        map.Append(" ");
                    }
                    else if (tile == TileType.Block)
                    {
                        map.Append("B");
                    }
                    else if (tile == TileType.Ball)
                    {
                        map.Append("@");
                    }
                    else if (tile == TileType.Paddle)
                    {
                        map.Append("=");
                    }
                    else if (tile == TileType.Wall)
                    {
                        map.Append("#");
                    }
                }
            }

            return map.ToString();
        }

        public override IEnumerable<Position> GetAvailableNeighbours(Position position)
        {
            throw new NotImplementedException();
        }
    }
}
