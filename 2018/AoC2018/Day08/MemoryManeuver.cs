using AoC.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.Aoc2018.Day08
{
    public class MemoryManeuver: AoCSolution<int>
    {
        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {

            Queue<int> numbers = new Queue<int>();

            // Convert the string input into a queue of numbers
            foreach (var line in input)
            {
                var numList = line.Split(' ').Select(x => int.Parse(x));
                foreach (var n in numList)
                {
                    numbers.Enqueue(n);
                }
            }

            Node root = new Node(numbers);
            yield return root.SumMetadata();

            yield return root.GetNodeValue();
        }

        public override int Year => 2018;
        public override int Day => 8;
        public override string Name => "Day 8: Memory Maneuver";
        public override string InputFileName => "Day08.txt";
    }
}