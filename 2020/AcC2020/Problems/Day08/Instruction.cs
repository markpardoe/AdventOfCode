namespace AoC.AoC2020.Problems.Day08
{
    public enum InstructionType
    {
        Accumulate,
        Jump,
        NoOperation
    }

    public class Instruction
    {
        public InstructionType Type { get; }
        public int Value { get; }

        public Instruction(InstructionType type, int value)
        {
            Type = type;
            Value = value;
        }
    }
}