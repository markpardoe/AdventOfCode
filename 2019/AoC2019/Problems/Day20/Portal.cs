using AoC.Common.Mapping;

namespace Aoc.AoC2019.Problems.Day20
{
    public enum TileType
    {
        Path = 0,
        Wall = 1,
        Portal = 2,
        Void = 3  // unwalkable

    }

    public enum PortalType
    {
        Inside = 0,
        Outside = 1,
        Start = 2,
        Exit = 3
    }

    public class Portal
    {
        public string PortalId { get; }
        public PortalType Type { get; }
        public Position Position { get; }
        public int X => Position.X;
        public int Y => Position.Y;

        public Portal (int x, int y, string portalId, PortalType portalType) 
        {
            Position = new Position(x, y);
            PortalId = portalId;
            this.Type = portalType;
        }

        public bool IsActive(int layer)
        {
            if (layer == 0 && Type == PortalType.Outside)
            {
                return false;
            }
            else if (layer > 0 && (Type == PortalType.Exit || Type == PortalType.Start))
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return $"{PortalId} ({X},{Y}) ({Type})";
        }        
    }
}
