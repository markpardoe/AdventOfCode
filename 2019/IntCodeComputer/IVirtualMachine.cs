using System.Collections.Generic;

namespace AoC2019.IntCodeComputer
{
    public interface IVirtualMachine
    {
        IReadOnlyList<long> Data { get; }
        Queue<long> Outputs { get; }
      
        ExecutionStatus Status { get; }

        void AddInput(params long[] inputs);

        void Execute(params long[] inputs);

        int InputsQueued { get; }

        void Restart();  // Resets VM back to initial values.
    }
}