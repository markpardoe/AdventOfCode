namespace Aoc.AoC2019.IntCode.Instructions
{
    class LessThanInstruction : Instruction
    {
        public LessThanInstruction() : base(4) { }
        internal override void Execute()
        {
            long value1 = State.GetValueAt(Index + 1, GetParameterMode(0));
            long value2 = State.GetValueAt(Index + 2, GetParameterMode(1));
            WriteDebugMessage($"Less Than Instruction: {value1} < {value2}");

            State.SetValueAt(Index + 3, GetParameterMode(2), value1 < value2 ? 1 : 0);
            State.Index = this.Index + this._parameterCount;
        }
    }
}