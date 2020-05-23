using AoC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Aoc.AoC2019.Problems.Day22
{
    public static class BigShuffle
    {
        public static string Part2(List<string> inputs)
        {
            BigInteger size = 119315717514047;
            BigInteger iter = 101741582076661;
            BigInteger position = 2020;
            BigInteger offset_diff = 0;
            BigInteger increment_mul = 1;

            foreach (var line in inputs)
            {
                RunP2(ref increment_mul, ref offset_diff, size, line);
            }

            (BigInteger increment, BigInteger offset) = GetSeq(iter, increment_mul, offset_diff, size);

            var card = Get(offset, increment, 2020, size);

            return card.ToString();
        }

        private static void RunP2(ref BigInteger inc_mul, ref BigInteger offset_diff, BigInteger size, string line)
        {
            if (line.StartsWith("cut"))
            {
                offset_diff += Int32.Parse(line.Split(" ").Last()) * inc_mul;
            }
            else if (line == "deal into new stack")
            {
                inc_mul *= -1;
                offset_diff += inc_mul;
            }
            else
            {
                var num = Int32.Parse(line.Split(" ").Last());

                inc_mul *= num.TBI().Inv(size);
            }

            inc_mul = inc_mul.Mod(size);
            offset_diff = offset_diff.Mod(size);
        }

        private static BigInteger Mod(this BigInteger x, BigInteger m)
        {
            return (x % m + m) % m;
        }

        private static BigInteger Inv(this BigInteger num, BigInteger size)
        {
            return num.Mpow(size - 2, size);
        }

        private static BigInteger Get(BigInteger offset, BigInteger increment, BigInteger i, BigInteger size)
        {
            return (offset + i * increment) % size;
        }

        private static (BigInteger increment, BigInteger offset) GetSeq(this BigInteger iterations, BigInteger inc_mul, BigInteger offset_diff, BigInteger size)
        {
            var increment = inc_mul.Mpow(iterations, size);

            var offset = offset_diff * (1 - increment) * ((1 - inc_mul) % size).Inv(size);

            offset %= size;

            return (increment, offset);
        }

        private static BigInteger TBI(this int num)
        {
            return new BigInteger(num);
        }

        private static BigInteger Mpow(this BigInteger bigInteger, BigInteger pow, BigInteger mod)
        {
            return BigInteger.ModPow(bigInteger, pow, mod);
        }

        private static List<int> DealNewDeck(this List<int> deck)
        {
            deck.Reverse();
            return deck;
        }

        private static List<int> CutN(this List<int> deck, int n)
        {
            if (n > 0)
            {
                var cut = deck.Take(n);

                deck = deck.Skip(n).ToList();
                deck.AddRange(cut);
            }
            else
            {
                var cut = deck.TakeLast(n.Abs());

                deck = deck.Take(deck.Count - n.Abs()).ToList();

                deck.InsertRange(0, cut);
            }

            return deck;
        }

        public static List<int> DealwithInc(this List<int> deck, int n)
        {
            var newdeck = new int[deck.Count];

            for (int i = 0; i < deck.Count; i++)
            {
                newdeck[(n * i) % deck.Count] = deck[i];
            }

            return newdeck.ToList();
        }
    }
}