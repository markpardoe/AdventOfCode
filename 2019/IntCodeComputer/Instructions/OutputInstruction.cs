namespace Aoc.AoC2019.IntCode.Instructions
{
    class OutputInstruction : Instruction
    {
        public OutputInstruction() : base(2) { }
        internal override void Execute()
        {
            long output = State.GetValueAt(Index + 1, GetParameterMode(0));
            State.AddOutput(output);
            State.Index = this.Index + this._parameterCount;
            WriteDebugMessage("Outputting: " + output);
        }
    }
}