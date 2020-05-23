using Aoc.AoC2019.IntCode;
using AoC.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc.AoC2019.Problems.Day21
{
    /// <summary>
    /// Solutions hand-processed as easier than calulating.
    /// </summary>
    public class Day21_Solution : AoCSolution
    {
        public override int Year => 2019;

        public override int Day => 21;

        public override string Name => "Day 21: Springdroid Adventure";

        public override string InputFileName => "Day21.txt";

        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {
            // Hand-coded solutions to the problem
            List<string> solution1 = new List<string>() { "NOT A J", "NOT B T", "OR T J", "NOT C T", "OR T J", "NOT D T", "NOT T T", "AND T J", "WALK" };
            List<string> solution2 = new List<string>() { "NOT A J", "NOT B T", "OR T J", "NOT C T", "OR T J", "NOT D T", "NOT T T", "AND T J", "NOT E T", "NOT T T", "OR H T", "AND T J", "RUN" };

            yield return Solve(input.First(), solution1).ToString();
            yield return Solve(input.First(), solution2).ToString();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "<Pending>")]
        public long Solve(string intCode, List<string> jumpCommands)
        {
            IVirtualMachine vm = new IntCodeVM(intCode);
            long[] asciInput = ToAscii(jumpCommands).ToArray();

            // Execute initial setup.
            vm.Execute();

            // We have to process outputs in order to clear output queue - even if we do nothing with them.
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
