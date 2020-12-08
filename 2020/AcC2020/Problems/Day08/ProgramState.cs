using System.Collections.Generic;
using System.Linq;

namespace AoC.AoC2020.Problems.Day08
{
    public enum ProgramStatus
    {
        Running,
        Complete
    }

    public class ProgramState
    {
        private static readonly Dictionary<string, InstructionType> _instructionTypesMapper = new Dictionary<string, InstructionType>
        {
            {"acc", InstructionType.Accumulate},
            {"jmp", InstructionType.Jump},
            {"nop", InstructionType.NoOperation}
        };

        public int Accumulator { get; private set; } = 0;
        public int Index { get; private set; } = 0;
        public ProgramStatus Status { get; private set; } = ProgramStatus.Running;

        private List<Instruction> _instructions = new List<Instruction>();

        public ProgramState(IEnumerable<string> input) : this(ProgramState.GenerateInstructions(input))
        {
        }

        public ProgramState(IEnumerable<Instruction> instructions)
        {
            // use ToList to make a copy
            _instructions = instructions.ToList();  
        }

        public void ExecuteNextInstruction()
        {
            if (Index >= _instructions.Count || Status == ProgramStatus.Complete)
            {
                Status = ProgramStatus.Complete;
            }
            else
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

                if (Index >= _instructions.Count)
                {
                    Status = ProgramStatus.Complete;
                }
            }
        }

        public static IList<Instruction> GenerateInstructions(IEnumerable<string> input)
        {
            List<Instruction> instructions = new List<Instruction>();
            foreach (string line in input)
            {
                var c = line.Split(' ');
                instructions.Add(new Instruction(_instructionTypesMapper[c[0]], int.Parse(c[1])));
            }

            return instructions;
        }
    }
}