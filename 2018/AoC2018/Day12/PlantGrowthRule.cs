using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;

namespace Aoc.Aoc2018.Day12
{
    public class PlantGrowthRule
    {
       
        private readonly PlantStatus[] _rule = new PlantStatus[5];

        public IReadOnlyCollection<PlantStatus> Rule => new ReadOnlyCollection<PlantStatus>(_rule);
        public readonly PlantStatus NewStatus;

        public PlantGrowthRule(PlantStatus[] rule, PlantStatus newStatus)
        {
            if (rule.Length != 5)
            {
                throw new ArgumentException("Invalid rule", nameof(rule));
            }

            _rule = rule;
            NewStatus = newStatus;
        }

        public override string ToString()
        {
            string output = string.Join("", Rule.Select(s => (char) s));
            return $"{output} => {(char) NewStatus}";
        }


        public virtual PlantStatus this[int x] => _rule[x];
    }
}
