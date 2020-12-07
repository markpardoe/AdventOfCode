using AoC.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Aoc.Aoc2018.Day01;

namespace Tests_Aoc2018.Solutions
{
    public class Day01_Tests
    {
        [Theory]
        [InlineData(3, "+1", "+1", "+1")]
        [InlineData(0, "+1", "+1", "-2")]
        [InlineData(-6, "-1", "-2", "-3")]
        public void Test_FrequencyModification(int expectedResult, params string[] inputData)
        {
            ChronalCalibration s = new ChronalCalibration();
            int result = s.CalculateFrequency(inputData);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(0, "+1", "-1")]
        [InlineData(10, "+3", "+3", "+4", "-2", "-4")]
        [InlineData(5, "-6", "+3", "+8", "+5", "-6")]
        [InlineData(14, "+7", "+7", "-2", "-7", "-4")]
        public void Test_DuplicateFrequency(int expectedResult, params string[] inputData)
        {
            ChronalCalibration s = new ChronalCalibration();
            int result = s.FindDuplicateFrequency(inputData);
            Assert.Equal(expectedResult, result);
        }
    }
}
