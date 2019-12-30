using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AoC.Common;
using Aoc.AoC2019.IntCode;

namespace Aoc.AoC2019.Problems.Day21
{
    public class Day21_Solution : ISolution
    {
        public int Year => 2019;

        public int Day => 21;

        public string Name => "Day 21: Springdroid Adventure";

        public string InputFileName => "Day21.txt";

        public IEnumerable<string> Solve(IEnumerable<string> input)
        {
            // Hand-coded solutions to the problem
            List<string> solution1 = new List<string>() { "NOT A J", "NOT B T", "OR T J", "NOT C T", "OR T J", "NOT D T", "NOT T T", "AND T J", "WALK" };
            List<string> solution2 = new List<string>() { "NOT A J", "NOT B T", "OR T J", "NOT C T", "OR T J", "NOT D T", "NOT T T", "AND T J", "NOT E T", "NOT T T", "OR H T", "AND T J", "RUN" };

            yield return Solve(input.First(), solution1).ToString();
            yield return Solve(input.First(), solution2).ToString();
        }        

        public long Solve(string intCode, List<string> jumpCommands)
        {
            IVirtualMachine vm = new IntCodeVM(intCode);
            long[] asciInput = ToAscii(jumpCommands).ToArray();

            // Execute initial setup.
            vm.Execute();
            List<long> outputs = vm.Outputs.DequeueToList();
            // Console.WriteLine(AsciiToString(outputs));  // Print initial output (asking for commands)

            vm.Execute(asciInput);  // Add the input commands
            outputs = vm.Outputs.DequeueToList();
            // Console.WriteLine(AsciiToString(outputs));  // print the final output (including visualisation of any errors)
            return outputs.Last();  
        }

        private string AsciiToString(List<long> chars)
        {
            StringBuilder b = new StringBuilder();
            foreach (long l in chars)
            {
                b.Append((char)l);
            }
            return b.ToString();
        }

        private List<long> ToAscii(List<string> input)
        {
            List<long> charValues = new List<long>();

            foreach (string s in input)
            {
                charValues.AddRange(s.ToCharArray().Select(c => (long)c));
                charValues.Add(10);
            }
            return charValues;
        }
    }
}
