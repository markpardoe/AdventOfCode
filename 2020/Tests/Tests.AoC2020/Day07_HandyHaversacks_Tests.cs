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
            var sut = new HandyHaversacks();

            sut.Solve(Example1()).First().ShouldBe(4);
        }


        [Fact]
        public void Test_CountBagsRequired_Example2()
        {
            var sut = new HandyHaversacks();

            sut.Solve(Example2()).Last().ShouldBe(126);
        }

        [Fact]
        public void Test_CountBagsRequired_Example1()
        {
            var sut = new HandyHaversacks();

            sut.Solve(Example1()).Last().ShouldBe(32);
        }



        public static IEnumerable<string> Example1()
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

        public static IEnumerable<string> Example2()
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
