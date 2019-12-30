using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Aoc.Aoc2018.Day07
{
    public class Node :IEquatable<Node>
    {
        public string  NodeId { get; }
        public readonly List<Node> Parents = new List<Node>();
        public readonly List<Node> Children = new List<Node>();
        public int Time { get; }


        public Node(string Id)
        {
            this.NodeId = Id;
            char c = Id.ToUpper()[0];
            Time = 60 + (c - 64);
        }

        public bool Equals([AllowNull] Node other)
        {
            if (other == null) return false;
            return this.NodeId.Equals(other.NodeId);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null) return false;
            if (obj is Node) return this.Equals(obj as Node);
            return false;
        }

        public override int GetHashCode()
        {
            return this.NodeId.GetHashCode();
        }

        public override string ToString()
        {
            return NodeId;
        }
    }
}
