using AoC.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day16
{
    public class Day16_Solution : AoCSolution
    {
        public override int Year => 2019;

        public override int Day => 16;

        public override string Name => "Day 16: Flawed Frequency Transmission";

        public override string InputFileName => "Day16.txt";


        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {
            FlawedFrequencyTransmission fft = new FlawedFrequencyTransmission(input.First());           
            List<int> results = fft.VerifySignal(100);
            string result = String.Join("", results).Substring(0, 8);

            yield return result;
            
            yield return fft.DecodeSignal(10000 , 100, 7).ToString();
        }
    }
}
