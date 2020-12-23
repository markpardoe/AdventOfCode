using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Aoc.Aoc2018.Day24
{
    public class ImmuneSystemSimulation
    {
        private HashSet<UnitGroup> Infection => _allUnits.Where(x => x.Side == Army.Infection).ToHashSet();
        private HashSet<UnitGroup> ImmuneSystem =>_allUnits.Where(x => x.Side == Army.ImmuneSystem).ToHashSet();
        private readonly HashSet<UnitGroup> _allUnits;

        public ImmuneSystemSimulation(IEnumerable<UnitGroup> allUnits, int immunityBoost = 0)
        {
            _allUnits = allUnits.ToHashSet();
            if (immunityBoost > 0)
            {
                foreach (var unit in ImmuneSystem)
                {
                    unit.AddImmunityBoost(immunityBoost);
                }
            }
        }

        public (Army? winner, int RemainingUnits) RunSimulation()
        {
            while (true)
            {
                int totalDeaths = 0;

               // WriteStatus();
                var targets = GetTargets();

                // Attack phase
                var attackers = _allUnits.OrderByDescending(x => x.Initiative).ToList();

                foreach (var unit in attackers)
                {
                    if (!targets.ContainsKey(unit) || unit.EffectivePower == 0) continue; // no target for the attacker

                    totalDeaths += targets[unit].AttackedBy(unit); // attack the target
                }

                _allUnits.RemoveWhere(x => x.Quantity <= 0); // remove dead unitGroups

                // check if we've got a winner
                if (ImmuneSystem.Count == 0)
                {
                    return (Army.Infection, Infection.Sum(x => x.Quantity));
                }
                else if (Infection.Count == 0)
                {
                    return (Army.ImmuneSystem, ImmuneSystem.Sum(x => x.Quantity));
                }
                else if (totalDeaths == 0)
                {
                    return (null, 0);
                }
            }
        }

        private void WriteStatus()
        {
            Console.WriteLine();
            Console.WriteLine("Immune System:");
            foreach (var unit in _allUnits.Where(x => x.Side == Army.ImmuneSystem).OrderBy(x => x.Id))
            {
                Console.WriteLine($"Group {unit.Id} has: {unit.Quantity} units");
            }
            Console.WriteLine("Infection:");
            foreach (var unit in _allUnits.Where(x => x.Side == Army.Infection).OrderBy(x => x.Id))
            {
                Console.WriteLine($"Group {unit.Id} has: {unit.Quantity} units");
            }
        }

        private Dictionary<UnitGroup, UnitGroup> GetTargets()
        {
            var attackTargets = new Dictionary<UnitGroup, UnitGroup>();
            var unTargetedUnits = _allUnits.ToHashSet();

            // Sort units by attack power
            var sortedUnits = _allUnits
                              .OrderByDescending(x => x.EffectivePower)
                              .ThenByDescending(x => x.Initiative);


            foreach (var unit in sortedUnits)
            {
                var target = unTargetedUnits
                                 .Where(x => x.Side != unit.Side)
                                 .OrderByDescending(x => x.EstimateDamage(unit))
                                 .ThenByDescending(x => x.EffectivePower)
                                 .ThenByDescending(x => x.Initiative).FirstOrDefault();

                if (target != null && target.EstimateDamage(unit) > 0)
                {
                    attackTargets.Add(unit, target);
                    unTargetedUnits.Remove(target);  // prevent it being targeted by a different unit
                }
            }

            return attackTargets;
        }
    }
}