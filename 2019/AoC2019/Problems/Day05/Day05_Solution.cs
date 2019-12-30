using System;
using System.Collections.Generic;
using System.Text;
using Aoc.AoC2019.IntCode;
using System.Linq;
using AoC.Common;

namespace Aoc.AoC2019.Problems.Day05
{
    public class Day05_Solution : ISolution
    {
        public int Year => 2019;

        public int Day => 5;

        public string Name => "Day 5: Sunny with a Chance of Asteroids";

        public string InputFileName => "Day05.txt";

        public IEnumerable<string> Solve(IEnumerable<string> input)
        {
            yield return ExecuteIntCode(input.First(), 1).ToString();
            yield return ExecuteIntCode(input.First(), 5).ToString();
        }
        
        private long ExecuteIntCode(string data, long inputArg)
        {
            var code = IntCodeVM.ParseStringData(data);

            IVirtualMachine vm = new IntCodeVM(code, inputArg);
            vm.Execute();
            return vm.Outputs.Last();
        }       
    }
}