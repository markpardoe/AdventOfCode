using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day18
{
    public sealed class SettlerMap : Map<Settler> 
    {
        private readonly HashSet<Settler> _allSettlers = new HashSet<Settler>();

        public SettlerMap(IEnumerable<string> input) : base(null)
        {
           var y = 0;
            foreach (var line in input)
            {
                for (var x = 0; x < line.Length; x++)
                {
                    // Have to cast as object first or it won't compile.
                    var tile = line[x];

                    Settler s = new Settler(x, y, (SettlerType) line[x]);
                    _allSettlers.Add(s);
                    Add(s, s);
                }
                y++;
            }

            // Add neighbors for each settler
            foreach (Settler settler in _allSettlers)
            {
                settler.AddNeighbors(settler.GetNeighboringPositionsIncludingDiagonals().Select(p => this[p]));
            }
        }

        public override string DrawMap()
        {
            int minX = MinX;
            int minY = MinY;
            int maxX = MaxX;
            int maxY = MaxY;

            StringBuilder map = new StringBuilder();
            for (int y = minY ; y <= maxY ; y++)
            {
                map.Append(Environment.NewLine);

                for (int x = minX ; x <= maxX ; x++)
                {
                    var c = this[x, y];
                    map.Append(c.Char);
                }
            }

            return map.ToString();
        }

        public int CountSettlers(SettlerType type)
        {
            return this.Values.Count(x => x.Type == type);
        }

        public void RunSimulation(long numTurns = 1)
        {
            for (int i = 0; i < numTurns; i++)
            {
                foreach (var settler in _allSettlers)
                {
                    settler.UpdateBuffer();
                }

                // Update status from the buffer for all Settlers simultaneously
                foreach (var settler in _allSettlers)
                {
                    settler.UpdateAcreFromBuffer();
                }
            }
        }

    }
}
