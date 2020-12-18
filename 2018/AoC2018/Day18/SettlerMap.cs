using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day18
{
    public enum SettlerType
    {
        Open = '.',
        Tree = '|',
        Lumberyard = '#',
        Unknown = ' '  // outside map boundaries
    }

    public sealed class SettlerMap : FixedSizeMap<SettlerType> 
    {
        public SettlerMap(IEnumerable<string> input) : base(SettlerType.Unknown, new Position(0, 0))
        {
           var y = 0;
            foreach (var line in input)
            {
                for (var x = 0; x < line.Length; x++)
                {
                    // Have to cast as object first or it won't compile.
                    var settler = (SettlerType) line[x];

                    Add(x, y, settler);
                }
                y++;
            }
        }

        protected override char? ConvertValueToChar(Position position, SettlerType value)
        {
            return (char) value;
        }


        public void RunSimulation(long numTurns = 1)
        {
            for (long i = 0; i < numTurns; i++)
            {

                foreach (var settler in GetBoundedEnumerator())
                {
                    var neighbors = settler.Key.GetNeighboringPositionsIncludingDiagonals();
                    AddToBuffer(settler.Key, GetNewType(settler.Value, neighbors.ToList()));

                }

                UpdateFromBuffer();
            }
        }

        private SettlerType GetNewType(SettlerType current, List<Position> neighbors)
        {
            if (current == SettlerType.Open)
            {
                if (neighbors.Count(x => this[x] == SettlerType.Tree) >= 3)
                {
                   return SettlerType.Tree;
                }
            }
            else if (current == SettlerType.Tree)
            {
                if (neighbors.Count(x => this[x] == SettlerType.Lumberyard) >= 3)
                {
                    return SettlerType.Lumberyard;
                }
            }
            else if (current == SettlerType.Lumberyard)
            {
                if (neighbors.Any(x => this[x] == SettlerType.Lumberyard) &&
                    neighbors.Any(x => this[x] == SettlerType.Tree))
                {
                    return SettlerType.Lumberyard;
                }
                else
                {
                    return SettlerType.Open;
                }
            }

            return current;
        }

    }
}