using AoC.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace AoC2019.Problems.Day20
{
    public class Path
    {
        public readonly Portal Origin;
        public readonly Portal Destination;
        public int TotalDistance { get; internal set; }

        private bool IsPortalPath;
        public Path(Portal origin, Portal endPoint, int distance, bool isPortal)
        {
            this.Origin = origin ?? throw new ArgumentNullException(nameof(origin));
            this.Destination = endPoint ?? throw new ArgumentNullException(nameof(endPoint));
            this.TotalDistance = distance;
            IsPortalPath = isPortal;
        }

        public override string ToString()
        {
            return $"{Origin.ToString()} ==> {Destination.ToString()}: {TotalDistance}";
        }

        public int LayerModifier
        {
            get
            {
                if (IsPortalPath)
                {
                    if (Origin.Type == PortalType.Inside)
                    {
                        //Inside portals move up 1 layer
                        return 1;
                    }
                    else if (Origin.Type == PortalType.Outside)
                    {
                        // outside moves down 1 layer
                        return -1;
                    }
                    return 0;
                }

                // if we're not going through the portal - return 0;
                return 0;
            }
        }
    }
}
