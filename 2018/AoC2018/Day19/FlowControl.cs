using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AoC.Common;

namespace Aoc.Aoc2018.Day19
{
    public class FlowControl : AoCSolution<int>
    {
        public override int Year => 2018;
        public override int Day => 19;
        public override string Name => "Day 19: Go With The Flow";
        public override string InputFileName => "Day19.txt";

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            yield return Solve(input, 0);
            yield return Solve(input, 1);
        }

        private int Solve(IEnumerable<string> input, int initialValue)
        {
            OpcodeVM opCode = new OpcodeVM(input, initialValue);
            opCode.ExecuteFast();
            return opCode.Register[0];
        }

        private readonly List<string> Example1 = new List<string>()
        {
            "#ip 0",
            "seti 5 0 1",
            "seti 6 0 2",
            "addi 0 1 0",
            "addr 1 2 3",
            "setr 1 0 0",
            "seti 8 0 4",
            "seti 9 0 5"
        };

    }
}
