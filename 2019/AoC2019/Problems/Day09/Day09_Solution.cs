using Aoc.AoC2019.IntCode;
using AoC.Common;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day09
{
    public class Day09_Solution : AoCSolution<long>
    {
        public override int Year => 2019;

        public override int Day => 9;

        public override string Name => "Day 9: Sensor Boost";

        public override string InputFileName => "Day09.txt";

        public override IEnumerable<long> Solve(IEnumerable<string> input)
        {
            yield return ExecuteCode(input.First(), 1);
            yield return ExecuteCode(input.First(), 2);
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