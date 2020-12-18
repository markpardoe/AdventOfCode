﻿using System.Collections.Generic;
using Aoc.Aoc2018.Day07;
using AoC.Common.TestHelpers;
using Shouldly;
using Xunit;

namespace AoC.AoC2018.Tests.Day07 
{
    public class SumOfItsPartsTests : AocSolutionTest<string>
    {
        protected override SolutionData<string> Solution => new SolutionData<string>(new SumOfItsParts(), "BGJCNLQUYIFMOEZTADKSPVXRHW", "N/A");

        private readonly List<string> ExampleInput = new List<string>()
        {
            "Step C must be finished before step A can begin.",
            "Step C must be finished before step F can begin.",
            "Step A must be finished before step B can begin.",
            "Step A must be finished before step D can begin.",
            "Step B must be finished before step E can begin.",
            "Step D must be finished before step E can begin.",
            "Step F must be finished before step E can begin."
        };

        private readonly string ExampleResult = "CABDFE";

        [Fact]
        public void Test_FindShortestPath()
        {
            var sut = new SumOfItsParts();
            string result =  sut.FindShortestPath(ExampleInput);

            result.ShouldBe(ExampleResult);
            Assert.Equal(ExampleResult, result);
        }
    }
}