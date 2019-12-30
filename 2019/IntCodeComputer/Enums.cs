using System;
using System.Collections.Generic;
using System.Text;

namespace Aoc.AoC2019.IntCode
{
    enum ParameterMode
    {
        Positon = 0,
        Immediate = 1,
        Relative = 2
    }

    enum InstructionType
    {
        Add = 1,
        Multiply = 2,
        Input = 3,
        Output = 4,
        Exit = 99,
        JumpIfTrue = 5,
        JumpIfFalse = 6,
        LessThan = 7,
        Equals = 8,
        AdjustRelativeBase = 9
    }

    public enum ExecutionStatus
    {
        Running = 0,
        Finished = 1,
        AwaitingInput = 2
    }
}
