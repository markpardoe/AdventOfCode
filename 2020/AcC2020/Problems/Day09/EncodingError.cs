using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using AoC.Common;

namespace AoC.AoC2020.Problems.Day09
{
    public class EncodingError : AoCSolution<long>
    {
        public override int Year => 2020;
        public override int Day => 9;
        public override string Name => "Day 9: Encoding Error";
        public override string InputFileName => "Day09.txt";

        public override IEnumerable<long> Solve(IEnumerable<string> input)
        {
            List<long> data = input.Select(long.Parse).ToList();
            var invalid =  FindFirstInvalidNumber(data, 25);

            yield return invalid;

            yield return FindContinuousSequence(data, invalid);
        }

        /// <summary>
        /// Finds the first number in the sequence that can't be found
        /// by adding 2 numbers in the previous <preambleSize> numbers.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="preambleSize"></param>
        /// <returns></returns>
        public long FindFirstInvalidNumber(IEnumerable<long> input, int preambleSize)
        {
            // hold list of numbers we can check against
            Queue<long> numQueue = new Queue<long>(input.Take(preambleSize));

            var data = input.Skip(preambleSize);

            foreach (var target in data)
            {
                if (!IsNumberValid(target, numQueue.ToList()))
                {
                    return target;
                }
                else
                {
                    numQueue.Dequeue();
                    numQueue.Enqueue(target);
                }
            }

            return -1;
        }

        /// <summary>
        /// Finds the sequence of continuous numbers in the list that added together equal the targetNumber
        /// Returns largest & smallest numbers in the sequence summed.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="targetNumber"></param>
        /// <returns></returns>
        public long FindContinuousSequence(IList<long> input, long targetNumber)
        {

            for (int i = 0; i <= input.Count; i++)
            {
                long total = input[i];
                List<long> seq = new List<long> {total};

                // Check that the 1st number in the sequence is valid.
                if (total >= targetNumber)
                {
                    // if its the same - then quit this iteration as we've just found the targetNumber in the list!
                    // if higher - no point continuing.
                    continue;;
                }


                for (int j = i + 1; j <= input.Count; j++)
                {
                    total += input[j];
                    seq.Add(input[j]);

                    if (total == targetNumber)
                    {
                        Console.Out.WriteLine($"Target ({targetNumber}) found: {seq.Min()}, {seq.Max()}");
                        return seq.Min() + seq.Max();
                    }
                    else if (total > targetNumber)
                    {
                        // we've missed the target - so quit this loop
                        break;
                    }
                }
            }

            return -1;
        }

        // Checks if a target number is valid
        // Only valid if it can be make up by the sum of 2 unique numbers in the [numbers] list.
        private bool IsNumberValid(long target, ICollection<long> numbers)
        {
            foreach (var x in numbers)
            {
                // subtract each available number from the target and check if the result is in the queue.
                // This is 25 operations per target number - as opposed to 25*25 if we try to calculate all the available sums.
                var diff = target - x;

                if (diff != x && numbers.Contains(diff))
                {
                    return true;
                }
            }

            return false;
        }
    }


   
}
