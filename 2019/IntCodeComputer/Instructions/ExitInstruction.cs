namespace AoC2019.IntCodeComputer.Instructions
{
    class ExitInstruction : Instruction
    {
        public ExitInstruction() : base(1) { }
        internal override void Execute()
        {
            State.Status = ExecutionStatus.Finished;
            WriteDebugMessage($"Exit Instruction");
        }
    }
}