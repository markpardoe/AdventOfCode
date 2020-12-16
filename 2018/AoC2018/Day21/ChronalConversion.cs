using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aoc.Aoc2018.Common.OpCode;
using AoC.Common;

namespace Aoc.Aoc2018.Day21
{
    public class ChronalConversion :AoCSolution<long>
    {

        public override int Year => 2020;
        public override int Day => 21;
        public override string Name => "Day 21: Chronal Conversion";
        public override string InputFileName => "Day21.txt";

        public override IEnumerable<long> Solve(IEnumerable<string> input)
        {
            // Part 1 is solved by reverse-engineering the input instructions.
            // The only line that references register[0] is
            // Line28: Eqrr 1 0 5
            //
            // This checks if register[1] and register[0] are equal.
            // If so it exits the program by setting register[5] = 1
            // Line 29 [addr 5 3 3] then adds this '1' to the InstructionPointer,
            // taking it out of range and ending the program.
            // So all we need to do is run the program for any input value, and
            // find the value of register[1] the first time we hit instruction 28
            //
            // Part 2 simply caches register[1] everytime we hit line 28
            // When a value repeats - we know we've started to cycle so return previoud value.
            // Could be optimized?  Takes 5 min to run but does give correct answer.
            OpcodeVM opcode = new OpcodeVM(input, 0);
            long lastValue = -1;
            HashSet<long> foundHashSet= new HashSet<long>();

            while (true)
            {

                if (opcode.InstructionPointer == 28)
                {
                    long value = opcode.Register[1];
                  //  Console.WriteLine($"IP=28: {value}");

                    if (lastValue == -1)
                    {
                        // Return 1st element
                        yield return value;
                    }
                    else if (foundHashSet.Contains(value))
                    {
                        // found a duplicate value - so quit
                        yield return lastValue;
                        yield break;
                    }

                    foundHashSet.Add(value);
                    lastValue = value;
                }

                opcode.ExecuteNextInstruction();
            }
        }
    }
}
