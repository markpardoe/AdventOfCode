using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Aoc.Aoc2018.Common.OpCode;

namespace Aoc.Aoc2018.Day19
{
    public class HackedOpcodeVM : OpcodeVM
    {
        
        public HackedOpcodeVM( IEnumerable<string> instructions, int initialValue) : base(instructions, initialValue) { }

        // Reverse-engineered solution for Day 19
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