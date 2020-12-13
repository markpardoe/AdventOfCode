using System;
using System.Collections.Generic;
using System.Text;
using AoC.Common.Mapping;
using System.Linq;

namespace Aoc.Aoc2018.Day18
{

    public enum SettlerType
    {
        Open = '.',
        Tree = '|',
        Lumberyard = '#'
    }

    public class Settler :Position
    {
        public SettlerType Type { get; private set; }

        private readonly List<Settler> _neighbors = new List<Settler>(8);
        
        public Settler(int x, int y, SettlerType type) : base(x, y)
        {
            Type = type;
            _buffer = type;
        }

        public void AddNeighbors(IEnumerable<Settler> neighbors)
        {
            foreach (var n in neighbors)
            {
                if (n != null)
                {
                    _neighbors.Add(n);
                }   
            }
        }

        public override string ToString()
        {
            return $"{Char}({X}, {Y})";
        }

        public char Char => (char) Type;

        private SettlerType _buffer;

        public void UpdateBuffer()
        {
            if (Type == SettlerType.Open)
            {
                if (_neighbors.Count(X => X.Type == SettlerType.Tree) >= 3)
                {
                    _buffer = SettlerType.Tree;
                }
            }
            else if (Type == SettlerType.Tree)
            {
                if (_neighbors.Count(X => X.Type == SettlerType.Lumberyard) >= 3)
                {
                    _buffer = SettlerType.Lumberyard;
                }
            }
            else if (Type == SettlerType.Lumberyard)
            {
                if (_neighbors.Any(x => x.Type == SettlerType.Lumberyard) && _neighbors.Any(x => x.Type == SettlerType.Tree))
                {
                    _buffer = SettlerType.Lumberyard;
                }
                else
                {
                    _buffer = SettlerType.Open;
                }
            }
        }

        public void UpdateAcreFromBuffer()
        {
            Type = _buffer;
        }
    }
}
