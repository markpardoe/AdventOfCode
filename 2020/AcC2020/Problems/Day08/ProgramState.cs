using System.Collections.Generic;

namespace AoC.AoC2020.Problems.Day08
{
    public class ProgramState
    {
        private readonly Dictionary<string, InstructionType> _instructionTypesMapper = new Dictionary<string, InstructionType>
        {
            {"acc", InstructionType.Accumulate},
            {"jmp", InstructionType.Jump},
            {"nop", InstructionType.NoOperation}
        };

        public int Accumulator { get; private set; } = 0;
        public int Index { get; private set; } = 0;

        private List<Instruction> _instructions = new List<Instruction>();

        public ProgramState(IEnumerable<string> input)
        {
            foreach (string line in input)
            {
                var c = line.Split(' ');
                _instructions.Add(new Instruction(_instructionTypesMapper[c[0]], int.Parse(c[1])));
            }
        }

        public void ExecuteNextInstruction()
        {
            var instruction = _instructions[Index];

            // Should move these to seperate classes (eg. public class JumpInstruction : Instruction {})
            // but no need if these are only used for this one problem.
            // If new instructions are added later, we can revisit this.
            if (instruction.Type == InstructionType.Accumulate)
            {
                Accumulator += instruction.Value;
                Index++;
            }
            else if (instruction.Type == InstructionType.Jump)
            {
                Index += instruction.Value;
            }
            else if (instruction.Type == InstructionType.NoOperation)
            {
                Index++;
            }
        }
    }
}