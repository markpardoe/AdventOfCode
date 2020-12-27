using System;
using System.Collections.Generic;
using System.Text;
using AoC.Common;

namespace AoC.AoC2020.Problems.Day25
{
    public class ComboBreaker : AoCSolution<long>
    {
        public override IEnumerable<long> Solve(IEnumerable<string> input)
        {
            long loopSize1 = GetLoopSize(key1);
            long loopSize2 = GetLoopSize(key2);

            long encrypt1 = GetEncryptKey(key1, loopSize2);
            long encrypt2 = GetEncryptKey(key2, loopSize1);
            yield return encrypt1;
            yield return encrypt2;
        }

        private int GetLoopSize(int key)
        {
            int val = 1;
            int subject = 7;
            int loopSize = 0;

            while (val != key)
            {
                val = val * subject;
                val = val % 20201227;
                loopSize++;
            }

            return loopSize;
        }

        private long GetEncryptKey(long key, long loopSize)
        {
            long val = 1;
            long subject = key;

            for (int i = 0; i < loopSize; i++)
            {
                //Console.WriteLine($"{i}: {val}");
                val = val * subject;
                val = val % 20201227;
            }

            return val;
        }

        private int key1 = 11349501;
        private int key2 = 5107328;

        public override int Year => 2020;

        public override int Day => 25;

        public override string Name => "day 25";

        public override string InputFileName => null;
    }
}
