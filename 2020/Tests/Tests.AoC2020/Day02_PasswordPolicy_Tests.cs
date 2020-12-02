using System;
using System.Collections.Generic;
using System.Text;
using AoC.AoC2020.Problems.Day02;
using Xunit;
using Shouldly;

namespace AoC.Tests.AoC2020
{
    public class Day02_PasswordPolicy_Tests
    {

        [Theory]
        [InlineData("1-3 a: abcde", 1, 3, 'a', "abcde")]
        [InlineData("1-3 b: cdefg", 1, 3, 'b', "cdefg")]
        [InlineData("2-9 c: ccccccccc", 2, 9, 'c', "ccccccccc")]
        public void Test_PasswordPolicy_ConstructsCorrectly(string input, int min, int max, char letter, string password)
        {
            PasswordPolicy sut = new PasswordPolicy(input);

            sut.MinLimit.ShouldBe(min);
            sut.MaxLimit.ShouldBe(max);
            sut.Password.ShouldBe(password);
            sut.Letter.ShouldBe(letter);

        }

        [Theory]
        [InlineData("1-3 a: abcde", true)]
        [InlineData("1-3 b: cdefg", false)]
        [InlineData("2-9 c: ccccccccc", true)]
        public void Test_Day02_IsValidPassword(string input, bool expectedValue)
        {
            var sut = new PasswordPolicy(input);

            sut.IsValid().ShouldBe(expectedValue);
        }

        [Theory]
        [InlineData("1-3 a: abcde", true)]
        [InlineData("1-3 b: cdefg", false)]
        [InlineData("2-9 c: ccccccccc", false)]
        public void Test_Day02_IsValidPasswordWithPositions(string input, bool expectedValue)
        {
            var sut = new PasswordPolicy(input);

            sut.IsValidWithPositions().ShouldBe(expectedValue);
        }

    }
}
