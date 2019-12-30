using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Aoc.AoC2019.IntCode.Instructions
{
    abstract class Instruction
    {

        protected const bool DEBUG_ENABLED = false;

        protected InstructionType InstructionType { get; private set; }
        protected IntCodeVM State { get; private set; }
        public long Index { get; private set; }
        protected List<ParameterMode> ParameterModes { get; } = new List<ParameterMode>();
        protected readonly int _parameterCount;

        protected Instruction(int paramCount)
        {
            _parameterCount = paramCount;
        }

        internal abstract void Execute();  // Executes the instruction and returns the next index to use

        protected ParameterMode GetParameterMode(int index)
        {
            if (index >= ParameterModes.Count)
            {
                return ParameterMode.Positon;
            }
            else
            {
                return ParameterModes[index];
            }
        }


        #region Static Builder Functions
        private static readonly Dictionary<InstructionType, Func<Instruction>> instructionMap = new Dictionary<InstructionType, Func<Instruction>>()
            {
                {InstructionType.Add, () => { return new AddInstruction(); } },
                {InstructionType.Multiply, () => { return new MultiplyInstruction(); } },
                {InstructionType.Input, () => { return new InputInstruction(); } },
                {InstructionType.Output, () => { return new OutputInstruction(); } },
                {InstructionType.Exit, () => { return new ExitInstruction(); } },
                {InstructionType.JumpIfFalse, () => { return new JumpIfFalseInstruction(); } },
                {InstructionType.JumpIfTrue, () => { return new JumpIfTrueInstruction(); } },
                {InstructionType.LessThan, () => { return new LessThanInstruction(); } },
                {InstructionType.Equals, () => { return new EqualsInstruction(); } },
                {InstructionType.AdjustRelativeBase, () => { return new AdjustRelativeBaseInstruction(); } }
            };

        public static Instruction Create(IntCodeVM state, long index)
        {
            string strCode = state[index].ToString();
            if (strCode.Length == 1)
            {
                strCode = "0" + strCode;  // add leading zero for backwards compatibility with 1 digit instruction codes
            }

            // its safe to just use Int32.Parse as values should always be numberic.  If not - we want an exception to be raised.
            // Get the instruction type from the enum
            InstructionType instructionType = (InstructionType)Int64.Parse(strCode.Substring(strCode.Length - 2));

            Instruction inst = instructionMap[instructionType]();

            inst.InstructionType = instructionType;
            inst.State = state;
            inst.Index = index;

            // If only 2 characters - we assume that the all the parameter values are 0
            // otherwise - add the paramters to the value
            if (strCode.Length > 2)
            {
                var v = strCode.Substring(0, strCode.Length - 2)
                   .Reverse()  // parameters are listed right - to - left - so we reverse it to get values
                   .Select(x => (ParameterMode)Int32.Parse(x.ToString()));
                inst.ParameterModes.AddRange(v);
            }

            return inst;
        }

        protected void WriteDebugMessage(string s)
        {
            if (DEBUG_ENABLED)
            {
#pragma warning disable CS0162 // Unreachable code detected
                Console.WriteLine(s);
#pragma warning restore CS0162 // Unreachable code detected
            }
        }

        #endregion
    }
}
