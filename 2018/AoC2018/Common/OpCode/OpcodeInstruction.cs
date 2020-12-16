using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.Aoc2018.Common.OpCode
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
        public int A { get; }
        public int B { get; }
        public int C { get; }  

        public abstract OpCodeInstructionType Type { get; }

        protected OpCodeInstruction(int a, int b, int c)
        {
            A = a;
            B = b;
            C = c;
        }

        public override string ToString()
        {
            return $"{Type} {A} {B} {C}";
        }

        public abstract long[] Execute(long[] registers);

        private static readonly Dictionary<OpCodeInstructionType, Func<IList<int>, OpCodeInstruction>> instructionMap =
            new Dictionary<OpCodeInstructionType, Func<IList<int>, OpCodeInstruction>>()
            {
                {OpCodeInstructionType.AddImmediate, (x) => new AddImmediate(x[0], x[1], x[2])},
                {OpCodeInstructionType.AddRegister, (x) => new AddRegister(x[0], x[1], x[2])},
                {OpCodeInstructionType.SetImmediate, (x) => new AssignImmediate(x[0], x[1], x[2])},
                {OpCodeInstructionType.SetRegister, (x) => new AssignRegister(x[0], x[1], x[2])},

                {OpCodeInstructionType.BitwiseOrImmediate, (x) => new BitwiseOrImmediate(x[0], x[1], x[2])},
                {OpCodeInstructionType.BitwiseOrRegister, (x) => new BitwiseOrRegister(x[0], x[1], x[2])},

                {OpCodeInstructionType.BitwiseAndImmediate, (x) => new BitwiseAndImmediate(x[0], x[1], x[2])},
                {OpCodeInstructionType.BitwiseAndRegister, (x) => new BitwiseAndRegister(x[0], x[1], x[2])},

                {OpCodeInstructionType.MultiplyImmediate, (x) => new MultiplyImmediate(x[0], x[1], x[2])},
                {OpCodeInstructionType.MultiplyRegister, (x) => new MultiplyRegister(x[0], x[1], x[2])},

                {OpCodeInstructionType.GreaterThanRegisterImmediate, (x) => new GreaterThanRegisterImmediate(x[0], x[1], x[2])},
                {OpCodeInstructionType.GreaterThanImmediateRegister, (x) => new GreaterThanImmediateRegister(x[0], x[1], x[2])},
                {OpCodeInstructionType.GreaterThanRegisterRegister, (x) => new GreaterThanRegisterRegister(x[0], x[1], x[2])},

                {OpCodeInstructionType.EqualRegisterImmediate, (x) => new EqualRegisterImmediate(x[0], x[1], x[2])},
                {OpCodeInstructionType.EqualRegisterRegister, (x) => new EqualRegisterRegister(x[0], x[1], x[2])},
                {OpCodeInstructionType.EqualImmediateRegister, (x) => new EqualImmediateRegister(x[0], x[1], x[2])}
            };

        private static readonly Dictionary<string, OpCodeInstructionType> opCodeNames =
            new Dictionary<string, OpCodeInstructionType>()
            {
                {"addr", OpCodeInstructionType.AddRegister},
                {"addi", OpCodeInstructionType.AddImmediate},
                {"mulr", OpCodeInstructionType.MultiplyRegister},
                {"muli", OpCodeInstructionType.MultiplyImmediate},
                {"banr", OpCodeInstructionType.BitwiseAndRegister},
                {"bani", OpCodeInstructionType.BitwiseAndImmediate},
                {"borr", OpCodeInstructionType.BitwiseOrRegister},
                {"bori", OpCodeInstructionType.BitwiseOrImmediate},
                {"setr", OpCodeInstructionType.SetRegister},
                {"seti", OpCodeInstructionType.SetImmediate},
                {"gtir", OpCodeInstructionType.GreaterThanImmediateRegister},
                {"gtri", OpCodeInstructionType.GreaterThanRegisterImmediate},
                {"gtrr", OpCodeInstructionType.GreaterThanRegisterRegister},
                {"eqir", OpCodeInstructionType.EqualImmediateRegister},
                {"eqri", OpCodeInstructionType.EqualRegisterImmediate},
                {"eqrr", OpCodeInstructionType.EqualRegisterRegister}
            };

        
        public static OpCodeInstruction Create(OpCodeInstructionType instructionType, int[] input)
        {
            if (input.Length != 3)
            {
                throw new ArgumentException("Input must contain 3 items.");
            }
            return instructionMap[instructionType](input);
        }

        public static OpCodeInstruction Create(string opcode, IList<int> input)
        {
            if (input.Count != 3)
            {
                throw new ArgumentException("Input must contain 3 items.");
            }
            return instructionMap[opCodeNames[opcode]](input);
        }

        // Return one of each instruction type
        public static IEnumerable<OpCodeInstruction> GetAllInstructions(int[] input)
        {
            if (input.Length != 3)
            {
                throw new ArgumentException("Input must contain 3 items.");
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

        public AddRegister(int a, int b, int c) : base( a, b, c) { }

        public override long[] Execute(long[] input)
        {
            long[] output = input.ToArray();

            output[C] = input[A] + input[B];
            return output;
        }
    }

    public class AddImmediate : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.AddImmediate;
        public AddImmediate(int a, int b, int c) : base( a, b, c) { }

        public override long[] Execute(long[] input)
        {
            long[] output = input.ToArray();

            output[C] = input[A] + B;
            return output;
        }
    }

    public class MultiplyRegister : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.MultiplyRegister;
        public MultiplyRegister(int a, int b, int c) : base( a, b, c) { }

        public override long[] Execute(long[] input)
        {
            long[] output = input.ToArray();

            output[C] = input[A] * input[B];
            return output;
        }
    }

    public class MultiplyImmediate : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.MultiplyImmediate;
        public MultiplyImmediate(int a, int b, int c) : base( a, b, c) { }

        public override long[] Execute(long[] input)
        {
            long[] output = input.ToArray();

            output[C] = input[A] * B;
            return output;
        }
    }

    public class BitwiseAndRegister : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.BitwiseAndRegister;
        public BitwiseAndRegister(int a, int b, int c) : base( a, b, c) { }

        public override long[] Execute(long[] input)
        {
            long[] output = input.ToArray();

            output[C] = input[A] & input[B];
            return output;
        }
    }

    public class BitwiseAndImmediate : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.BitwiseAndImmediate;
        public BitwiseAndImmediate(int a, int b, int c) : base( a, b, c) { }

        public override long[] Execute(long[] input)
        {
            long[] output = input.ToArray();

            output[C] = input[A] & B;
            return output;
        }
    }


    public class BitwiseOrRegister : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.BitwiseOrRegister;
        public BitwiseOrRegister(int a, int b, int c) : base( a, b, c) { }

        public override long[] Execute(long[] input)
        {
            long[] output = input.ToArray();

            output[C] = input[A] | input[B];
            return output;
        }
    }

    public class BitwiseOrImmediate : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.BitwiseOrImmediate;
        public BitwiseOrImmediate(int a, int b, int c) : base( a, b, c) { }

        public override long[] Execute(long[] input)
        {
            long[] output = input.ToArray();

            output[C] = input[A] |(long) B;
            return output;
        }
    }

    public class AssignRegister : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.SetRegister;
        public AssignRegister(int a, int b, int c) : base( a, b, c) { }

        public override long[] Execute(long[] input)
        {
            long[] output = input.ToArray();

            output[C] = input[A];
            return output;
        }
    }

    public class AssignImmediate : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.SetImmediate;
        public AssignImmediate(int a, int b, int c) : base( a, b, c) { }

        public override long[] Execute(long[] input)
        {
            long[] output = input.ToArray();

            output[C] = A;
            return output;
        }
    }

    public class GreaterThanImmediateRegister : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.GreaterThanImmediateRegister;
        public GreaterThanImmediateRegister(int a, int b, int c) : base( a, b, c) { }

        public override long[] Execute(long[] input)
        {
            long[] output = input.ToArray();

            output[C] = Convert.ToInt32(A > input[B]);
            return output;
        }
    }

    public class GreaterThanRegisterImmediate : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.GreaterThanRegisterImmediate;
        public GreaterThanRegisterImmediate(int a, int b, int c) : base( a, b, c) { }

        public override long[] Execute(long[] input)
        {
            long[] output = input.ToArray();

            output[C] = Convert.ToInt32(input[A] > B);
            return output;
        }
    }

    public class GreaterThanRegisterRegister : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.GreaterThanRegisterRegister;
        public GreaterThanRegisterRegister(int a, int b, int c) : base( a, b, c) { }

        public override long[] Execute(long[] input)
        {
            long[] output = input.ToArray();

            output[C] = Convert.ToInt32(input[A] > input[B]);
            return output;
        }
    }

    public class EqualImmediateRegister : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.EqualImmediateRegister;
        public EqualImmediateRegister(int a, int b, int c) : base( a, b, c) { }

        public override long[] Execute(long[] input)
        {
            long[] output = input.ToArray();

            output[C] = Convert.ToInt32(A == input[B]);
            return output;
        }
    }

    public class EqualRegisterImmediate : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.EqualRegisterImmediate;
        public EqualRegisterImmediate(int a, int b, int c) : base( a, b, c) { }

        public override long[] Execute(long[] input)
        {
            long[] output = input.ToArray();

            output[C] = Convert.ToInt32(input[A] == B);
            return output;
        }
    }

    public class EqualRegisterRegister : OpCodeInstruction
    {
        public override OpCodeInstructionType Type => OpCodeInstructionType.EqualRegisterRegister;
        public EqualRegisterRegister(int a, int b, int c) : base( a, b, c) { }

        public override long[] Execute(long[] input)
        {
            long[] output = input.ToArray();

            output[C] = Convert.ToInt32(input[A] == input[B]);
            return output;
        }
    }


}
