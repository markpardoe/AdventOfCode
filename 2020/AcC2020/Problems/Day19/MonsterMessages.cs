using AoC.Common;
using System.Collections.Generic;
using System.Text;

namespace AoC.AoC2020.Problems.Day19
{
    public class MonsterMessages : AoCSolution<int>
    {
        public override int Year => 2020;
        public override int Day => 19;
        public override string Name => "Day 19: Monster Messages";
        public override string InputFileName => "Day19.txt";

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            var (rules, messages) = ParseInput(input);
            RuleChecker checker = new RuleChecker(rules);
            yield return checker.CountMatchedMessages(0, messages);

            // Update rules 8 and 11
            checker.UpdateRule(new Rule("8: 42 | 42 8"));
            checker.UpdateRule(new Rule("11: 42 31 | 42 11 31"));
            yield return checker.CountMatchedMessages(0, messages);
        }


        private (Dictionary<int, Rule> rules, List<string> messages) ParseInput(IEnumerable<string> input)
        {
            var receivedMessages = new List<string>();
            var allRules = new Dictionary<int, Rule>();

            foreach (var line in input)
            {
             
                if (line.Contains(':'))
                {
                    var rule = new Rule(line);
                    allRules.Add(rule.RuleId, rule);

                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    receivedMessages.Add(line);
                }
            }

            return (allRules, receivedMessages);
        }
    }
}