using AoC.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aoc.AoC2019.Problems.Day13
{
    public class GameMap :Map<TileType>
    {
        public GameMap() : base(TileType.Empty)
        {   }

        public Position Ball { get; set; }
        public Position Paddle { get; private set; }

        // We need to keep track of changes to ball or paddle positions
        protected override void OnMapUpdated(Position key, TileType value)
        {
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