using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc.Aoc2018.Common.OpCode
{
    public class OpcodeVM
    {
        protected readonly int[] _register = new int[6];
        private readonly List<OpCodeInstruction> _instructions = new List<OpCodeInstruction>();
        protected int _registerIndex = 0;

        public int InstructionPointer => _register[_registerIndex];
        public IReadOnlyList<int> Register => _register.ToList();

        private readonly string instructionPattern = @"^(?<instruction>\w+)\s(?<A>\d+)\s(?<B>\d+)\s(?<C>\d+)$";

        public bool IsRunning => _register[_registerIndex] < _instructions.Count;  // Still in range of instructions?

        public OpcodeVM( IEnumerable<string> instructions, int initialValue)
        {
            _register[0] = initialValue;

            foreach (var line in instructions)
            {
                if (line.StartsWith("#ip"))
                {
                    // get last character and set it as the index of the register to use.
                    _registerIndex = int.Parse(line[^1].ToString());
                }
                else
                {
                    var match = Regex.Match(line, instructionPattern);
                    string inst = match.Groups["instruction"].Value;
                    int a = int.Parse(match.Groups["A"].Value);
                    int b = int.Parse(match.Groups["B"].Value);
                    int c = int.Parse(match.Groups["C"].Value);

                    _instructions.Add(OpCodeInstruction.Create(inst, new List<int>{a,b,c}));
                }
            }
        }

        public void ExecuteNextInstruction()
        {
            int instructionIndex = _register[_registerIndex];

            var instruction = _instructions[instructionIndex];
            var newValues = instruction.Execute(_register);
            
            // Debugging output
            // string output = $"ip={InstructionPointer}, {string.Join(',', _register)}, {instruction.ToString()}, {string.Join(',',newValues)}";
            // Console.WriteLine(output);

            // Update results with new values
            for (int i = 0; i < newValues.Length; i++)
            {
                _register[i] = newValues[i];
            }

            _register[_registerIndex]++;
        }

        /// <summary>
        /// Runs the instructions until the end
        /// </summary>
        public void ExecuteAll()
        {
            while (_register[_registerIndex] < _instructions.Count)
            {
                ExecuteNextInstruction();
            }
        }
    }
}