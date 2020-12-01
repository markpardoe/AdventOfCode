using AoC.Common;
using AoC.Common.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.Aoc2018.Day03
{
    public class Day03_Solution : AoCSolution<string>
    {
        public override int Year => 2018;

        public override int Day => 3;

        public override string Name => "Day 3: No Matter How You Slice It";

        public override string InputFileName => "Day03.txt";

        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {
            yield return CountOverlappingClothes(input).ToString();
            yield return FindNonOverlappingCloth(input);
        }

        public int CountOverlappingClothes(IEnumerable<string> input)
        {
            var cloths = input.Select(s => new ClothRectangle(s));
            Map<int> fabric = GenerateFabricMap(cloths);

            // Count entries with more than one allocation
            return fabric.Values.Count( c => c>1);
        }

        public string FindNonOverlappingCloth(IEnumerable<string> input)
        {
            var cloths = input.Select(s => new ClothRectangle(s));
            Map<int> fabric = GenerateFabricMap(cloths);

            foreach (ClothRectangle c in cloths)
            {
                if (HasNoOverlap(c, fabric))
                {
                    return c.Id;
                }
            }

            return null;
        }

        private bool HasNoOverlap(ClothRectangle cloth, Map<int> fabric)
        {
            for (int x = cloth.X; x < cloth.X + cloth.Width; x++)
            {
                for (int y = cloth.Y; y < cloth.Y + cloth.Height; y++)
                {
                    if (fabric[x, y] > 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private Map<int> GenerateFabricMap(IEnumerable<ClothRectangle> cloths)
        {
            Map<int> fabric = new Map<int>(0);
            foreach (ClothRectangle c in cloths)
            {
                for (int x = c.X; x < c.X + c.Width; x++)
                {
                    for (int y = c.Y; y < c.Y + c.Height; y++)
                    {
                        fabric[new Position(x, y)]++;
                    }
                }
            }
            return fabric;
        }
    }
}
