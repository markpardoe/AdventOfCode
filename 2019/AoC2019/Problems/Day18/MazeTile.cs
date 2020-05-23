﻿using AoC.Common.Mapping;
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
    

    public class MazeTile : Position, IEquatable<MazeTile>, IEquatable<Position>
    {
        public TileType Tile { get; }
        public string KeyId {get;}

        public MazeTile(int x, int y, char tile) :base(x,y)
        {
            KeyId = tile.ToString().ToUpper();

            if (tile == '#') 
            {
                Tile = TileType.Wall;
            }
            else if (tile == '.' || tile == '@')
            {
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

        public bool Equals([AllowNull] MazeTile other)
        {
            if (other == null) return false;
            return this.X == other.X && this.Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Position)
            {
                return this.Equals(obj as Position);
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
