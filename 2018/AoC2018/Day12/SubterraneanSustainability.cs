using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day12
{
    public class SubterraneanSustainability : AoCSolution<long>
    {
        private readonly Dictionary<char, PlantStatus> _plantMappings = new Dictionary<char, PlantStatus>
        {
            {'.', PlantStatus.Empty},
            {'#', PlantStatus.Plant}
        };

        public override IEnumerable<long> Solve(IEnumerable<string> input)
        {
            string firstLine =input.First();

            HashSet<PlantGrowthRule> rules = GenerateRules(input.Skip(2));
            PlantMap map = GenerateMap(firstLine, rules);

            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine($"{i}: {map.SumPlants()}");
                map.GrowGeneration();
            }

            yield return map.SumPlants();


            for (int i = 20; i < 1000; i++)
            {
                Console.WriteLine($"{i}: {map.SumPlants()}");
                map.GrowGeneration();
            }

            // at 1000 generation it stabilizes at +58 per generation
            long genLeft = 50000000000 - 1000;
            long finalScore = map.SumPlants() + (genLeft * 58);
            yield return finalScore;
        }

        public override int Year => 2018;
        public override int Day => 12;
        public override string Name => "Day 12: Subterranean Sustainability";
        public override string InputFileName => "Day12.txt";

        private PlantMap GenerateMap(string input, HashSet<PlantGrowthRule> rules)
        {
            PlantMap map = new PlantMap(rules);
            input = input.Remove(0, 14).Trim();

            for (int i = 0; i < input.Length; i++)
            {
                map.Add(i, input[i] == '#' ? PlantStatus.Plant : PlantStatus.Empty);
            }

            return map;
        }

        private HashSet<PlantGrowthRule> GenerateRules(IEnumerable<string> input)
        {
            var rules = new HashSet<PlantGrowthRule>();
            foreach (string line in input)
            {
                var r = line.Substring(0, 5).Select(x => _plantMappings[x]).ToArray();
                var status = _plantMappings[line.Last()];

                rules.Add(new PlantGrowthRule(r, status));
            }

            return rules;
        }
    }
}