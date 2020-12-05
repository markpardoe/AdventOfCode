using AoC.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Aoc.AoC2019.Problems.Day18
{
    /// <summary>
    /// Path from one key to another key.
    /// Contains the shortest path and which doors need to be opened on the way.
    /// </summary>
    public class KeyDistance :IEquatable<KeyDistance>
    {
        public MazeTile Origin { get;  }
        public MazeTile Destination { get;  }
        public int Distance { get; set; }

        public HashSet<string> Doors { get; } = new HashSet<string>();

        public HashSet<string> ExtraKeys { get; } = new HashSet<string>();

        public List<Position> Path { get; }

        public KeyDistance(MazeTile start, MazeTile end, MapNode node)
        {
            Origin = start;
            Destination = end;
            Distance = node.DistanceFromStart; ;

            // Map nodes are recursive paths - starting at Destination location.
            // So iterate through it to get the path from Destination --> Origin.
            MapNode cur = node;
            List<Position> p = new List<Position>();
            while (cur.Parent != null)
            {
                cur = cur.Parent;
                p.Add(new Position(cur.X, cur.Y));
            }
            p.Reverse();  // Reverse to get path from Origin --> Destination
            Path = p;
        }


        public bool Equals([AllowNull] KeyDistance other)
        {
            if (other == null)
            {
                return false;
            }

            // We only care if the Origin & Destination are the same.
            // We only want the shortest route - so we can ignore Doors / Keys enroute.
            return Origin.Equals(other.Origin) && Destination.Equals(other.Destination);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is KeyDistance)
            {
                return Equals((KeyDistance)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Origin.GetHashCode(), Destination.GetHashCode());
        }

        public override string ToString()
        {
            return $"{Origin.ToString()} ==> {Destination.ToString()} = {Distance.ToString()}:  KeysNeeded = {string.Join(",", Doors)} :  ExtraKeys = {string.Join(",", ExtraKeys)}";
        }


    }
}
