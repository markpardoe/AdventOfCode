namespace AoC2019.IntCodeComputer.Instructions
{
    class JumpIfFalseInstruction : Instruction
    {
        public JumpIfFalseInstruction() : base(3) { }
        internal override void Execute()
        {
            long value1 = State.GetValueAt(Index + 1, GetParameterMode(0));
            long value2 = State.GetValueAt(Index + 2, GetParameterMode(1));
            WriteDebugMessage($"Jump If True Instruction: {value1} != 0. Set index to {value2}");
            if (value1 == 0)
            {
                State.Index = value2;
            }
            else
            {
                State.Index = this.Index + this._parameterCount;
            }
        }
    }
}