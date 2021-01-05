using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC.AoC2020.Problems.Day19
{
    public class Rule
    {
        public int RuleId { get; }
        public string Letter { get; private set; }
        public bool IsLetter => !string.IsNullOrWhiteSpace(Letter);

        // Or rules
        public readonly List<List<int>> SubRuleGroups = new List<List<int>>();

        public Rule (string input)
        {
            var split = input.Split(":", StringSplitOptions.RemoveEmptyEntries);

            RuleId = int.Parse(split[0].Trim());

            var ruleText = split[1].Trim();
            if (ruleText.Contains('"'))  //its a letter
            {
                Letter = ruleText[1].ToString();
            }
            else
            {
                var orRules = ruleText.Split('|');

                foreach (var rule in orRules)
                {
                    SubRuleGroups.Add(rule.Trim().Split(' ').Select(x => int.Parse((string) x)).ToList());
                }
            }
        }
    }
}