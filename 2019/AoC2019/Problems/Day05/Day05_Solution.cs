using Aoc.AoC2019.IntCode;
using AoC.Common;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day05
{
    public class Day05_Solution : AoCSolution<long>
    {
        public override int Year => 2019;
        public override int Day => 5;
        public override string Name => "Day 5: Sunny with a Chance of Asteroids";

        public override string InputFileName => "Day05.txt";

        public override IEnumerable<long> Solve(IEnumerable<string> input)
        {
            yield return ExecuteIntCode(input.First(), 1);
            yield return ExecuteIntCode(input.First(), 5);
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