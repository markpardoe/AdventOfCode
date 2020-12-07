using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Shouldly;
using AoC.AoC2020.Problems.Day07;

namespace AoC.Tests.AoC2020
{
    public class Day07_HandyHaversacks_Tests
    {
        [Fact]
        public void Test_SearchForBag()
        {
            var input = GetData();
            var sut = new HandyHaversacks();

            sut.Solve(GetData()).First().ShouldBe(4);
        }



        public static IEnumerable<string> GetData()
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
    }

}
