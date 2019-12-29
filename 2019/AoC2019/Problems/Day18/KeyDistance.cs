using AoC.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AoC2019.Problems.Day18
{
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

            MapNode cur = node;
            List<Position> p = new List<Position>();
            while (cur.Parent != null)
            {
                cur = cur.Parent;
                p.Add(new Position(cur.X, cur.Y));
            }
            p.Reverse();
            Path = p;
        }

        public bool Equals([AllowNull] KeyDistance other)
        {
            if (other == null)
            {
                return false;
            }
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
