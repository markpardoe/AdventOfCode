using AoC.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aoc.AoC2019.Problems.Day13
{
    public class GameMap :Map<TileType>
    {
        public GameMap() : base(TileType.Empty)
        {
            this.DrawPadding = 2;  // Draw 2 spaces around the map when printing it
        }

        public Position Ball { get; set; }
        public Position Paddle { get; private set; }

        // Override default implementation of AddOrReplace as we need to
        // keep track of the location of the ball & paddle in separate variables
        protected override void AddOrReplace(Position key, TileType value)
        {
            if (_map.ContainsKey(key))
            {
                _map[key] = value;
            }
            else
            {
                _map.Add(key, value);
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

        private readonly Dictionary<TileType, char> _tileTypeCharMapping = new Dictionary<TileType, char>()
        {
            {TileType.Empty, ' '},
            {TileType.Block, 'B'},
            {TileType.Paddle, '='},
            {TileType.Ball, '@'},
            {TileType.Wall, '#'},
        };

        protected override char? ConvertValueToChar(Position position, TileType value)
        {
            return _tileTypeCharMapping[value];
        }
    }
}