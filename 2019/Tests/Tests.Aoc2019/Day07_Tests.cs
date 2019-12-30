using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using AoC.Common;
using Aoc.AoC2019.Problems.Day07;
using Aoc.AoC2019.IntCode;

namespace Tests
{
   
    public class Day07_Tests
    {
        [Theory]
        [InlineData(43210, 4, 3, 2, 1, 0, 3, 15, 3, 16, 1002, 16, 10, 16, 1, 16, 15, 15, 4, 15, 99, 0, 0)]
        [InlineData(54321, 0, 1, 2, 3, 4, 3, 23, 3, 24, 1002, 24, 10, 24, 1002, 23, -1, 23, 101, 5, 23, 23, 1, 24, 23, 23, 4, 23, 99, 0, 0)]
        [InlineData(65210, 1, 0, 4, 3, 2, 3, 31, 3, 32, 1002, 32, 10, 32, 1001, 31, -2, 31, 1007, 31, 0, 33, 1002, 33, 7, 33, 1, 33, 31, 31, 1, 32, 31, 31, 4, 31, 99, 0, 0, 0)]
        public void Test_InCodeParse_RunAmplifiedCode(int expectedResult, int s1, int s2, int s3, int s4, int s5, params long[] code)
        {
            var settings = new List<long>() { s1, s2, s3, s4, s5 };
            AmplificationController controller = new AmplificationController(settings, code);

            long output = controller.RunAmplifiedCircuits(0);
            Assert.Equal(expectedResult, output);
        }

        [Theory]
        [InlineData(139629729, 9,8,7,6,5, 3, 26, 1001, 26, -4, 26, 3, 27, 1002, 27, 2, 27, 1, 27, 26, 27, 4, 27, 1001, 28, -1, 28, 1005, 28, 6, 99, 0, 0, 5)]
        [InlineData(18216, 9, 7, 8, 5, 6, 3, 52, 1001, 52, -5, 52, 3, 53, 1, 52, 56, 54, 1007, 54, 5, 55, 1005, 55, 26, 1001, 54, -5, 54, 1105, 1, 12, 1, 53, 54, 53, 1008, 54, 0, 55, 1001, 55, 1, 55, 2, 53, 55, 53, 4, 53, 1001, 56, -1, 56, 1005, 56, 6, 99, 0, 0, 0, 0, 10)]
        public void Test_FeedbackAmplificationController(int expectedResult, int s1, int s2, int s3, int s4, int s5, params long[] code)
        {
            var settings = new List<long>() { s1, s2, s3, s4, s5 };
            FeedbackAmplificationController controller = new FeedbackAmplificationController(settings, code);

            long output = controller.RunAmplifiedCircuits(0);
            Assert.Equal(expectedResult, output);
        }
    }
}
