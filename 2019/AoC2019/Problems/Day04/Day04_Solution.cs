using AoC.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day04
{
    public class Day04_Solution : AoCSolution<int>
    {
        public override int Year => 2019;

        public override int Day => 4;

        public override string Name => "Day 4: Secure Container";

        public override string InputFileName => null;

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            yield return CalculatePasswordCombinations(284639, 748759);
            yield return CalculatePasswordCombinationsWithNoRepeatingGroups(284639, 748759);
        }

        private readonly int _maxPasswordLength = 6;

        private int CalculatePasswordCombinations(int start, int end)
        {
           int total = 0;
           for (int i = start; i<= end;i++)
           {
                if (IsValidPassword(i))
                {
                    total++;
                }
           }
           return total;
        }

        private int CalculatePasswordCombinationsWithNoRepeatingGroups(int start, int end)
        {
            int total = 0;
            for (int i = start; i <= end; i++)
            {
                if (IsValidPasswordNoRepeatingGroups(i))
                {
                    total++;
                }
            }
            return total;
        }

        public bool IsValidPassword(int value)
        {
            int[] password = value.ToString().Select(x => Int32.Parse(x.ToString())).ToArray();

            if (password.Length != 6) return false;
            bool doubleFound = false;

            // check from 1st to 2nd from last digit.
            // ignore final digit as we're only checking the digit against the digit to the right
            for (int i =0; i< password.Length-1;i++)
            {
                // check for duplicates & incrementing values 
                if (password[i] == password[i + 1])
                {
                    doubleFound = true;
                }
                else if (password[i] > password[i + 1])
                {
                    return false;
                }
            }

            return doubleFound;            
        }

        public bool IsValidPasswordNoRepeatingGroups(int value)
        {
            int[] password = value.ToString().Select(x => Int32.Parse(x.ToString())).ToArray();

            if (password.Length != _maxPasswordLength) return false;
            bool doubleFound = false;
            int groupDigit = -1;

            // check from 1st to 2nd from last digit.
            // ignore final digit as we're only checking the digit against the digit to the right
            for (int i = 0; i < _maxPasswordLength - 1; i++)
            {
                // start a new group check
                if (password[i] != groupDigit)
                {           
                    groupDigit = password[i];
                    if (password[i] == password[i + 1])
                    {
                        if ((i == _maxPasswordLength - 2) || password[i] != password[i + 2])  // also check 3rd in group to make sure its not the same
                        {
                            doubleFound = true;
                        }
                    }
                }
                
                if (password[i] > password[i + 1])
                {
                    return false;
                }
            }

            return doubleFound;
        }
    }
}
