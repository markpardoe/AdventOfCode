using System.Collections.Generic;
using System.Data;
using System.IO;
using AoC.Common;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day22
{
    public class ModeMaze :AoCSolution<int>
    {
        public override int Year => 2018;
        public override int Day => 22;
        public override string Name => "Day 22: Mode Maze";
        public override string InputFileName => null;  // hardcode puzzle values

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            CaveMap map = new CaveMap(_target, _depth);

           yield  return map.GetRiskLevel();

           Explorer explorer = new Explorer(map);
           yield return explorer.FindShortestPath(new Position(0, 0), _target);
        }

        private readonly int _depth =  3339;
        private readonly Position _target = new Position(10, 715); //715);
    }

    public enum RegionType
    {
        Wet = '=',
        Rocky = '.',
        Narrow = '|',
    }

    public class CaveRegion  :Position
    {
        private static readonly IReadOnlyDictionary<RegionType, int> RiskLevels = new Dictionary<RegionType, int>()
        {
            {RegionType.Rocky, 0},
            {RegionType.Narrow, 2},
            {RegionType.Wet, 1}
        };

        public int ErosionLevel { get; }
        public int GeologicalIndex { get; }
        public RegionType RegionType { get; }

        public CaveRegion(int x, int y, int erosionLevel, int geologicalIndex) : base(x, y)
        {
            GeologicalIndex = geologicalIndex;
            ErosionLevel = erosionLevel;

            int t = erosionLevel % 3;

            RegionType = t switch
            {
                0 => RegionType.Rocky,
                1 => RegionType.Wet,
                2 => RegionType.Narrow,
                _ => throw new DataException("Unable to convert RegionType")
            };
        }

        public override string ToString()
        {
            return $"({X},{Y}): {RegionType}";
        }

        public int RiskLevel => RiskLevels[RegionType];
    }
}
