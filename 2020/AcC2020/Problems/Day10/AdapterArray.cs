using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AoC.Common;
using AoC.Common.DataStructures;

namespace AoC.AoC2020.Problems.Day10
{
    public class AdapterArray :AoCSolution<long>
    {
        public override int Year => 2020;
        public override int Day => 10;
        public override string Name => "Day 10: Adapter Array";
        public override string InputFileName => "Day10.txt";

        public override IEnumerable<long> Solve(IEnumerable<string> input)
        {
            List<int> adapters = input.Select(int.Parse).ToList();

            var diffCount = CalculateGapDifferences(adapters);
            yield return diffCount.Item1 * diffCount.Item2;

            long part2 = CountAdaptorPaths(adapters);
            yield return part2;
        }


        public Tuple<int, int> CalculateGapDifferences(List<int> adapters)
        {
            int[] diffCount = new int[3];  // count number of differences

            // make sure the list of numbers is sorted
            adapters.Sort();
            int previous = 0;

            foreach (var adapter in adapters)
            {
                int diff = adapter - previous;
                previous = adapter;
                if (diff <= 0 || diff >= 4)
                {
                    throw new InvalidDataException("Invalid Joltage gap");
                }

                diffCount[diff - 1]++;
            }

            // Add 1 to the 3 difference count for the connection to final device.
            diffCount[2]++;
            // Console.Out.WriteLine(string.Join(", ", diffCount));
            return new Tuple<int, int>(diffCount[0], diffCount[2]);
        }



        /// <summary>
        ///  Calculate the number of unique paths through the graph.
        ///  Basically the number of paths to node x = p(X) = p(X-1) + p(X-2) + p(X-3)  [not all will be valid nodes)
        ///  ie. If there are 4 paths to [node 5] and 7 paths to [node 7]  then there are 4 + 7 = 11 paths to node 8
        /// 
        /// Foreach node, we keep a running total of the number of paths to reach that node.
        ///  Each child node then gets this number added to their total, and so-on until the final node is reached.
        ///  Eg.
        ///     Nodes   0, 1, 2, 3
        ///     We have the following 4 routes:
        ///     0 --> 1 --> 2 --> 3
        ///     0 --------> 2 --> 3
        ///     0 --> 1 --------> 3
        ///     0 --------------> 3
        ///
        /// Since the intial node [0] has 1 path to it, we pass 1 down each path, totalling at each node:
        /// So node [3] = 1 + 1 + 1 + 1 = 4
        ///
        ///  IF we continue with 3, 4, 5
        ///    3 --> 4 --> 5   [5] = 4 routes
        ///    3 --------> 5    [5] = 4 + 4 = 8 routes
        ///    
        /// </summary>
        /// <param name="adaptors"></param>
        /// <returns></returns>
        public long CountAdaptorPaths(List<int> adaptors)
        {
            adaptors.Add(0); // add start location
            
            int target = adaptors.Max() + 3;
            adaptors.Add(target);  // add the final charge
            adaptors.Sort();

            // Count the number of paths to each node (int joltage, long number of paths).
            // Start with 1 path from initial point
            Dictionary<int, long> pathCounter = new Dictionary<int, long>
                    {
                        {0, 1}  // Add start point
                    }; 

            // Step through the adaptors one-by-one
            foreach (var node in adaptors)
            {
                // Get number of paths to current node
                long paths = pathCounter[node];

                // Get possible future nodes
                var nextNodes = adaptors.Where(x => x > node && x <= node + 3);
                foreach (var n in nextNodes)
                {
                    if (pathCounter.ContainsKey(n))
                    {
                        pathCounter[n] += paths;
                    }
                    else
                    {
                        pathCounter.Add(n, paths);
                    }
                }
            }

            return pathCounter[target];
        }

       
    }
}