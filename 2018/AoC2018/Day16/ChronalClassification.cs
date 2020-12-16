using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using Aoc.Aoc2018.Common;
using Aoc.Aoc2018.Common.OpCode;
using AoC.Common;

namespace Aoc.Aoc2018.Day16
{
    public class ChronalClassification : AoCSolution<long>
    {
        public override int Year => 2018;
        public override int Day => 16;
        public override string Name => "Day 16: Chronal Classification";
        public override string InputFileName => "Day16.txt";

        public override IEnumerable<long> Solve(IEnumerable<string> input)
        {
            var inputData = ParseInputData(input.ToList());
            var tests = inputData.Item1;
            var instructions = inputData.Item2; // 2nd half of input is a program
            
            // Count the number of tests that have at least 3 matching instructionTypes.
            int count = 0;
            foreach (var test in tests)
            {
                var matched = FindValidInstructionsForTest(test);
                if (matched.Count >= 3)
                {
                    count++;
                }
            }
            
            yield return count;

            // Finds which int value maps to each opcode
            var instructionMapping = FindOpCodeInstructions(tests);
            var register = RunInstructions(instructionMapping, instructions);

            Console.WriteLine($"[{register[0]}, {register[1]}, {register[2]}, {register[3]}]");
            yield return register[0];
        }

        private long[] RunInstructions(Dictionary<int, OpCodeInstructionType> instructionMap, List<int[]> instructions)
        {
            long[] register = new long[4];

            foreach (int[] values in instructions)
            {
                OpCodeInstructionType type = instructionMap[values[0]];
                OpCodeInstruction instruction = OpCodeInstruction.Create(type, values.Skip(1).ToArray());
                register = instruction.Execute(register);
            }

            return register;
        }

        private Tuple<List<OpcodeTestGroup>, List<int[]>> ParseInputData(List<string> input)
        {
            List<OpcodeTestGroup> grps = new List<OpcodeTestGroup>();
            List<int[]> instructions = new List<int[]>();

            int lineCount = 0;

            // Get tests by grabbing groups of 3 lines, and skipping over blank line afterwards
            for (; lineCount< input.Count; lineCount +=4)
            {
                if (!input[lineCount].StartsWith('B')) break;  // Reached end of input set
                var testGroup = new OpcodeTestGroup(input[lineCount], input[lineCount + 2], input[lineCount + 1]);
                grps.Add(testGroup);
            }

            // Get the instructions
            for (; lineCount < input.Count; lineCount++)
            {
                if (string.IsNullOrWhiteSpace(input[lineCount])) continue;  // ignore blank lines
                instructions.Add(input[lineCount].Split(' ').Select(x => int.Parse(x)).ToArray());
            }

            return new Tuple<List<OpcodeTestGroup>, List<int[]>>(grps, instructions);
        }


        /// <summary>
        /// Finds all the instruction types that could be valid for this test.
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        private ICollection<OpCodeInstructionType> FindValidInstructionsForTest(OpcodeTestGroup test)
        {
            var instructions = OpCodeInstruction.GetAllInstructions(test.Instructions);
            List<OpCodeInstructionType> matches = new List<OpCodeInstructionType>();

            foreach (var instruction in instructions)
            {
                if (CheckOpCode(instruction, test))
                {
                    matches.Add(instruction.Type);
                }
            }

            return matches;
        }


        /// <summary>
        /// Calculates which Opcode (0-16) relates to which opcodeInstructionType.
        /// For each Opcode, we can find a list of all the types that pass all tests containing that OpCode.
        /// That list can then be reduced by looking at any Instructions with only one match - and removing that matched type from all other opcodes.
        ///
        /// Eg.
        ///     0 = [addr, mulr, banr]
        ///     1 = [addr, mulr]
        ///     2 = [addr]
        ///
        ///   Reduces to:
        ///     0 = banr
        ///     1 - mulr
        ///     2 = addr
        /// </summary>
        /// <param name="tests"></param>
        /// <returns></returns>
        public Dictionary<int, OpCodeInstructionType> FindOpCodeInstructions(List<OpcodeTestGroup> tests)
        {
            // Foreach opcode number (0 - 16) - find all the instruction types that are valid for it.
            var matchedCodesPerOpCode = new Dictionary<int, List<OpCodeInstructionType>>();

            for (int i = 0; i < 16; i++)
            {
               List<OpCodeInstructionType> matchedCodes = new List<OpCodeInstructionType>();

                foreach (OpCodeInstructionType type in Enum.GetValues(typeof(OpCodeInstructionType)))
                {
                    if (IsOpcodeValidForInstructionType(i, type, tests))
                    {
                        matchedCodes.Add(type);
                    }
                }

                matchedCodesPerOpCode.Add(i, matchedCodes);
            }

            // Sort output so that we only have one type per code.
            // Start with any with count 1 - and remove that type from all other groups.

             // Keep track of which ones we've already filter
             var matchedTypes = new Dictionary<int, OpCodeInstructionType>();

             while (matchedCodesPerOpCode.Keys.Count > 0)
             {
                 var singleValue = matchedCodesPerOpCode.First(x => x.Value.Count == 1);
                 var instructionType = singleValue.Value[0];
                 Console.WriteLine($"Matched: {singleValue.Key} to {instructionType}");
                 // Since only one type found - we know its a match
                 matchedTypes.Add(singleValue.Key, instructionType);
                 matchedCodesPerOpCode.Remove(singleValue.Key);

                 // Remove the matched instructionType from all other groups.
                 // This should then give us another grouping with only one match
                 foreach (var key in matchedCodesPerOpCode.Keys)
                 {
                     if (matchedCodesPerOpCode[key].Contains(instructionType))
                     {
                         matchedCodesPerOpCode[key].Remove(instructionType);
                     }
                 }
             }

             return matchedTypes;
        }

        /// <summary>
        /// Checks if a given instruction type is valid for all tests with the specified number.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="type"></param>
        /// <param name="opCodeTests"></param>
        /// <returns></returns>
        private bool IsOpcodeValidForInstructionType(int code, OpCodeInstructionType type, List<OpcodeTestGroup> opCodeTests)
        {
            var tests = opCodeTests.Where(x => x.OpCode == code);

  
            foreach (var test in tests)
            {
                OpCodeInstruction instruction = OpCodeInstruction.Create(type, test.Instructions);

                if (!CheckOpCode(instruction, test))
                {
                    return false;
                }
            }
            return true;
        }

        // Check if an instruction is valid for the test
        private bool CheckOpCode(OpCodeInstruction instruction, OpcodeTestGroup test)
        {
            long[] after = instruction.Execute(test.Before);
            return after.SequenceEqual(test.After);
        }


        private readonly List<string> Example1 = new List<string>()
        {
            "Before: [3, 2, 1, 1]",
            "9 2 1 2",
            "After:  [3, 2, 2, 1]"
        };
    }


    public class OpcodeTestGroup
    {
        private const string pattern = @"^\D+\[(?<CodeA>\d*),\s?(?<CodeB>\d*),\s?(?<CodeC>\d*),\s?(?<CodeD>\d*)\s?\]$";

        public long[] Before { get; }
        public long[] After { get; }
        public int OpCode { get; }
        public int[] Instructions { get; }


        public OpcodeTestGroup(string before, string after, string opCodes)
        {
            Before = GetInputValues(before);
            After = GetInputValues(after);

            var values = opCodes.Split(" ").Select(a => int.Parse(a)).ToList();
            OpCode = values[0];
            Instructions = values.Skip(1).ToArray();
        }

        private long[] GetInputValues(string input)
        {
            long[] result = new long[4];
            var match = Regex.Match(input, pattern);

            result[0] = int.Parse(match.Groups["CodeA"].Value);
            result[1] = int.Parse(match.Groups["CodeB"].Value);
            result[2] = int.Parse(match.Groups["CodeC"].Value);
            result[3] = int.Parse(match.Groups["CodeD"].Value);
            return result;
        }
    }
}
