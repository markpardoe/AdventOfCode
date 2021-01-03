using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using AoC.Common;

namespace AoC.AoC2020.Problems.Day18
{

    /// <summary>
    /// Uses Shunting-yard algorithm https://en.wikipedia.org/wiki/Shunting-yard_algorithm
    /// to convert input into Reverse-Polish notation based on the operator precedence.
    /// Bracketed expressions are solved recursively and added to the output as their final value
    /// The final
    /// </summary>
    public class OperationOrder : AoCSolution<long>
    {
        public override int Year => 2020;
        public override int Day => 18;
        public override string Name => "Day 18: Operation Order";
        public override string InputFileName => "Day18.txt";

        public override IEnumerable<long> Solve(IEnumerable<string> input)
        {
            long part1Total = 0;
            long part2Total = 0;
            foreach (var line in input)
            {
                var operations = line.ToCharArray().Where(x => x != ' ').Select(x => x.ToString());

                long part1 = Evaluate(new Queue<string>(operations), _precedencePart1);
                long part2 = Evaluate(new Queue<string>(operations), _precedencePart2);

                part1Total += part1;
                part2Total += part2;

              //  Console.WriteLine($"[{line}] = {part1} & {part2}");
              //  total += val;
               // Console.WriteLine($"Total = {val}");
            }

            yield return part1Total;
            yield return part2Total;
        }

        private enum Instruction
        {
            Add,
            Multiply
        }

        private long EvaluateLeftToRight(Queue<char> operations)
        {
            long finalValue = 0;

            var currentInstruction = Instruction.Add;

            while (operations.Count > 0)
            {
                var op = operations.Dequeue();

                if (op >= 48 && op <= 57)
                {
                    // must be number
                    var num = (long) op - 48;
                    finalValue = PerformInstruction(finalValue, num, currentInstruction);
                }
                else if (op == '(')
                {
                    finalValue = PerformInstruction(finalValue, EvaluateLeftToRight(operations), currentInstruction);
                }
                else if (op == ')')
                {
                    return finalValue;
                }
                else if (op == '*')
                {
                    currentInstruction = Instruction.Multiply;
                }
                else if (op == '+')
                {
                    currentInstruction = Instruction.Add;
                }
                else
                {
                    throw new InvalidOperationException($"Invalid character in input data: [{op}]");
                }
            }

            return finalValue;
        }

        private long Evaluate(Queue<string> input, Dictionary<string, int> precedence)
        {
            Queue<string> output = new Queue<string>();
            Stack<string> ops = new Stack<string>();
            
            while (input.Count > 0)
            {
                var token = input.Dequeue();
   
                if (int.TryParse(token, out _))
                {
                    output.Enqueue(token);
                }

                else if (token == "(")
                {
                    output.Enqueue(Evaluate(input, precedence).ToString()); // start a new eval for the bracketed group of input and queue the result
                }
                else if (token == ")")
                {
                    // finish the bracketed group and return the result
                    break;
                }
                else if (token == "*" || token == "+")
                {
                    // move any tokens from the ops stack with a higher precedence to the output stack
                    // we use >= as we want it to default to Left-to-right precedence if the token precedences are equal
                    while (ops.TryPeek(out var prev)  &&  precedence[prev] >= precedence[token])
                    {
                        output.Enqueue(ops.Pop());
                    }
                    ops.Push(token);
                }
                else
                {
                    throw new InvalidOperationException($"Invalid character in input data: [{token}]");
                }
            }

            // empty the ops stack
            while (ops.Count > 0)
            {
                output.Enqueue(ops.Pop());
            }

            return EvaluateReversePolish(output);
        }

        // Part 2 - set precedence of Add higher than multiply
        private readonly Dictionary<string, int> _precedencePart2 = new Dictionary<string, int>()
        {
            {"+", 1},
            {"*", 0}
        };

        // Part 1 - set precedence the same.  So left to right takes precedence
        private readonly Dictionary<string, int> _precedencePart1 = new Dictionary<string, int>()
        {
            {"+", 0},
            {"*", 0}
        };

        // Evaluates a queue of strings making up an expression in reverse-polish notation
        // and returns the result
        private long EvaluateReversePolish(Queue<string> ops)
        {
            Stack<long> values = new Stack<long>();

            while (ops.Count > 0)
            {
                string token = ops.Dequeue();
                if (long.TryParse(token, out var val))
                {
                    values.Push(val);
                }
                else
                {
                    long val1 = values.Pop();
                    long val2 = values.Pop();
                    if (token == "*")
                    {
                        values.Push(val1 * val2);
                    }
                    else if (token == "+")
                    {
                        values.Push(val1 + val2);
                    }
                    else
                    {
                        throw new InvalidDataException("Invalid Token: " + token);
                    }
                }
            }

            // should only be 1 value left
            Debug.Assert(values.Count == 1);
            return values.Pop();
        }

        private long PerformInstruction(long value1, long value2, Instruction instruction)
        {
            if (instruction == Instruction.Add)
            {
                return value1 + value2;
            }
            else
            {
                return value1 * value2;
            }
        }

        private readonly List<string> example = new List<string>()
        {
            "2 * 3 + (4 * 5)",
            "5 + (8 * 3 + 9 + 3 * 4 * 3)",
            "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))",
            "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2"
        };
        
    }
}
