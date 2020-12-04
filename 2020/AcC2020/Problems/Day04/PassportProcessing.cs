using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;

namespace AoC.AoC2020.Problems.Day04
{
    public class PassportProcessing : AoCSolution<int>
    {
        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            var Passports = ProcessInputs(input).ToList();

            var simpleValidator = new SimplePassportValidator();
            var validator = new PassportValidator();

            yield return Passports.Count(p => simpleValidator.Validate(p).IsValid);
            yield return Passports.Count(p => validator.Validate(p).IsValid);
        }

        public override int Year => 2020;
        public override int Day => 4;
        public override string Name => "Day 4: Passport Processing";
        public override string InputFileName => "Day04.txt";


        private IEnumerable<Passport> ProcessInputs(IEnumerable<string> input)
        {
            var passports = new List<Passport>();
            var passportData = new List<string>();

            foreach (string line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (passportData.Count > 0)
                    {
                        passports.Add(new Passport(passportData));
                    }

                    // Empty line - so start a new passport
                    passportData = new List<string>();
                }
                else
                {
                    passportData.AddRange(line.Split(' ', StringSplitOptions.RemoveEmptyEntries));
                }
            }

            // Add final passport - just in case there's no trailing empty line
            if (passportData.Count > 0)
            {
                passports.Add(new Passport(passportData));
            }

            return passports;
        }
    }
}
