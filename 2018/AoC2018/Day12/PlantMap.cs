using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day12
{
    public enum PlantStatus
    {
        Empty = '.',
        Plant = '#'
    }

    public class PlantMap
    {
        private readonly Dictionary<int, PlantStatus> _plantmap = new Dictionary<int, PlantStatus>();
        private readonly PlantStatus _default = PlantStatus.Empty;
        private readonly HashSet<PlantGrowthRule> _rules;

        public PlantMap(HashSet<PlantGrowthRule> rules)
        {
            _rules = rules;
        }

        public virtual PlantStatus this[int x]
        {
            get
            {
                if (!_plantmap.ContainsKey(x))
                {
                    return _default;
                }
                else
                {
                    return _plantmap[x];
                }
            }
            set
            {
                if (!_plantmap.ContainsKey(x))
                {
                    _plantmap.Add(x, value);
                }
                else
                {
                    _plantmap[x] = value;
                }
            }
        }

        public int MaxX => _plantmap.Keys.Max();
        public int MinX => _plantmap.Keys.Min();

        public override string ToString()
        {
            int minX = MinX ;
            int maxX = MaxX;

            StringBuilder sb = new StringBuilder();
            for (int x = minX; x <= maxX; x++)
            {
                sb.Append((char) this[x]);
            }

            return sb.ToString();
        }

        public void Add(int x, PlantStatus value)
        {
            if (_plantmap.ContainsKey(x))
            {
                _plantmap[x] = value;
            }
            else
            {
                _plantmap.Add(x, value);
            }
        }

        public void GrowGeneration()
        {
            int minX = MinX - 2;
            int maxX = MaxX + 2;
            Dictionary<int, PlantStatus> updates = new Dictionary<int, PlantStatus>();

            for (int x = minX; x <= maxX; x++)
            {
                foreach (PlantGrowthRule rule in _rules)
                {
                    if (RuleApplies(rule, x))
                    {
                       // Console.WriteLine("Rule added for " + x.ToString());
                        updates.Add(x, rule.NewStatus);
                        break;
                    }
                }

                if (!updates.ContainsKey(x))
                {
                    // no update found - so must be empty
                    updates.Add(x, PlantStatus.Empty);
                }
            }

            // update with new values
            foreach (int x in updates.Keys)
            {
              //  Console.WriteLine($"Updating ({x}):  {this[x]} ==> {updates[x]}");
                this[x] = updates[x];
            }

        }

        private bool RuleApplies(PlantGrowthRule rule, int index)
        {
            return (this[index - 2] == rule[0]
                    && this[index - 1] == rule[1]
                    && this[index] == rule[2]
                    && this[index + 1] == rule[3]
                    && this[index + 2] == rule[4]);
        }

        public int SumPlants()
        {
           return _plantmap
               .Where(kv => kv.Value == PlantStatus.Plant)
               .Sum(kv => kv.Key);
       
        }
    }
}