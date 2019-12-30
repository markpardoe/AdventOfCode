using AoC.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Aoc.AoC2019.IntCode;

namespace Aoc.AoC2019.Problems.Day02
{
    public class Day02_Solution : ISolution
    {
        public int Year => 2019;

        public int Day => 2;

        public string Name => "Day 2: 1202 Program Alarm";

        public string InputFileName => "Day02.txt";

        public IEnumerable<string> Solve(IEnumerable<string> input)
        {
            yield return ExecuteMachine(input.First(), 12, 2).ToString() ;
            yield return FindAddressForTargetResult(input.First(), 19690720).ToString();
        }       

        public long FindAddressForTargetResult(string data, long target)
        {
            for (int noun = 0; noun < 99; noun++)
            {
                for (int verb = 0; verb < 99; verb++)
                {
                    if (ExecuteMachine(data, noun, verb) == target)
                    {
                        return (100 * noun) + verb;
                    }
                }
            }

            return -1; // no result found
        }

        public long ExecuteMachine(string data, int noun, int verb)
        {
            var inputData = IntCodeVM.ParseStringData(data);
            inputData[1] = noun;
            inputData[2] = verb;
            var intCode = new IntCodeVM(inputData);
            intCode.Execute();
            return intCode.Data[0];
        }
        
        public List<long> SolveA(List<int> values)
        {
            var computer = new IntCodeVM(values);
            computer.Execute();
            return new List<long>(computer.Data);
        }
    }
}
