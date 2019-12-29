using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC2019.IntCodeComputer;

namespace AoC2019.Problems.Day25
{
    public class AdventureGame
    {
        private readonly IVirtualMachine _computer;

        public AdventureGame(IVirtualMachine computer)
        {
            _computer = computer ?? throw new ArgumentNullException(nameof(computer));
        }

        public void RunGame()
        {
            while (true)
            {
                if (_computer.Outputs.Count > 0)
                {
                    string output = AsciiToString(_computer.Outputs.DequeueToList());
                    Console.WriteLine(output);
                }
                if (_computer.Status == ExecutionStatus.Finished)
                {
                    return;
                } else if (_computer.Status == ExecutionStatus.AwaitingInput)
                {
                    string input = Console.ReadLine();
                    _computer.AddInput(StringToAscii(input).ToArray());
                }

                _computer.Execute();
            }
        }

        private string AsciiToString(IEnumerable<long> values)
        {
            StringBuilder sb = new StringBuilder();
            foreach(char c in values)
            {
                sb.Append(c.ToString());              
            }

            return sb.ToString();
        }

        public IEnumerable<long> StringToAscii(string value)
        {
            List<long> result = new List<long>();
            foreach(char c in value)
            {
                result.Add((long)c);
            }


            result.Add(10); // add trailing new line
            return result;
        }        
    }
}
