using System;
using System.Collections.Generic;
using System.Text;
using AoC.Common;

namespace AoC.AoC2020.Problems.Day08
{
    public class HandheldHalting : AoCSolution<int>
    {
        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            ProgramState state = new ProgramState(input);
            yield return FindValueAtRepeat(state);

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
    }



}
