using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AoC.Common.Mapping;

namespace Aoc.AoC2019.Problems.Day18
{
    /// <summary>
    ///  Holds a path through the maze.
    ///  Contains the keys collected and which KeyDistances (between start --> key or key --> key) travelled.
    /// </summary>
    public class Path : IEquatable<Path>
    {       
        public Path Parent { get; set; }

        public Position Position { get; }
        public int X => Position.X;
        public int Y => Position.Y;

        public List<string> KeysCollected { get; set; }

        public List<KeyDistance> PathTraveled { get; set; } = new List<KeyDistance>();

        public string Id { get; }

        public int TotalDistance
        {
            get;  internal set;
        }

        public Path(MazeTile p,  params string[] keys)
        {
            Position = new Position(p.X, p.Y);
            KeysCollected = new List<string>(keys);
            Id = string.Join("", keys.OrderBy(c => c));
        }

        public override string ToString()
        {
            return base.ToString() + " - " +  String.Join(", ", KeysCollected) + " - " + TotalDistance;
        }

        // have they found the same keys and ended up in the same place?
        public bool Equals([AllowNull] Path other)
        {
            if (other == null) throw new ArgumentNullException();

            return other.X == this.X && other.Y == this.Y && this.Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, X, Y);
        }

        public override bool Equals(object other)
        {

            if (other == null) throw new ArgumentNullException();
            if (other is Path) return this.Equals(other as Path);
            return false;
        }
    }
}