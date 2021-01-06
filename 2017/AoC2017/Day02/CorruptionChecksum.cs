using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AoC.Common;

namespace AoC2017.Day02
{
    public class CorruptionChecksum : AoCSolution<int>
    {
        public override int Year => 2017;
        public override int Day => 2;
        public override string Name => "Day 2: Corruption Checksum";
        public override string InputFileName => "Day02.txt";

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            var rawData = input.ToList();
            yield return CalculateChecksum(rawData);
            yield return PartB(rawData);
        }

        private int CalculateChecksum(IEnumerable<string> input)
        {
            var total = 0;
            foreach (var row in input)
            {
                var split = row.Split("\t").Select(x => int.Parse(x)).ToList();
                total += split.Max() - split.Min();
            }

            return total;
        }

        private int PartB(IEnumerable<string> rawData)
        {
            var total = 0;
            foreach (var row in rawData)
            {
                var split = row.Split("\t").Select(x => int.Parse(x)).ToList();
                total += FindDivisibleValues(split);
            }

            return total;
        }

        private int FindDivisibleValues(List<int> values)
        {
            // sort it so we check lowest divisors first - likely to be more efficient
            values.Sort();
            for (int i = 0; i < values.Count; i++)
            {
                int current = values[i];
                for (int j = i + 1; j < values.Count; j++)
                {
                    int next = values[j];
                    if (next % current == 0)
                    {
                        return next / current;
                    }
                }
            }

            throw new InvalidDataException("No divisible values found");
        }
    }
}