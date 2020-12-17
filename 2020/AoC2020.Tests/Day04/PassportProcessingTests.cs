using AoC.AoC2020.Problems.Day04;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AoC.AoC2020.Tests.Day04
{
    public class PassportProcessingTests :AocSolutionTest<int>
    {

        [Fact]
        public void Solve_WithExampleData_ReturnsCorrectValues()
        {
            var sut = new PassportProcessing();

            var actualResults = sut.Solve(ExampleData).ToList();

            actualResults.First().ShouldBe(ExampleResult1);
        }

        protected override SolutionData<int> Solution => new SolutionData<int>(new PassportProcessing(), 256, 198);

        private readonly int ExampleResult1 = 2;
        private readonly IEnumerable<string> ExampleData = new List<string>()
        {
            "ecl:gry pid:860033327 eyr:2020 hcl:#fffffd",
            "byr:1937 iyr:2017 cid:147 hgt:183cm",
            "",
            "iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884",
            "hcl:#cfa07d byr:1929",
            "",
            "hcl:#ae17e1 iyr:2013",
            "eyr:2024",
            "ecl:brn pid:760753108 byr:1931",
            "hgt:179cm",
            "",
            "hcl:#cfa07d eyr:2025 pid:166559648",
           " iyr:2011 ecl:brn hgt:59in"
        };
    }
}
