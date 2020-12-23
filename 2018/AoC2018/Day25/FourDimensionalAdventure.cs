using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using AoC.Common;
using AoC.Common.Mapping._4d;

namespace Aoc.Aoc2018.Day25
{
    public class FourDimensionalAdventure : AoCSolution<int>
    {
        public override int Year => 2020;
        public override int Day => 25;
        public override string Name => "Day 25: Four-Dimensional Adventure";
        public override string InputFileName => "Day25.txt";

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            var stars = ParseInput(input);
            var constellations = GetConstellations(stars, 3);
            yield return constellations.Count;
        }

        private IEnumerable<Position4d> ParseInput(IEnumerable<string> rawData)
        {
            foreach (var line in rawData)
            {
                var point = line.Split(',').Select(int.Parse).ToList();
                yield return new Position4d(point[0], point[1], point[2], point[3]);
            }
        }

        private HashSet<Constellation> GetConstellations(IEnumerable<Position4d> stars, int constellationDistance)
        {
            HashSet<Constellation> constellations = new HashSet<Constellation>();

            foreach (var star in stars)
            {
                var matchedConstellations = new HashSet<Constellation>();
                foreach (var constellation in constellations)
                {
                    if (constellation.TryAdd(star))
                    {
                        matchedConstellations.Add(constellation);
                    }
                }

                if (matchedConstellations.Count == 0)
                {
                    // no matches - so add a new constellation
                    constellations.Add(new Constellation(star, constellationDistance));
                }
                else if (matchedConstellations.Count > 1)
                {
                    // more than 1 constellation with the same star - so merge into one mege constellation
                    Constellation mergedConstellation = new Constellation(constellationDistance);
                    foreach (var currentConstellation in matchedConstellations)
                    {
                        mergedConstellation.Merge(currentConstellation);
                        constellations.Remove(currentConstellation);
                    }

                    constellations.Add(mergedConstellation);
                }
            }

            return constellations;
        }
    }

    public class Constellation
    {
        private readonly int _constellationDistance;
        private readonly HashSet<Position4d> _stars = new HashSet<Position4d>();
        public IReadOnlyCollection<Position4d> Stars => _stars;

        public Constellation(Position4d star, int constellationDistance)
        {
            if (constellationDistance <=0 ) throw new ArgumentOutOfRangeException(nameof(constellationDistance));
            _constellationDistance = constellationDistance;
            _stars.Add(star);
        }

        public Constellation(int constellationDistance)
        {
            if (constellationDistance <= 0) throw new ArgumentOutOfRangeException(nameof(constellationDistance));
            _constellationDistance = constellationDistance;
        }

        // Merge 2 constellations into one
        public void Merge(Constellation constellation)
        {
            _stars.UnionWith(constellation.Stars);
        }

        public bool TryAdd(Position4d star)
        {
            if (_stars.Any(x => x.DistanceTo(star) <= _constellationDistance))
            {
                _stars.Add(star);
                return true;
            }
            
            return false;
        }
    }
}