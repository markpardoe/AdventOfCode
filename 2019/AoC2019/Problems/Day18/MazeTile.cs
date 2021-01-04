using AoC.Common.Mapping;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Aoc.AoC2019.Problems.Day18
{
    public enum TileType
    {
        Empty = 0,
        Wall = 1,
        Door = 2,
        Key = 3
    }
    
    /// <summary>
    /// A location in the maze - with the tileType and position.
    /// </summary>
    public class MazeTile : IEquatable<MazeTile>
    {
        public TileType Tile { get; }
        public string KeyId {get;}
        public Path Parent { get; set; }

        public Position Position { get; }
        public int X => Position.X;
        public int Y => Position.Y;

        public MazeTile(int x, int y, char tile) 
        {
            Position = new Position(x, y);

            KeyId = tile.ToString().ToUpper();

            if (tile == '#') 
            {
                Tile = TileType.Wall;
            }
            else if (tile == '.' || tile == '@')
            {
                // @ is starting location of a robot - so still an empty square.
                Tile = TileType.Empty;
                KeyId = ".";
            }
            else if (tile >= 97 && tile <= 122) // lower case letters = keys
            {
                Tile =  TileType.Key;
            }
            else if (tile >= 65 && tile <= 90)  // Capital letters = doors
            {
                Tile =  TileType.Door;
            }
        }

        public override string ToString()
        {
            if (Tile == TileType.Door)
            {
                return $"Door: " + KeyId;
            }
            else if (Tile == TileType.Key)
            {
                return "Key: " + KeyId;
            }
            else return $"{base.ToString()} - {Tile.ToString()}";
        }

        // Check if 2 tiles are the same.
        // Since a tile can only contain one 'type' (wall, key, door, etc)
        // We only need to check the (x, y) co-ordinates match.
        // This also allows us to check it directly against a Position.
        public bool Equals( MazeTile other)
        {
            if (other == null) return false;
            return this.X == other.X && this.Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is MazeTile)
            {
                return this.Equals(obj as MazeTile);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public string MapValue 
        {
            get
            {
                if (Tile == TileType.Key)
                {
                    return KeyId.ToLower();
                }
                else
                {
                    return KeyId;
                }
            }

        }
    }
}
