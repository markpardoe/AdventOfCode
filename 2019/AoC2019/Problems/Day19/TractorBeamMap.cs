using AoC.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AoC2019.Problems.Day19
{
    public enum BeamStatus
    {
        Stationary = 0,
        Pulling = 1,
        Unknown = 2
    }
    public class TractorBeamMap : Map<BeamStatus>
    {

        public TractorBeamMap() : base(BeamStatus.Unknown) { }

        public override string DrawMap()
        {
            int min_X = MinX;
            int min_Y = MinY;
            int max_X = MaxX;
            int max_Y = MaxY;
           
            StringBuilder map = new StringBuilder();
            for (int y = min_Y; y <= max_Y; y++)
            {
                map.Append(Environment.NewLine);

                for (int x = min_X; x <= max_X; x++)
                {
                    BeamStatus status = this[new Position(x, y)];
                    if (status == BeamStatus.Stationary)
                    {
                        map.Append(".");
                    }
                    else if (status == BeamStatus.Unknown)
                    {
                        map.Append("?");
                    }
                    else if (status == BeamStatus.Pulling)
                    {
                        map.Append("#");
                    }                   
                }
            }

            return map.ToString();
        }

        public int CountTiles(BeamStatus status)
        {
            return this.Values.Where(p => p == status).Count();
        }
    }
}
