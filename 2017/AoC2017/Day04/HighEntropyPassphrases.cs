using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC.Common;

namespace AoC2017.Day04
{
    public class HighEntropyPassphrases : AoCSolution<int>
    {
        public override int Year => 2017;
        public override int Day => 4;
        public override string Name => "Day 4: High-Entropy Passphrases";
        public override string InputFileName => "Day04.txt";

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            var data = ParseInput(input).ToList();
            yield return CountValidPassphrases(data, IsPassphraseValid);

            yield return CountValidPassphrases(data, IsPassphraseWithAnagramsValid);

        }

        private int CountValidPassphrases(List<List<string>> passPhrases, Func<List<String>, bool> validator)
        {
            return passPhrases.Count(x => validator(x));
        }

        private IEnumerable<List<string>> ParseInput(IEnumerable<string> rawData)
        {
            foreach (var line in rawData)
            {
                yield return line.Split(" ").ToList();
            }
        }

        private bool IsPassphraseValid(List<string> passPhrase)
        {
            // We could use a dictionary to count each word - but quicker to remove duplicates
            // (by converting to hashSet) and comparing lengths.
            // If different - we've remove a duplicate so its not valid
            var hashed = passPhrase.ToHashSet();
            return hashed.Count == passPhrase.Count;
        }

        private bool IsPassphraseWithAnagramsValid(List<string> passPhrase)
        {
            var copy = passPhrase.ToList();

            // Easiest way to check for anagrams is to convert each word to a char array - then sort it - and convert back to a string.
            // That way anagrams all have same pattern
            var sorted = passPhrase.Select(word => string.Join("", word.ToCharArray().OrderBy(x => x))).ToList();
            return IsPassphraseValid(sorted);
        }
    }
}
