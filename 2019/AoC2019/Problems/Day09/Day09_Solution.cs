using System;
using System.Collections.Generic;
using System.Text;
using Aoc.AoC2019.IntCode;
using System.Linq;
using AoC.Common;

namespace Aoc.AoC2019.Problems.Day09
{
    public class Day09_Solution : ISolution
    {
        public string URL => @"https://adventofcode.com/2019/day/9";
        public int Year => 2019;

        public int Day => 9;

        public string Name => "Day 9: Sensor Boost";

        public string InputFileName => "Day09.txt";

        public IEnumerable<string> Solve(IEnumerable<string> input)
        {
            yield return ExecuteCode(input.First(), 1).ToString();
            yield return ExecuteCode(input.First(), 2).ToString();
        }

        private long ExecuteCode(string code, long initialInput)
        {
            var intCode = IntCodeVM.ParseStringData(code);
            IVirtualMachine vm = new IntCodeVM(intCode, initialInput);

            vm.Execute();
            return vm.Outputs.Last();
        }       
    }
}