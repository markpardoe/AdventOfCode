﻿using Aoc.AoC2019.Problems.Day04;
using AoC.Common.TestHelpers;
using Xunit;

namespace AoC.AoC2019.Tests.Day04
{
    public class Day04Tests : AocSolutionTest<int>
    {
        private readonly Day04_Solution sut = new Day04_Solution();

        [Theory]
        [InlineData(111111, true)]
        [InlineData(223450, false)]
        [InlineData(123789, false)]
        public void Passwords_Are_Valid(int password, bool expectedValue)
        {
            bool result = sut.IsValidPassword(password);
            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData(111111, false)]
        [InlineData(112233, true)]
        [InlineData(123444, false)]
        [InlineData(111122, true)]
        [InlineData(677778, false)]
        [InlineData(667777, true)]
        public void Passwords_With_No_Larger_Groups_Are_Valid(int password, bool expectedValue)
        {
            bool result = sut.IsValidPasswordNoRepeatingGroups(password);
            Assert.Equal(expectedValue, result);
        }

        protected override SolutionData<int> Solution => new SolutionData<int>(new Day04_Solution(), 895, 591);
    }
}
