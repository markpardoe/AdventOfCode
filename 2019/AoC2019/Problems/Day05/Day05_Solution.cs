using Aoc.AoC2019.IntCode;
using AoC.Common;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day05
{
    public class Day05_Solution : AoCSolution
    {
        public override int Year => 2019;
        public override int Day => 5;
        public override string Name => "Day 5: Sunny with a Chance of Asteroids";

        public override string InputFileName => "Day05.txt";

        public override IEnumerable<string> Solve(IEnumerable<string> input)
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