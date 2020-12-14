using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC.AoC2020.Problems.Day14
{
    public class DockingController
    {
       private readonly string memPattern = @"^mem\[(?<address>\d+)]\s*=\s*(?<value>\d+)\s*";

        public DockingController()
        { }

        public long UpdateValues(IEnumerable<string> input)
        {
            Dictionary<int, long> memory = new Dictionary<int, long>();
            Bitmask mask = null;

            foreach (string line in input)
            {
                if (line.StartsWith("mask"))
                {
                    mask = new Bitmask(line);
                }
                else if(line.StartsWith("mem"))
                {
                    var match = Regex.Match(line, memPattern);

                    var address = int.Parse(match.Groups["address"].Value);
                    var value = long.Parse(match.Groups["value"].Value);
                    value = mask.CalculateValue(value);

                    if (!memory.ContainsKey(address))
                    {
                        memory.Add(address, value);
                    }
                    else
                    {
                        memory[address] = value;
                    }
                }
            }

            return memory.Values.Sum();
        }

        public long UpdateAddresses(IEnumerable<string> input)
        {
            Dictionary<long, long> memory = new Dictionary<long, long>();
            Bitmask mask = null;

            foreach (string line in input)
            {
                if (line.StartsWith("mask"))
                {
                    mask = new Bitmask(line);
                }
                else if (line.StartsWith("mem"))
                {
                    var match = Regex.Match(line, memPattern);

                    var add = int.Parse(match.Groups["address"].Value);
                    var value = long.Parse(match.Groups["value"].Value);


                    var addresses  = mask.GenerateAddresses(add);
                    
                    foreach (long address in addresses)
                    {
                        if (!memory.ContainsKey(address))
                        {
                            memory.Add(address, value);
                        }
                        else
                        {
                            memory[address] = value;
                        }
                    }
                }
            }

            return memory.Values.Sum();
        }

        private class Bitmask {
            private string Mask { get; }
            private readonly char[] _mask;

            public Bitmask(string mask)
            {
                this.Mask = mask.Replace("mask =", "").Trim();
                _mask = Mask.ToCharArray();
            }

            public override string ToString()
            {
                return Mask;
            }

            // Returns the specified value put through the bitmask operation
            public long CalculateValue(long value)
            {
                var binary = ConvertDecimalToBinary(value);
                
                // convert each character using the mask
                for (int i = 0; i < Mask.Length; i++)
                {
                    char c = _mask[i];

                    if (c != 'X')
                    {
                        binary[i] = c;
                    }
                }
                string s = new string(binary);

                // Convert back to long
                return Convert.ToInt64(s, 2);
            }

            // Converts the memory address using the Bitmask into a set of addresses
            public IEnumerable<long> GenerateAddresses(long address)
            {

                var permutations = new HashSet<List<char>> { ConvertDecimalToBinary(address).ToList() };

                for (int i = 0; i < Mask.Length; i++)
                {
                    char c = _mask[i];

                    // set each corresponding address bit with 1
                    if (c == '1')
                    {
                        foreach (var permutation in permutations)
                        {
                            permutation[i] = '1';
                        }
                    }
                    else if (c == 'X')
                    {
                       
                        // We can't add new permutations to the parent collection during enumeration
                        // so copy to a new set and add at the end
                        var newPermutations = new HashSet<List<char>>();

                       // char[] newArray = new char[_mask.Length];
                        foreach (var permutation in permutations)
                        {
                            // Create a copy of the array.
                            // We want to use both values in the address - so create a copy and update one with '1' and the other with '0;
                            var copy = permutation.ToList();  // creates a copy

                            permutation[i] = '1';
                            copy[i] = '0';
                            newPermutations.Add(copy);
                        }

                        foreach (var l in newPermutations)
                        {
                            permutations.Add(l);
                        }
                    }
                }
                
                return permutations.Select(x => ConvertCharArrayToLong(x));
            }

            // Convert decimal value into a char array.
            // The array will be padded with leading zeros to make it match the length of the mask for easy operations
            private char[] ConvertDecimalToBinary(long value)
            {
                string binary = Convert.ToString(value, 2);
                binary = binary.PadLeft(_mask.Length, '0'); // pad with leading zeros to make the 2 strings the same length

                return binary.ToCharArray(); 
            }

            private long ConvertCharArrayToLong(IEnumerable<char> binary)
            {
                return Convert.ToInt64(new string(binary.ToArray()), 2);
            }
        }

    }
}