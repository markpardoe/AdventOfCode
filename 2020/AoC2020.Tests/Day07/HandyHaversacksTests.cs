using AoC.AoC2020.Problems.Day07;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AoC.AoC2020.Tests.Day07
{
    public class HandyHaversacksTests
    {
        [Fact]
        public void Solve_WithExample1_ReturnsCorrectPart1()
        {
            var sut = new HandyHaversacks();

            sut.Solve(Example1Data()).First().ShouldBe(4);
        }


        [Fact]
        public void Solve_WithExample2_ReturnsCorrectPart2()
        {
            var sut = new HandyHaversacks();

            sut.Solve(Example2Data()).Last().ShouldBe(126);
        }

        [Fact]
        public void Solve_WithExample1_ReturnsCorrectPart2()
        {
            var sut = new HandyHaversacks();

            sut.Solve(Example1Data()).Last().ShouldBe(32);
        }

        [Theory]
        [MemberData(nameof(Solution))]
        public void Solve_WithInput_ReturnsCorrectValues(ISolution<int> sut, int result1, int result2)
        {
            var data = InputData.LoadSolutionInput(sut);
            var actualResults = sut.Solve(data).ToList();

            actualResults.First().ShouldBe(result1);
            actualResults.Last().ShouldBe(result2);
        }

        public static SolutionData<int> Solution => new SolutionData<int>(new HandyHaversacks(), 235, 158493);


        public static IEnumerable<string> Example1Data()
        {
            yield return "light red bags contain 1 bright white bag, 2 muted yellow bags.";
            yield return "dark orange bags contain 3 bright white bags, 4 muted yellow bags.";
            yield return "bright white bags contain 1 shiny gold bag.";
            yield return "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.";
            yield return "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.";
            yield return "dark olive bags contain 3 faded blue bags, 4 dotted black bags.";
            yield return "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.";
            yield return "faded blue bags contain no other bags.";
            yield return "dotted black bags contain no other bags.";
        }

        public static IEnumerable<string> Example2Data()
        {
            yield return "shiny gold bags contain 2 dark red bags.";
            yield return "dark red bags contain 2 dark orange bags.";
            yield return "dark orange bags contain 2 dark yellow bags.";
            yield return "dark yellow bags contain 2 dark green bags.";
            yield return "dark green bags contain 2 dark blue bags.";
            yield return "dark blue bags contain 2 dark violet bags.";
            yield return "dark violet bags contain no other bags.";
        }
    }

}
