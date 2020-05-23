using System;
using System.Collections.Generic;
using System.Text;
using Aoc.Aoc2018.Day07;
using Xunit;

namespace Tests_Aoc2018.Solutions
{
    public class Day07_Tests
    {

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

        private readonly string Example_Result = "CABDFE";

        [Fact]
        public void Test_FindShortestPath()
        {
            var sut = new Day07_Solution();
            string result =  sut.FindShortestPath(ExampleInput);
            Assert.Equal(Example_Result, result);
        }
    }
}
