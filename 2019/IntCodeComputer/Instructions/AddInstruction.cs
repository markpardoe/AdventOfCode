namespace Aoc.AoC2019.IntCode.Instructions
{
    class AddInstruction : Instruction
    {
        public AddInstruction() : base(4) { }
        internal override void Execute()
        {
            long value1 = State.GetValueAt(Index + 1, GetParameterMode(0));
            long value2 = State.GetValueAt(Index + 2, GetParameterMode(1));
            State.SetValueAt(Index + 3, GetParameterMode(2), value1 + value2);
            State.Index = this.Index + this._parameterCount;

            WriteDebugMessage($"Add Instruction: {value1} + {value2}");

        }
    }
}