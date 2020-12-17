using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AoC.Common;

namespace AoC.AoC2020.Problems.Day07
{
    public class HandyHaversacks : AoCSolution<int>
    {
        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            var bags = GenerateBags(input);

            Bag target = bags.First(b => b.Name == "shiny gold");
            yield return SearchForBag(bags, target);

            yield return CountNestedBags(bags, target, 1) - 1;  // subtract 1 to exclude the original bag.
        }

        public override int Year => 2020;
        public override int Day => 7;
        public override string Name => "Day 7: Handy Haversacks";
        public override string InputFileName => "Day07.txt";

        private readonly string regExPattern = @"^(?<bag>[a-z ]+)(?=\bbags contain\b)bags contain (?<children>.+)*.$";
        private readonly string childBagPattern = @"(?<qty>\d+) (?<bag>[a-z]+ [a-z]+) bag[s]*";

        // Generates a graph of all bags with links to their children (bags they contain)
        // and parents (bags they can fit into).
        private HashSet<Bag> GenerateBags(IEnumerable<string> input)
        {
            var bags = new Dictionary<string, Bag>();

            foreach (string line in input)
            {
                Bag parent = null;
                var cleanLine = line.Trim().ToLower();

                Match match = Regex.Match(cleanLine, regExPattern);

                // Get the name of the parent bag.
                // If it already exists - use the existing Bag.
                string parentBagName = match.Groups["bag"].Value.Trim();

                if (bags.ContainsKey(parentBagName))
                {
                    parent = bags[parentBagName];
                }
                else
                {
                    parent = new Bag(parentBagName);
                    bags.Add(parent.Name, parent);
                }
                
                string children = match.Groups["children"].Value.Trim();

                // Process the child (contained within parent) bags.
                if (children != "no other bags") // Check for no children
                {
                    string[] splitChildren = children.Split(",");

                    // Create a bag (or use existing) or each bag
                    foreach (var child in splitChildren)
                    {
                        Bag childBag;
                        var childMatch = Regex.Match(child, childBagPattern);
                        var childName = childMatch.Groups["bag"].Value.Trim();
                        var qty = int.Parse(childMatch.Groups["qty"].Value);

                        if (bags.ContainsKey(childName))
                        {
                            childBag = bags[childName];
                        }
                        else
                        {
                            childBag = new Bag(childName);
                            bags.Add(childBag.Name, childBag);
                        }

                        // Add children to parents - and vice-versa.
                        parent.AddChild(childBag, qty);
                        childBag.AddParent(parent);
                    }
                }
              
            }

            return bags.Values.ToHashSet();
        }

        private int SearchForBag(HashSet<Bag> bags, Bag target)
        {
            HashSet<Bag> result = new HashSet<Bag>();

            HashSet<Bag> checkedBags = new HashSet<Bag>();  // All the bags we've checked - prevent any loops in the graph.
            Queue<Bag> bagsToCheck = new Queue<Bag>();

            // Add initial parents of the target to the list to check.
            var initialTargets = bags.Where(b => b.ContainsChild(target));

            foreach (Bag b in initialTargets)
            {
                bagsToCheck.Enqueue(b);
                result.Add(b);
            }

            // Foreach bag to check:
            //  - Get all parents
            //      - Add parent(s) to result
            //      - Add parents(s) to bagsToCheck - unless its already been checked.
            while (bagsToCheck.Count > 0)
            {
                Bag bag = bagsToCheck.Dequeue();
                foreach (var parent in bag.Parents)
                {
                    result.Add(parent);
                    if (checkedBags.Add(parent))
                    {
                        bagsToCheck.Enqueue(parent);
                    }
                }
   
            }

            return result.Count;
        }

        private int CountNestedBags(HashSet<Bag> bags, Bag target, int qty)
        {
            int total = 0;
            
            // Add initial parents of the target to the list to check.
            var children = target.Children;

            if (children.Count == 0)
            {
                // no children - so return value.
                Console.WriteLine($"{target.Name} - no children");
                return qty;
            }
            else
            {
                foreach (var child in children )
                {
                    Console.WriteLine($"{target.Name} - Searching {child.Key.Name} {child.Value}.  Qty = {qty}");
                    total += (CountNestedBags(bags, child.Key, child.Value)) * qty;
                }

                Console.WriteLine($"{target.Name} Total = {total}");
                return total + qty;
            }
        }
    }
}
