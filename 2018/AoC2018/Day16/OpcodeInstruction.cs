using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace Aoc.Aoc2018.Day16
{
    public enum OpCodeInstructionType
    {
        AddRegister,
        AddImmediate,
        MultiplyRegister,
        MultiplyImmediate,
        BitwiseAndRegister,
        BitwiseAndImmediate,
        BitwiseOrRegister,
        BitwiseOrImmediate,
        SetRegister,
        SetImmediate,
        GreaterThanImmediateRegister,
        GreaterThanRegisterImmediate,
        GreaterThanRegisterRegister,
        EqualImmediateRegister,
        EqualRegisterImmediate,
        EqualRegisterRegister
    }


    public abstract class OpCodeInstruction
    {
        public int OpCode { get; }
        public int A { get; }
        public int B { get; }
        public int C { get; }  

        public abstract OpCodeInstructionType Type { get; }

        protected OpCodeInstruction(int opCode, int a, int b, int c)
        {
            A = a;
            OpCode = opCode;
            B = b;
            C = c;
        }

        public abstract int[] Execute(int[] registers);


        private static readonly Dictionary<OpCodeInstructionType, Func<int[], OpCodeInstruction>> instructionMap =
            new Dictionary<OpCodeInstructionType, Func<int[], OpCodeInstruction>>()
            {
                {OpCodeInstructionType.AddImmediate, (x) => new AddImmediate(x[0], x[1], x[2], x[3])},
                {OpCodeInstructionType.AddRegister, (x) => new AddRegister(x[0], x[1], x[2], x[3])},
                {OpCodeInstructionType.SetImmediate, (x) => new AssignImmediate(x[0], x[1], x[2], x[3])},
                {OpCodeInstructionType.SetRegister, (x) => new AssignRegister(x[0], x[1], x[2], x[3])},

                {OpCodeInstructionType.BitwiseOrImmediate, (x) => new BitwiseOrImmediate(x[0], x[1], x[2], x[3])},
                {OpCodeInstructionType.BitwiseOrRegister, (x) => new BitwiseOrRegister(x[0], x[1], x[2], x[3])},

                {OpCodeInstructionType.BitwiseAndImmediate, (x) => new BitwiseAndImmediate(x[0], x[1], x[2], x[3])},
                {OpCodeInstructionType.BitwiseAndRegister, (x) => new BitwiseAndRegister(x[0], x[1], x[2], x[3])},

                {OpCodeInstructionType.MultiplyImmediate, (x) => new MultiplyImmediate(x[0], x[1], x[2], x[3])},
                {OpCodeInstructionType.MultiplyRegister, (x) => new MultiplyRegister(x[0], x[1], x[2], x[3])},

                {OpCodeInstructionType.GreaterThanRegisterImmediate, (x) => new GreaterThanRegisterImmediate(x[0], x[1], x[2], x[3])},
                {OpCodeInstructionType.GreaterThanImmediateRegister, (x) => new GreaterThanImmediateRegister(x[0], x[1], x[2], x[3])},
                {OpCodeInstructionType.GreaterThanRegisterRegister, (x) => new GreaterThanRegisterRegister(x[0], x[1], x[2], x[3])},

                {OpCodeInstructionType.EqualRegisterImmediate, (x) => new EqualRegisterImmediate(x[0], x[1], x[2], x[3])},
                {OpCodeInstructionType.EqualRegisterRegister, (x) => new EqualRegisterRegister(x[0], x[1], x[2], x[3])},
                {OpCodeInstructionType.EqualImmediateRegister, (x) => new EqualImmediateRegister(x[0], x[1], x[2], x[3])}

            };

        
        public static OpCodeInstruction Create(OpCodeInstructionType instructionType, int[] input)
        {
            if (input.Length != 4)
            {
                throw new ArgumentException("Input must contain 4 items.");
            }
            return instructionMap[instructionType](input);
        }

        // Return one of each instruction type
        public static IEnumerable<OpCodeInstruction> GetAllInstructions(int[] input)
        {
            if (input.Length != 4)
            {
                throw new ArgumentException("Input must contain 4 items.");
            }

            var instructions = new List<OpCodeInstruction>();

            foreach (OpCodeInstructionType type in Enum.GetValues(typeof(OpCodeInstructionType)))
            {
                instructions.Add(instructionMap[type](input));
            }

            return instructions;
        }
    }

    public class AddRegister :OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.AddRegister;

        public AddRegister(int opCode, int a, int b, int c) : base(opCode, a, b, c) { }

        public override int[] Execute(int[] input)
        {
            int[] output = input.ToArray();

            output[C] = input[A] + input[B];
            return output;
        }
    }

    public class AddImmediate : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.AddImmediate;
        public AddImmediate(int opCode, int a, int b, int c) : base(opCode, a, b, c) { }

        public override int[] Execute(int[] input)
        {
            int[] output = input.ToArray();

            output[C] = input[A] + B;
            return output;
        }
    }

    public class MultiplyRegister : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.MultiplyRegister;
        public MultiplyRegister(int opCode, int a, int b, int c) : base(opCode, a, b, c) { }

        public override int[] Execute(int[] input)
        {
            int[] output = input.ToArray();

            output[C] = input[A] * input[B];
            return output;
        }
    }

    public class MultiplyImmediate : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.MultiplyImmediate;
        public MultiplyImmediate(int opCode, int a, int b, int c) : base(opCode, a, b, c) { }

        public override int[] Execute(int[] input)
        {
            int[] output = input.ToArray();

            output[C] = input[A] * B;
            return output;
        }
    }

    public class BitwiseAndRegister : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.BitwiseAndRegister;
        public BitwiseAndRegister(int opCode, int a, int b, int c) : base(opCode, a, b, c) { }

        public override int[] Execute(int[] input)
        {
            int[] output = input.ToArray();

            output[C] = input[A] & input[B];
            return output;
        }
    }

    public class BitwiseAndImmediate : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.BitwiseAndImmediate;
        public BitwiseAndImmediate(int opCode, int a, int b, int c) : base(opCode, a, b, c) { }

        public override int[] Execute(int[] input)
        {
            int[] output = input.ToArray();

            output[C] = input[A] & B;
            return output;
        }
    }


    public class BitwiseOrRegister : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.BitwiseOrRegister;
        public BitwiseOrRegister(int opCode, int a, int b, int c) : base(opCode, a, b, c) { }

        public override int[] Execute(int[] input)
        {
            int[] output = input.ToArray();

            output[C] = input[A] | input[B];
            return output;
        }
    }

    public class BitwiseOrImmediate : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.BitwiseOrImmediate;
        public BitwiseOrImmediate(int opCode, int a, int b, int c) : base(opCode, a, b, c) { }

        public override int[] Execute(int[] input)
        {
            int[] output = input.ToArray();

            output[C] = input[A] | B;
            return output;
        }
    }

    public class AssignRegister : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.SetRegister;
        public AssignRegister(int opCode, int a, int b, int c) : base(opCode, a, b, c) { }

        public override int[] Execute(int[] input)
        {
            int[] output = input.ToArray();

            output[C] = input[A];
            return output;
        }
    }

    public class AssignImmediate : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.SetImmediate;
        public AssignImmediate(int opCode, int a, int b, int c) : base(opCode, a, b, c) { }

        public override int[] Execute(int[] input)
        {
            int[] output = input.ToArray();

            output[C] = A;
            return output;
        }
    }

    public class GreaterThanImmediateRegister : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.GreaterThanImmediateRegister;
        public GreaterThanImmediateRegister(int opCode, int a, int b, int c) : base(opCode, a, b, c) { }

        public override int[] Execute(int[] input)
        {
            int[] output = input.ToArray();

            output[C] = Convert.ToInt32(A > input[B]);
            return output;
        }
    }

    public class GreaterThanRegisterImmediate : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.GreaterThanRegisterImmediate;
        public GreaterThanRegisterImmediate(int opCode, int a, int b, int c) : base(opCode, a, b, c) { }

        public override int[] Execute(int[] input)
        {
            int[] output = input.ToArray();

            output[C] = Convert.ToInt32(input[A] > B);
            return output;
        }
    }

    public class GreaterThanRegisterRegister : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.GreaterThanRegisterRegister;
        public GreaterThanRegisterRegister(int opCode, int a, int b, int c) : base(opCode, a, b, c) { }

        public override int[] Execute(int[] input)
        {
            int[] output = input.ToArray();

            output[C] = Convert.ToInt32(input[A] > input[B]);
            return output;
        }
    }

    public class EqualImmediateRegister : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.EqualImmediateRegister;
        public EqualImmediateRegister(int opCode, int a, int b, int c) : base(opCode, a, b, c) { }

        public override int[] Execute(int[] input)
        {
            int[] output = input.ToArray();

            output[C] = Convert.ToInt32(A == input[B]);
            return output;
        }
    }

    public class EqualRegisterImmediate : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.EqualRegisterImmediate;
        public EqualRegisterImmediate(int opCode, int a, int b, int c) : base(opCode, a, b, c) { }

        public override int[] Execute(int[] input)
        {
            int[] output = input.ToArray();

            output[C] = Convert.ToInt32(input[A] == B);
            return output;
        }
    }

    public class EqualRegisterRegister : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.EqualRegisterRegister;
        public EqualRegisterRegister(int opCode, int a, int b, int c) : base(opCode, a, b, c) { }

        public override int[] Execute(int[] input)
        {
            int[] output = input.ToArray();

            output[C] = Convert.ToInt32(input[A] == input[B]);
            return output;
        }
    }


}
