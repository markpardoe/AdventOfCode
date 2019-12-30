using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day16
{
    public class FlawedFrequencyTransmission
    {
        private readonly List<int> _basePattern = new List<int>() { 0, 1, 0, -1 };
        private readonly Dictionary<int, IList<int>> _cachedPatterns = new Dictionary<int, IList<int>>();

        private readonly List<int> _data;

        public FlawedFrequencyTransmission(string input)
        {
            _data = BuildData(input, 1);
        }

        public string DecodeSignal(int numberOfRepeats, int numberOfPhases, int offSetLength)
        {
            List<int> data = new List<int>();
            for (int i = 0; i < numberOfRepeats; i++)
            {
                data.AddRange(_data);  // make a copy of the input
            }

            // Get the offset from the first <offSetLength> digits in the data
            int offSet = Int32.Parse(string.Join("",_data.GetRange(0, offSetLength)));

            // Remove the offset from the initial data.
            // We know the offset is in the 2nd half of the data - so we can just skip the data before it.
            data = data.Skip(offSet).ToList();

            for (int i = 1; i <= numberOfPhases; i++)
            {
                int result = 0;

                // Each digit in the 2nd half of the data obays the rule:
                //  Value = Data[ix] + ResultFor[ix+1]  (so for last digit its just Data[ix])
                // So rather than calulating every digit - we can just calculate the last digit
                //  then work inwards, adding the current value to the total so far.
                for (int ix = data.Count - 1; ix >= 0; ix--)
                {
                    result += data[ix];
                    result = Math.Abs(result % 10);
                    data[ix] = result;
                }
            }

            return String.Join("", data.GetRange(0, 8));
        }

        public List<int> VerifySignal(int numOfPhases)
        {           
            List<int> result = new List<int>(_data);  // make a copy of input data
            for (int i = 0; i < numOfPhases; i++)
            {
                result = PhaseShift(result);
            }
            return result;
        }

        private List<int> PhaseShift(List<int> data)
        {
            List<int> result = new List<int>(data.Count);
            
            for (int i = 1; i<= data.Count; i++)
            {
                if (!_cachedPatterns.ContainsKey(i))
                {
                    _cachedPatterns.Add(i, GeneratePattern(i));
                }
                var pattern = _cachedPatterns[i];
                result.Add(GenerateDigit(data, pattern));

            }
            return result;
        }

        private List<int> BuildData(string input, int numRepeats = 1)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < numRepeats; i++)
            {
                sb.Append(input);
            }

            // Convert each character to a digit and return a list
            return sb.ToString().Select(c => Int32.Parse(c.ToString())).ToList();
        }
                
        
        private int GenerateDigit(List<int> input, IList<int> pattern)
        {
            int total = 0;
            for (int i = 0; i < input.Count; i++)
            {
                total += (input[i] * pattern[i]);
            }

            total = Math.Abs(total);
            while (total >= 10)
            {
                total = total % 10;
            }
            return total;
        }

        private IList<int> GeneratePattern(int numRepeats)
        {
            var pattern = new RepeatingList<int>();

            foreach (int v in _basePattern) 
            { 
                for (int j =0; j < numRepeats;j++)
                {
                    pattern.Add(v);
                }
            }

            pattern.Add(_basePattern[0]);  // add first digit to end - this ensures that future repeating patterns contain the right-number of the 1st element
            pattern.RemoveAt(0); // cut off the first digit -
            return pattern; 
        }
    }
}
