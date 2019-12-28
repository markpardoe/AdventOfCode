namespace AoC2019.IntCodeComputer.Instructions
{
    class InputInstruction : Instruction
    {
        public InputInstruction() : base(2) { }
        internal override void Execute()
        {
            long? input = State.GetNextInput();
            if (input.HasValue)
            {
                State.SetValueAt(Index + 1, GetParameterMode(0), input.Value);
                State.Index = this.Index + this._parameterCount;
                WriteDebugMessage("Reading input: " + input.Value);
                
            }
            else
            {
                WriteDebugMessage("Awaiting input");
                State.Status = ExecutionStatus.AwaitingInput;
            }
        }
    }
}