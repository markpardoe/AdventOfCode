using System;
using System.Collections.Generic;
using System.Text;
using AoC2019.IntCodeComputer;
using System.Linq;

namespace AoC2019.Problems.Day23
{
    public class NicController
    {
        protected readonly Dictionary<int, IVirtualMachine> _nics = new Dictionary<int, IVirtualMachine>();
        protected readonly int Size;
        protected readonly NAT Nat = new NAT();

        public NicController(int size, string instructions)
        {
            Size = size;
            for (int i=0; i< size;i++)
            {
                IVirtualMachine vm = new IntCodeVM(instructions);
                vm.AddInput(i, -1);
                _nics.Add(i, vm);                
            }
        }

        public virtual long Execute()
        {
            while(true)
            {
                RunExecutionLoop();

                // Check if the Nat (Address 255) has been assigned.
                if (Nat.Y != 0)
                {
                    return Nat.Y;
                }
            }           
        }

        // Process inputs and outputs for every IntCodeVM.
        protected void RunExecutionLoop()
        {
            for (int i = 0; i < Size; i++)
            {
                IVirtualMachine comp = _nics[i];
                ProcessOutputs(comp, i);
                               
                if (comp.Status == ExecutionStatus.Finished)
                {
                    continue;
                }
                else if (comp.Status == ExecutionStatus.AwaitingInput && comp.InputsQueued == 0)
                {
                    continue;
                }
                else
                {
                    comp.Execute();
                }

                ProcessOutputs(comp, i);
            }
        }

        private void ProcessOutputs(IVirtualMachine comp, int add)
        {
            while (comp.Outputs.Count >= 3)
            {
                int address = (int)comp.Outputs.Dequeue();
                
                long x = comp.Outputs.Dequeue();
                long y = comp.Outputs.Dequeue();

                if (address == 255)
                {
                    Nat.SetCache(x, y);
                    return;
                }
                else
                {
                    _nics[address].AddInput(x, y);
                }
            }

            if (comp.Outputs.Count > 0)
            {
                throw new InvalidOperationException("IntCodeVM shouldn't have any outputs remaining!");
            }
        }
    }
}
