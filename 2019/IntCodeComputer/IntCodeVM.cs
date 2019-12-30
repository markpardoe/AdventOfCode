using Aoc.AoC2019.IntCode.Instructions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Aoc.AoC2019.IntCode
{
    public class IntCodeVM : IVirtualMachine
    {
        private InstructionDictionary _instructions;
        internal long Index { get; set; } = 0;
        private readonly Queue<long> _inputs = new Queue<long>();
        public Queue<long> Outputs { get; } = new Queue<long>();
        internal long InstructionBase { get; set; } = 0;
        private readonly List<long> _initialInput;

        public IReadOnlyList<long> Data
        {
            get
            {
                return new List<long>(_instructions.Instructions());

            }
        }

        public override string ToString()
        {
            return this.Status.ToString() + ", Input = " + _inputs.Count + ", Outputs = " + Outputs.Count;
        }

        public int InputsQueued => _inputs.Count;
        // private readonly ExecutionState state;
        public ExecutionStatus Status { get; internal set; }

        public IntCodeVM(IEnumerable<long> instructions, params long[] inputs)
        {
            if (instructions == null)
            {
                throw new ArgumentNullException(nameof(instructions));
            }
            _instructions = new InstructionDictionary(instructions);
            _initialInput = new List<long>(instructions);

            if (inputs != null)
            {
                _inputs = new Queue<long>(inputs);
            }

            this.Status = ExecutionStatus.Running;
        }

        public IntCodeVM(IEnumerable<int> instructions, params int[] inputs)
        {
            if (instructions == null)
            {
                throw new ArgumentNullException(nameof(instructions));
            }
            _instructions = new InstructionDictionary(instructions);

            if (inputs != null)
            {
                _inputs = new Queue<long>(inputs.Select(a => (long)a));
            }

            this.Status = ExecutionStatus.Running;
        }

        public IntCodeVM(string code) : this(ParseStringData(code)) { }

        public static List<long> ParseStringData(string dataStream)
        {
            return dataStream.Split(',').Select(x => Int64.Parse(x)).ToList();
        }

        public void Execute(params long[] inputs)
        {
            AddInput(inputs);
            while (Status == ExecutionStatus.Running)
            {
                Instruction inst = Instruction.Create(this, Index);
                inst.Execute();
            }
        }

        public void AddInput(params long[] inputs)
        {
            foreach (long i in inputs)
            {
                _inputs.Enqueue(i);
            }
            if (inputs.Length > 0 && Status == ExecutionStatus.AwaitingInput)
            {
                Status = ExecutionStatus.Running;
            }
        }

        public void Restart()
        {
            this.InstructionBase = 0;
            this._inputs.Clear();
            this.Outputs.Clear();
            this.Index = 0;
            _instructions = new InstructionDictionary(_initialInput);
        }

        internal long? GetNextInput()
        {
            if (_inputs.Count > 0)
            {
                return _inputs.Dequeue();
            }
            else
            {
                Status = ExecutionStatus.AwaitingInput;
                return null;
            }
        }

        internal long GetValueAt(long index, ParameterMode mode)
        {
            long temp = _instructions[index];
            return mode switch
            {
                (ParameterMode.Immediate) => temp,
                (ParameterMode.Positon) => _instructions[temp],
                (ParameterMode.Relative) => _instructions[temp + InstructionBase],
                _ => throw new ArgumentException("invalid Parameter Type."),
            };
        }

        internal void SetValueAt(long index, ParameterMode mode, long value)
        {
            long temp = _instructions[index];
            switch (mode)
            {
                case (ParameterMode.Positon):
                    _instructions[temp] = value;
                    break;
                case (ParameterMode.Relative):
                    _instructions[temp + InstructionBase] = value;
                    break;
                default:
                    throw new ArgumentException("invalid Parameter Type.");
            }
        }

        internal void AddOutput(long value)
        {
            Outputs.Enqueue(value);
        }

        internal long this[long index]
        {
            get
            {
                return _instructions[index];
            }
        }
       
    }
}
