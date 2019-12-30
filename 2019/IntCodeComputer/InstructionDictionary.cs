using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Aoc.AoC2019.IntCode
{
    /// <summary>
    /// Container for instructions in an IntCodeCompiler.  
    /// Uses a Dictionary<index, value> and returns a default value (0) if the given Index is empty.
    /// This allows us to reference indexes outside the initial list, and use any value as an index.
    /// /// </summary>
    internal class InstructionDictionary
    {
        private readonly long DEFAULT_VALUE = 0;
        private readonly Dictionary<long, long> _instructions = new Dictionary<long, long>();

        internal InstructionDictionary(IEnumerable<long> instructions)
        {
            int index = 0;
            foreach (long instruction in instructions)
            {
                _instructions.Add(index, instruction);
                index++;                
            }
        }

        internal InstructionDictionary(IEnumerable<int> instructions)
        {
            int index = 0;
            foreach (long instruction in instructions)
            {
                _instructions.Add(index, instruction);
                index++;
            }
        }

        internal IEnumerable<long> Instructions()
        {
            return _instructions.Values;
        }


        internal long this[long index]
        {
            get
            {
                if (!_instructions.ContainsKey(index))
                {
                    return DEFAULT_VALUE;
                }
                else
                {
                    return _instructions[index];
                }
            }
            set
            {
                if (!_instructions.ContainsKey(index))
                {
                    _instructions.Add(index, value);
                }
                else
                {
                    _instructions[index] = value;
                }
            }
        }
    }
}