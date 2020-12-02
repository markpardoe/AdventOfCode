using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;

namespace AoC.AoC2020.Problems.Day02
{
    public class PasswordPolicy
    {
        public int MinLimit { get; } = 0;
        public int MaxLimit { get; } = 0;
        public char Letter { get; }
        public string Password { get; }

        public PasswordPolicy(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(nameof(input));
            }

            var p = input.Split(' ', '-');
            if (p.Length != 4)
            {
                throw new ArgumentOutOfRangeException(nameof(input));
            }

            // First 2 values will be Min & Max occurrences of the character
            MinLimit = int.Parse(p[0]);
            MaxLimit = int.Parse(p[1]);

            // 3rd value is the letter - with colon.  ie.  "a:".  So we only need the first character
            Letter = p[2].First();

            // last value is the password
            Password = p[3].ToLower().Trim();
        }

        /// <summary>
        /// Checks if the password is valid by looking for the number of times the letter occurs in the password.
        /// This must be between MinLimit and MaxLimit
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            int occurancess = Password.ToCharArray().Count(a => a == Letter);

            return occurancess >= MinLimit && occurancess <= MaxLimit;
        }

        /// <summary>
        /// Checks if the password is valid by looking for the letter to occur in exactly one of the positions.
        /// ie.  MinLmit = 2, MaxLimit = 4, Letter = 'A'.  'A' must be the 2nd or 4th letter (not zero based!) to be valid password.
        /// </summary>
        /// <returns></returns>
        public bool IsValidWithPositions()
        {
            bool firstPosition = false;
            bool secondPosition = false;

            if (MinLimit <= Password.Length)
            {
                firstPosition = Password[MinLimit - 1].Equals(Letter);
            }

            if (MaxLimit <= Password.Length)
            {
                secondPosition = Password[MaxLimit - 1].Equals(Letter);
            }

            // We only want 1 occurance - so if either both TRUE or both FALSE then its invalid
            if (firstPosition == secondPosition)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
