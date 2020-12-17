using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;

namespace AoC.AoC2020.Problems.Day02
{
    public class PasswordPhilosophy :AoCSolution<int>
    {
        public override int Year => 2020;
        public override int Day => 2;
        public override string Name => "Day 02: Password Philosophy";
        public override string InputFileName => "Day02.txt";

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            var passwords = input.Select(x => new PasswordPolicy(x)).ToList();

            yield return passwords.Count(p => p.IsValid());
            yield return passwords.Count(p => p.IsValidWithPositions());
        }
    }
}
