using System;
using System.Collections.Generic;
using System.Text;

namespace AoC.AoC2020.Problems.Day17
{

    public struct Position4d : IEquatable<Position4d>
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public int W { get; }

        public Position4d(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Fetches all 26 neighboring positions
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Position4d> GetNeighbors()
        {
            var n = new HashSet<Position4d>();

            for (int w = W - 1; w <= W + 1; w++)
            {
                for (int horizontal = X - 1; horizontal <= X + 1; horizontal++)
                {
                    for (int vertical = Y - 1; vertical <= Y + 1; vertical++)
                    {
                        for (int layer = Z - 1; layer <= Z + 1; layer++)
                        {
                            n.Add(new Position4d(horizontal, vertical, layer, w));
                        }
                    }
                }
            }

            n.Remove((this));  // remove current position
            return n;
        }
        
           
        public bool Equals(Position4d other)
        {
            return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
        }

        public override bool Equals(object obj)
        {
            return obj is Position4d other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z, W);
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z}, {W})";
        }
    }
}
