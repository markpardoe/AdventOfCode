using AoC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc.Aoc2018.Day24
{
    public enum Army
    {
        ImmuneSystem,
        Infection
    }

    public class ImmuneSystem20XX :AoCSolution<int>
    {
        public override int Year => 2018;
        public override int Day => 24;
        public override string Name => "Day 24: Immune System Simulator 20XX";
        public override string InputFileName => "Day24.txt";

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {

            ImmuneSystemSimulation sim = new ImmuneSystemSimulation(ParseInput(input.ToList()), 26);
            var (winner, remainingUnits) = sim.RunSimulation();

            Console.WriteLine($"Winner = {winner}.  Units = {remainingUnits}");
            yield return remainingUnits;

            yield return FindMinimumInfectionBoost(input.ToList());
        }

        private int FindMinimumInfectionBoost(IEnumerable<string> rawData)
        {
            for (int i = 1; i < 1000; i++)
            {
                ImmuneSystemSimulation sim = new ImmuneSystemSimulation(ParseInput(rawData), i );
                var result = sim.RunSimulation();
                
                
                if ( result.winner.HasValue && result.winner.Value == Army.ImmuneSystem)
                {
                    return result.RemainingUnits;
                }
            }

            return -1;
        }

        private IEnumerable<UnitGroup> ParseInput(IEnumerable<string> rawData)
        {
            Army side = Army.ImmuneSystem;
            int count = 0;  // Give each group an Id for identification purposes

            foreach (var line in rawData)
            {
                if (line.StartsWith("Immune"))
                {
                    count = 1;
                    side = Army.ImmuneSystem;
                }
                else if (line.StartsWith("Infection"))
                {
                    count = 1;
                    side = Army.Infection;
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                   yield return ParseLine(line, side, count);
                   count++;
                }
            }
        }

        private UnitGroup ParseLine(string line, Army side, int id)
        {
            string unitPattern =
                @"^(?<qty>\d+) units each with (?<hp>\d+) hit points .*with an attack that does (?<attack>\d+)\s+(?<attackType>\w+) damage at initiative (?<initiative>\d+)\s*$";

            var match = Regex.Match(line, unitPattern);

            int qty = int.Parse(match.Groups["qty"].Value);
            var unit = new Unit()
            {
                HP = int.Parse(match.Groups["hp"].Value),
                AttackDamage = int.Parse(match.Groups["attack"].Value),
                Initiative = int.Parse(match.Groups["initiative"].Value),
                AttackType = match.Groups["attackType"].Value
            };

            var weaknessesMatch = Regex.Match(line, @"weak to (?<weakness>[\w ,]+)[;|)]").Groups["weakness"];
            if (weaknessesMatch.Success)
            {
               var weak = weaknessesMatch.Value.Split(',').Select(x => x.Trim());
               unit.Weaknesses.UnionWith(weak);
            }
            var immuneMatch = Regex.Match(line, @"immune to (?<immunity>[\w ,]+)[;|)]").Groups["immunity"];
            if (immuneMatch.Success)
            {
                var immunities = immuneMatch.Value.Split(',').Select(x => x.Trim());
                unit.Immunities.UnionWith(immunities);
            }

            return new UnitGroup(unit, qty, side, id);
        }

        private readonly IEnumerable<string> _example = new List<string>()
        {
            "Immune System:",
            "17 units each with 5390 hit points (weak to radiation, bludgeoning) with an attack that does 4507 fire damage at initiative 2",
            "989 units each with 1274 hit points (immune to fire; weak to bludgeoning, slashing) with an attack that does 25 slashing damage at initiative 3",
            "",
            "Infection:",
            "801 units each with 4706 hit points (weak to radiation) with an attack that does 116 bludgeoning damage at initiative 1",
            "4485 units each with 2961 hit points (immune to radiation; weak to fire, cold) with an attack that does 12 slashing damage at initiative 4"
        };
    }
}