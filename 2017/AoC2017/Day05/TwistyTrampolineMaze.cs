using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC.Common;

namespace AoC2017.Day05
{
    public class TwistyTrampolineMaze : AoCSolution<int>
    {
        public override int Year => 2017;
        public override int Day => 5;
        public override string Name => "Day 5: A Maze of Twisty Trampolines, All Alike";
        public override string InputFileName => "Day05.txt";

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {

            // Convert to int list
            var instructions = input.Select(int.Parse).ToList();
            yield return RunInstructions(instructions, SimpleIncrease);
            yield return RunInstructions(instructions, ComplexIncrement);
        }

        private int RunInstructions(List<int> instructions, Func<int, int> offsetIncrementer)
        {
            var data = instructions.ToList();  // make a copy as we'll be amending the data
            var count = data.Count;  // cache as it doesn't change
            int index = 0;
            int stepCount = 0;

            while (index >= 0 && index < count)
            {
                int nextIndex = index + data[index];
                data[index] = offsetIncrementer(data[index]);  // update current positions
                index = nextIndex;
                stepCount++;
            }

            return stepCount;
        }

        private int SimpleIncrease(int offset)
        {
            return offset + 1;
        }

        private int ComplexIncrement(int offset)
        {
            if (offset >= 3)
            {
                return offset - 1;
            }
            return offset + 1;
        }
    }
}