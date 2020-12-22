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
            var nanobots = ParseInput(input);
            var maxSignalBot = nanobots.OrderByDescending(x => x.SignalRadius).First();

            int nanoBotsInRange = nanobots.Count(x => x.Positon.DistanceTo(maxSignalBot.Positon) <= maxSignalBot.SignalRadius);
            yield return nanoBotsInRange;

           Console.WriteLine("Min X " + nanobots.Min(p => p.Positon.X));
           Console.WriteLine("Max X " + nanobots.Max(p => p.Positon.X));
           Console.WriteLine("Min y " + nanobots.Min(p => p.Positon.Y));
           Console.WriteLine("Max y " + nanobots.Max(p => p.Positon.Y));
           Console.WriteLine("Min z " + nanobots.Min(p => p.Positon.Z));
           Console.WriteLine("Max z " + nanobots.Max(p => p.Positon.Z));
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
