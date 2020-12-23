using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AoC.Common;
using AoC.Common.Mapping._3d;

namespace Aoc.Aoc2018.Day23
{
    public class ExperimentalEmergencyTeleportation : AoCSolution<int>
    {
        public override int Year => 2018;
        public override int Day => 23;
        public override string Name => "Day 23: Experimental Emergency Teleportation";
        public override string InputFileName => "Day23.txt";

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            var nanobots = ParseInput(input).ToList();
            var maxSignalBot = nanobots.OrderByDescending(x => x.SignalRadius).First();

            int nanoBotsInRange = nanobots.Count(x => x.Positon.DistanceTo(maxSignalBot.Positon) <= maxSignalBot.SignalRadius);
            yield return nanoBotsInRange;

            int minX = nanobots.Min(p => p.Positon.X);
            int maxX = nanobots.Max(p => p.Positon.X);
            int minY = nanobots.Min(p => p.Positon.Y);
            int maxY = nanobots.Max(p => p.Positon.Y);
            int minZ = nanobots.Min(p => p.Positon.Z);
            int maxZ = nanobots.Max(p => p.Positon.Z);

            Position3d bestPosition =  SearchGrid(new Position3d(minX, minY, minZ), new Position3d(maxX, maxY, maxZ), 100000000, nanobots.ToHashSet());
            yield return bestPosition.DistanceToOrigin();
        }
        
        private IEnumerable<NanoBot> ParseInput(IEnumerable<string> rawData)
        {
            string nanoBotPattern = @"^pos=<(?<x>-*\d+),\s*(?<y>-*\d+),\s*(?<z>-*\d+)>\s*.\s*r=(?<r>-*\d+)";

            foreach (var line in rawData)
            {
                var match = Regex.Match(line, nanoBotPattern);
                int x = int.Parse(match.Groups["x"].Value);
                int y = int.Parse(match.Groups["y"].Value);
                int z = int.Parse(match.Groups["z"].Value);
                int r = int.Parse(match.Groups["r"].Value);
                yield return new NanoBot(x,y,z,r);
            }
        }

        /// <summary>
        /// Find best position by using a recursive search
        /// Each step we make the search area smaller, and centered around the best positon from the previous step
        /// </summary>
        /// <param name="topLeft"></param>
        /// <param name="bottomRight"></param>
        /// <param name="stepSize"></param>
        /// <param name="nanobots"></param>
        /// <returns></returns>
        private Position3d SearchGrid(Position3d topLeft, Position3d bottomRight, int stepSize, HashSet<NanoBot> nanobots)
        {
            int max = Int32.MinValue;
            Position3d maxPosition =new Position3d(0,0,0);

            for (int z = topLeft.Z; z <= bottomRight.Z; z += stepSize)
            {
                for (int y = topLeft.Y; y <= bottomRight.Y; y += stepSize)
                {
                    for (int x = topLeft.X; x <= bottomRight.X; x += stepSize)
                    {
                        Position3d currentPosition = new Position3d(x,y,z);
                        int count = nanobots.Count(x => x.Positon.DistanceTo(currentPosition) <= x.SignalRadius);

                        if (count > max)
                        {
                            max = count;
                            maxPosition = currentPosition;
                        }
                        else if (count == max)
                        {
                            // use position closest to the origin
                            if (maxPosition.DistanceToOrigin() > currentPosition.DistanceToOrigin())
                            {
                                maxPosition = currentPosition;
                            }
                        }
                    }
                }
            }
            
            if (stepSize == 1)
            {
                return maxPosition;
            }
            else
            {
                // Search again around the best position with a smaller stepsize
                return SearchGrid(new Position3d(maxPosition.X - stepSize, maxPosition.Y - stepSize, maxPosition.Z - stepSize),
                                  new Position3d(maxPosition.X + stepSize,
                                                 maxPosition.Y + stepSize,
                                                 maxPosition.Z + stepSize),
                                  stepSize / 2, nanobots);      // make search step half the size.  Tried with 10 and it couldn't find the best solution
            }
        }
    }

    public class NanoBot
    {
        public Position3d Positon { get; }
        public int SignalRadius { get; }

        public NanoBot(int x, int y, int z, int r)
        {
            Positon = new Position3d(x,y,z);
            SignalRadius = r;
        }
    }
}
