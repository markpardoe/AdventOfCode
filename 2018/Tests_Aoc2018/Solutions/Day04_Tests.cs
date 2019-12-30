﻿using System;
using System.Collections.Generic;
using System.Text;
using Aoc.Aoc2018.Day04;
using Xunit;

namespace Tests_Aoc2018.Solutions
{
    public class Day04_Tests
    {

        private readonly List<string> Example1 = new List<string>()
        {
            "[1518 - 11 - 01 00:00] Guard #10 begins shift",
            "[1518 - 11 - 01 00:05] falls asleep",
            "[1518 - 11 - 03 00:24] falls asleep",
            "[1518 - 11 - 01 00:25] wakes up",
            "[1518 - 11 - 01 00:30] falls asleep",
            "[1518 - 11 - 05 00:03] Guard #99 begins shift",
            "[1518 - 11 - 05 00:55] wakes up",
            "[1518 - 11 - 01 00:55] wakes up",
            "[1518 - 11 - 01 23:58] Guard #99 begins shift",
            "[1518 - 11 - 02 00:40] falls asleep",
            "[1518 - 11 - 02 00:50] wakes up",
            "[1518 - 11 - 03 00:05] Guard #10 begins shift",            
            "[1518 - 11 - 03 00:29] wakes up",
            "[1518 - 11 - 04 00:02] Guard #99 begins shift",
            "[1518 - 11 - 04 00:36] falls asleep",
            "[1518 - 11 - 04 00:46] wakes up",            
            "[1518 - 11 - 05 00:45] falls asleep"            
        };

        private readonly int Example1_Strategy1_Result = 240;
        private readonly int Example1_Strategy2_Result = 4455;

        [Fact]
        public void Test_Strategy1()
        {
            Day04_Solution sut = new Day04_Solution();
            int actualResult = sut.Strategy1(Example1);
            Assert.Equal(Example1_Strategy1_Result, actualResult);
        }

        [Fact]
        public void Test_Strategy2()
        {
            Day04_Solution sut = new Day04_Solution();
            int actualResult = sut.Strategy2(Example1);
            Assert.Equal(Example1_Strategy2_Result, actualResult);
        }
    }
}
