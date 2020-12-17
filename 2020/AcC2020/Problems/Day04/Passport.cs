using System;
using System.Collections.Generic;
using System.IO;

namespace AoC.AoC2020.Problems.Day04
{
    public enum HeightUnit
    {
        inch = 0,
        cm = 1
    }

    public class Passport
    {
        public int? BirthYear  {get; }
        public int? IssueYear { get; }
        public int? ExpirationYear { get; }
        public int? Height { get; }

        public HeightUnit? HeightType { get; }
        public string HairColor { get; }
        public string EyeColor { get; }
        public string PassportId { get; }
        public string CountryId { get; }

        public Passport(IEnumerable<string> input)
        {
            foreach (string s in input)
            {
                var data = s.Split(':');

                switch (data[0])
                {
                    case "byr":
                        BirthYear = int.Parse(data[1].Trim());
                        break;
                    case "iyr":
                        IssueYear = int.Parse(data[1].Trim());
                        break;
                    case "eyr":
                        ExpirationYear = int.Parse(data[1].Trim());
                        break;
                    case "hgt":
                        var height = data[1].Trim();
                        var units = height.Substring(height.Length - 2);
                        height = height.Substring(0, height.Length - 2); // remove last 2 characters which are the units


                        if (units.Equals("cm"))
                        {
                            HeightType = HeightUnit.cm;
                        }
                        else if (units.Equals("in"))
                        {
                            HeightType = HeightUnit.inch;
                        }
                        else if (int.TryParse(units, out var val))
                        {
                            // if its numeric - then no units included.
                            HeightType = null;
                            height = data[1].Trim();  // no units - so use full height measurement
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException(nameof(input), $"Invalid height unit: {units}");
                        }

                        Height = int.Parse(height);
                        break;
                    case "hcl":
                        HairColor = data[1].Trim();
                        break;
                    case "ecl":
                        EyeColor = data[1].Trim();
                        break;
                    case "pid":
                        PassportId = data[1].Trim();
                        break;
                    case "cid":
                        CountryId = data[1].Trim();
                        break;
                    default:
                        throw new InvalidDataException($"{nameof(input)} Contains an invalid key: {data[0]}");
                }
            }
        }
    }
}