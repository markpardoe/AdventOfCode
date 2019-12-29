using AoC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2019.Problems.Day16
{
    public class Day16_Solution : ISolution
    {
        public int Year => 2019;

        public int Day => 16;

        public string Name => "Day 16: Flawed Frequency Transmission";

        public string InputFileName => "Day16.txt";


        public IEnumerable<string> Solve(IEnumerable<string> input)
        {
            FlawedFrequencyTransmission fft = new FlawedFrequencyTransmission(input.First());
            int offSet = Int32.Parse(input.First().Substring(0, 7));
            List<int> results = fft.VerifySignal(100);
            string result = String.Join("", results).Substring(0, 8);

            yield return result;
            
            yield return fft.DecodeSignal(10000 , 100, offSet).ToString();
        }
    }
}
