using AoC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc.Aoc2018.Day07
{
    public class Day07_Solution : ISolution
    {
        public int Year => 2018;

        public int Day => 7;

        public string Name => "Day 7: The Sum of Its Parts";

        public string InputFileName => "Day07.txt";

        public IEnumerable<string> Solve(IEnumerable<string> input)
        {
            yield return FindShortestPath(input);
        }

        public string FindShortestPath(IEnumerable<string> input)
        {
            Dictionary<string, Node> graph = ParseInput(input);

            List<Node> closedList = new List<Node>();  // we can't use hashset as we want to maintain the order.           
            List<Node> openList = graph.Values.Where(n => n.Parents.Count == 0).ToList();   // select all nodes with no parents

            while (openList.Count > 0)
            {
                Node current = openList.OrderBy(c => c.NodeId).First();
                closedList.Add(current);
                openList.Remove(current);

                foreach (Node child in current.Children)
                {
                    if (!closedList.Contains(child) && !openList.Contains(child) && child.Parents.All(n => closedList.Contains(n)))
                    {
                        openList.Add(child);
                    }
                }
            }
            return string.Join("", closedList.Select(n => n.NodeId));
        }

        private Dictionary<string, Node> ParseInput(IEnumerable<string> input)
        {
            Dictionary<string, Node> graph = new Dictionary<string, Node>();

            foreach (string s in input)
            {
                string parent = s[5].ToString();
                string child = s[36].ToString();

                // Add new nodes if not already existing
                if (!graph.ContainsKey(parent)) { graph.Add(parent, new Node(parent)); }
                if (!graph.ContainsKey(child)) { graph.Add(child, new Node(child)); }

                graph[parent].Children.Add(graph[child]);
                graph[child].Parents.Add(graph[parent]);
            }

            return graph;
        }
    }
}
