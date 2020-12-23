using System.Collections.Generic;

namespace Aoc.Aoc2018.Day24
{
    public class Unit
    {
        public int HP { get; set; }
        public int AttackDamage { get; set; }
        public int Initiative { get; set; }
        public string AttackType { get; set; }

        public HashSet<string> Weaknesses { get;  }  = new HashSet<string>();
        public HashSet<string> Immunities { get;  } = new HashSet<string>();

        
        public bool IsWeakTo(string weakness)
        {
            return Weaknesses.Contains(weakness);
        }

        public bool IsImmuneTo(string immunity)
        {
            return Immunities.Contains(immunity);
        }
    }
}