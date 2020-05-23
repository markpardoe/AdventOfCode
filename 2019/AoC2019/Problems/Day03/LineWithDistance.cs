using AoC.Common.Mapping;

namespace Aoc.AoC2019.Problems.Day03
{
    public class LineWithDistance : Line
    {
        public int TotalDistance { get;  }

        public LineWithDistance(Position start, Vector vector,int totalDistance) : base(start, vector)
        {
            TotalDistance = totalDistance;
        }
    }
}
