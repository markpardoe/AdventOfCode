using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using AoC.Common;

namespace AoC.AoC2020.Problems.Day08
{
    public class HandheldHalting : AoCSolution<int>
    {
        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            var instructions = ProgramState.GenerateInstructions(input);
            ProgramState state = new ProgramState(instructions);
            yield return FindValueAtRepeat(state);
            yield return FixProgram(instructions);
            // throw new NotImplementedException();
        }

        public override int Year => 2020;
        public override int Day => 8;
        public override string Name => "Day 8: Handheld Halting";
        public override string InputFileName => "Day08.txt";


        private int FindValueAtRepeat(ProgramState state)
        {
            HashSet<int> checkedInstructions = new HashSet<int>();
            while (!checkedInstructions.Contains(state.Index))
            {
                checkedInstructions.Add(state.Index);
                state.ExecuteNextInstruction();
            }

            return state.Accumulator;
        }


        private int FixProgram(IEnumerable<Instruction> input)
        {
            List<Instruction> instructions = input.ToList();

            for (int i = 0; i < instructions.Count; i++)
            {
                Console.WriteLine($"Checking line: {i}");
                Instruction originalInstruction = instructions[i];
                if (originalInstruction.Type != InstructionType.Accumulate)
                {
                    Instruction newInstruction = null;

                    if (originalInstruction.Type == InstructionType.Jump)
                    {
                        newInstruction = new Instruction(InstructionType.NoOperation, originalInstruction.Value);
                    }
                    else if (originalInstruction.Type == InstructionType.NoOperation)
                    {
                        newInstruction = new Instruction(InstructionType.Jump, originalInstruction.Value);
                    }
                    else
                    {
                        throw new InvalidDataException();
                    }

                    instructions[i] = newInstruction;
                    var newState = new ProgramState(instructions);
                    if (IsProgramComplete(newState))
                    {
                        return newState.Accumulator;
                    }
                    else
                    {
                        // put the old instruction back...
                        instructions[i] = originalInstruction;
                    }
                }
            }

            return -1;
        }

        // Check if a given program state - does it finish by moving past the final instruction
        private bool IsProgramComplete(ProgramState state)
        {
            HashSet<int> checkedInstructions = new HashSet<int>();
            while (!checkedInstructions.Contains(state.Index) && state.Status == ProgramStatus.Running)
            {
                checkedInstructions.Add(state.Index);
                state.ExecuteNextInstruction();
            }

            return state.Status == ProgramStatus.Complete;

        }
    }



}
