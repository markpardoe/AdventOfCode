using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common.Mapping.Hex;

namespace AoC.AoC2020.Problems.Day24
{
    public enum TileColor
    {
        Black,
        White
    }

    public class LobbyMap : HexGrid<TileColor>
    {
        public LobbyMap(IEnumerable<string> rawData) : base(TileColor.White)
        {
            foreach (var line in rawData)
            {
                FlipTiles(line.ToLower().Trim());
            }
        }

        protected override char? ConvertValueToChar(AxialPosition position, TileColor value)
        {
            return value == TileColor.Black ? '#' : '.';
        }

        private readonly Dictionary<string, AxialDirection> _directionMapping = new Dictionary<string, AxialDirection>()
        {
            {"e", AxialDirection.East},
            {"w", AxialDirection.West},
            {"ne", AxialDirection.NorthEast},
            {"nw", AxialDirection.NorthWest},
            {"sw", AxialDirection.SouthWest},
            {"se", AxialDirection.SouthEast}
        };

        private void FlipTiles(string line)
        {
            AxialPosition current = new AxialPosition(0,0,0);

            for (int i = 0; i < line.Length; i++)
            {
                string dir = line[i].ToString();

                // need next character to complete direction
                if (dir == "s" || dir == "n")
                {
                    dir = line.Substring(i, 2);
                    i++;
                }

                AxialDirection direction = _directionMapping[dir];
                current = current.Move(direction);
            }

            // flip color if tile
            if (this[current] == TileColor.Black)
            {
                
                this[current] = TileColor.White;
            }
            else
            {
                this[current] = TileColor.Black;
            }

            //Console.WriteLine($"Flipping: {current} to {this[current]}");
        }

        public void MoveForwards()
        {
            var allPositions = this.GetBoundedEnumerator(1).ToList();
            foreach (var location in allPositions)
            {
                int blackCount = location.Key.GetNeighbors().Count(x => this[x] == TileColor.Black);

                //Console.WriteLine($"{location.Key} Blacks = {blackCount}");
                // Sparse map - we only add the black tiles
                if (location.Value == TileColor.Black && (blackCount ==1 || blackCount == 2))
                {
                    AddToBuffer(location.Key, TileColor.Black);
                }
                else if (location.Value == TileColor.White && blackCount == 2)
                {
                    AddToBuffer(location.Key, TileColor.Black);
                }
            }

            UpdateFromBuffer();
        }
    }
}
