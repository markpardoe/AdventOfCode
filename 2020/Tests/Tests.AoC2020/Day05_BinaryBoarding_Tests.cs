using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AoC.AoC2020.Problems.Day05;
using Xunit;
using Shouldly;

namespace AoC.Tests.AoC2020
{
    
    public class Day05_BinaryBoarding_Tests
    {

        [Theory]
        [InlineData("FBFBBFFRLR", 357)]
        [InlineData("BFFFBBFRRR", 567)]
        [InlineData("FFFBBBFRRR", 119)]
        [InlineData("BBFFBBFRLL", 820)]
        public void Test_GetSeatId_ReturnsCorrectId(string input, int expectedValue)
        {
            var sut = new BinaryBoarding();
            var actual = sut.GetSeatId(input);

            actual.ShouldBe(expectedValue);
        }
    }
}
