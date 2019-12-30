using System;
using System.Collections.Generic;
using System.Text;
using Aoc.AoC2019.IntCode;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day23
{
    public class NicControllerWithNat : NicController
    {
        private long _lastNat_Y = Int64.MinValue;

        public NicControllerWithNat(int size, string instructions) : base(size, instructions) { }

        public override long Execute()
        {
            while (true)
            {
                RunExecutionLoop();

                // If all the nics are awiating input - and have no input or output - use the Nat to restart the process.
                // Check if the last value for Y was reused - and return it.
                if (_nics.Values.All(c => c.InputsQueued == 0 && c.Status == ExecutionStatus.AwaitingInput && c.Outputs.Count == 0))
                {
                    if (Nat.Y.Equals(_lastNat_Y))
                    {
                        return _lastNat_Y;
                    }
                  //  Console.WriteLine($"Resetting Address 0 with values: {Nat.X}, {Nat.Y}");
                    _nics[0].AddInput(Nat.X, Nat.Y);
                    _lastNat_Y = Nat.Y;
                }
            }           
        }      
    }
}
