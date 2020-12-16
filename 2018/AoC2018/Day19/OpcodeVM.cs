using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Aoc.Aoc2018.Common.OpCode;

namespace Aoc.Aoc2018.Day19
{
    public class OpcodeVM
    {
        protected readonly int[] _register = new int[6];
        private readonly List<OpCodeInstruction> _instructions = new List<OpCodeInstruction>();
        protected int _registerIndex = 0;

        public int InstructionPointer => _register[_registerIndex];
        public IReadOnlyList<int> Register => _register.ToList();

        private string instructionPattern = @"^(?<instruction>\w+)\s(?<A>\d+)\s(?<B>\d+)\s(?<C>\d+)$";

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

        // Reverse-engineered solution.
        // Worked out what the problem input did and converted it to an optimised loop
        public void ExecuteFast()
        {
            // Set the target
            int target = _register[0] == 0 ? 896 : 10551296;


            // We'll loop through values at position 12 (the outer loop)
            _register[0] = 0; // reset to 1 to start
            _register[1] = 12;  // run it at step 12
            _register[3] = target+1;
            _register[4] = 1;
            _register[5] = target;
            

            // The program execution basically runs 2 loops:
            // for (_register[2] = 1 to _register[5])       - Lines 12 to 16
            //      for (_register[3] = 1 to _register[5])  - Lines 3 to 11
            //          if (_register[2] *  _register[3]) == register[5] THEN register[0] = register[0] + register[2]
            //
            // Obviously when _register[5] is very large, this is too long to brute-force it
            // So if we just skip the inside loop and increment _register[2] until we get the final result
            for (int i = 1; i <= _register[5]; i++)
            {
                _register[2] = i;

                // check if the target is divisible by register[2]
                if (target % i == 0)
                {
                    _register[0] += i;
                }

                string output = $"ip={InstructionPointer}, {string.Join(',', _register)}";
            }

            // finish the program
            ExecuteAll(); 
        }
    }
}