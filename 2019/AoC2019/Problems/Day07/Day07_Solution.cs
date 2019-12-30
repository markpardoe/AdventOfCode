using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Aoc.AoC2019.IntCode;
using AoC.Common;

namespace Aoc.AoC2019.Problems.Day07
{
    public class Day07_Solution : ISolution
    {
        public int Year => 2019;

        public int Day => 7;

        public string Name => "Day 7: Amplification Circuit";

        public string InputFileName => "Day07.txt";

        public IEnumerable<string> Solve(IEnumerable<string> input)
        {
            yield return FindHighestSignal(input.First()).ToString();
            yield return FindHighestSignalWithFeedback(input.First()).ToString();
        }

        public long FindHighestSignal(string intCode)
        {
            long maxValue = Int64.MinValue;
            var data = IntCodeVM.ParseStringData(intCode);

            var initialSettings = new List<long>(){ 0, 1, 2, 3, 4 };
            var permutations = initialSettings.GetPermutations(5).Select(a => a.ToList()).ToList();
           

            foreach (List<long> currentSetting in permutations)
            {
                AmplificationController controller = new AmplificationController(currentSetting, data);
                long result = controller.RunAmplifiedCircuits(0);

                if (result > maxValue)
                {
                    maxValue = result;
                }
            }
            return maxValue;
        }

        public long FindHighestSignalWithFeedback(string intCode)
        {
            long maxValue = Int64.MinValue;
            var initialSettings = new List<long>() { 5, 6, 7, 8, 9 };

            var code = IntCodeVM.ParseStringData(intCode);

            var perms = initialSettings.GetPermutations(5).Select(a => a.ToList()).ToList();

            foreach (List<long> settings in perms)
            {
                FeedbackAmplificationController controller = new FeedbackAmplificationController(settings, code);
                long result = controller.RunAmplifiedCircuits(0);

                if (result > maxValue)
                {
                    maxValue = result;
                }
            }

            return maxValue;
        }
    }
}
