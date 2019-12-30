namespace Aoc.AoC2019.IntCode.Instructions
{
    class AdjustRelativeBaseInstruction : Instruction
    {
        public AdjustRelativeBaseInstruction() : base(2) { }
        internal override void Execute()
        {
            long value1 = State.GetValueAt(Index + 1, GetParameterMode(0));
            State.InstructionBase += value1;
            State.Index = this.Index + this._parameterCount;
            WriteDebugMessage($"Adjust Relative Base Instruction: {value1}");
        }
    }
}