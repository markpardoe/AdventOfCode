using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AoC.Common.Mapping
{

    // Helper class for caching map boundaries.
    // Calculating them is expensive on large maps - so we don't want to recalculate more than neccessary
    public class MapBoundary : IEnumerable<Position>
    {
        public readonly int MaxX;
        public readonly int MaxY;
        public readonly int MinX;
        public readonly int MinY;

        public MapBoundary(IReadOnlyCollection<Position> keys, int padding = 0)
        {
            MaxX = keys.Max(p => p.X) + padding;
            MinX = keys.Min(p => p.X) - padding;
            MaxY = keys.Max(p => p.Y) + padding;
            MinY = keys.Min(p => p.Y) - padding;
        }

        public IEnumerator<int> GetYEnumerator()
        {
            for (int y = MinY; y <= MaxY; y++)
            {
                yield return y;
            }
        }

        public IEnumerator<int> GetXEnumerator()
        {
            for (int x = MinX; x <= MaxX; x++)
            {
                yield return x;
            }
        }

        public IEnumerator<Position> GetEnumerator()
        {
            for (int y = MinY; y <= MaxY; y++)
            {
                for (int x = MinX; x <= MaxX; x++)
                {
                    yield return new Position(x, y);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}