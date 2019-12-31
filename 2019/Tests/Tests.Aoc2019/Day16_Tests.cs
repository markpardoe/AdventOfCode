using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using AoC.Common;
using Aoc.AoC2019.Problems.Day16;

namespace Aoc.AoC2019.Tests
{
    public class Day16_Tests
    {
        [Theory]
        [InlineData("01029498", 4, 8, "12345678")]
        [InlineData("24176176", 100, 8, "80871224585914546619083218645595")]
        [InlineData("73745418", 100, 8, "19617804207202209144916044189917")]
        [InlineData("52432133", 100, 8, "69317163492948606335995924319873")]
        public void Test_FlawedFrequencyTransmission_VerifySignal(string result, int numPhases, int numDigits, string input)
        {
            List<int> expectedResultData = result.Select(c => Int32.Parse(c.ToString())).ToList();
            FlawedFrequencyTransmission fft = new FlawedFrequencyTransmission(input);
            List<int> actualResult = fft.VerifySignal(numPhases).GetRange(0, numDigits);
            Assert.Equal(expectedResultData, actualResult);
        }

        [Theory]
        [InlineData("84462026",  "03036732577212944063491565474664", 7 )]
        [InlineData("78725270",  "02935109699940807407585447034323", 7)]
        [InlineData("53553731",  "03081770884921959731165446850517", 7)]
        public void Test_FlawedFrequencyTransmission_DecodeSignal(string expectedResultData, string data, int offSetLength)
        {
            FlawedFrequencyTransmission fft = new FlawedFrequencyTransmission(data);
            string actualResult = fft.DecodeSignal( 10000, 100, offSetLength);
            Assert.Equal(expectedResultData, actualResult);
        }
    }
}