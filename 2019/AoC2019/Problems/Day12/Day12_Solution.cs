using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Aoc.AoC2019.Problems.Day12
{
    public class Day12_Solution : AoC2019Solution
    {
        public override string URL => @"https://adventofcode.com/2019/day/12";

        public override int Day => 12;

        public override string Name => "Day 12: The N-Body Problem";

        public override string InputFileName => null;

        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {
            // use hard-coded input rather than file.
            yield return GetTotalEnergy(initialPositions, 1000).ToString();
            yield return FindStepsUntilRepeat(initialPositions).ToString();
        }

        private int GetTotalEnergy(List<Position3d> positions, int numMoves)
        {
            MoonMap map = new MoonMap(positions);
            map.Move(numMoves);
            return map.GetTotalEnergy();
        }

        private BigInteger FindStepsUntilRepeat(List<Position3d> positions)
        {
            MoonMap map = new MoonMap(positions);
            long count = 0;

            long x_Count = 0;
            long y_Count = 0;
            long z_Count = 0;

            // Since X, y & z are calculated independantly - we just need to find the repeating cycle for each individually
            // And then find the Lowest Common Muliplier (LCM) of all 3.
            while (x_Count == 0 || y_Count == 0 || z_Count == 0)
            {
                map.Move();
                count++;

                // Check if X co-ordinate is back at inital location - and we haven't already found the cycle
                if (x_Count == 0 && map.Moons.All(m => m.AtInitialX()))
                {
                    x_Count = count;
                }

                // Check if Y co-ordinate is back at inital location - and we haven't already found the cycle
                if (y_Count == 0 && map.Moons.All(m => m.AtInitialY()))
                {
                    y_Count = count;
                }

                // Check if Z co-ordinate is back at inital location - and we haven't already found the cycle
                if (z_Count == 0 && map.Moons.All(m => m.AtInitialZ()))
                {
                    z_Count = count;
                }                
            }

            return AoC.Common.MathFunctions.LCM(x_Count, y_Count, z_Count);
        }

        private readonly List<Position3d> initialPositions = new List<Position3d>
        {
            new Position3d(-7, -8, 9),
            new Position3d(-12, -3, -4),
            new Position3d(6, -17, -9),
            new Position3d(4, -10, -6),
        };
    }
}