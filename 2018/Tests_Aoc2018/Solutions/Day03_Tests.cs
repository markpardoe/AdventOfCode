using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using AoC2018.Day03;

namespace Tests_Aoc2018.Solutions
{
    public class Day03_Tests
    {

        private readonly List<string> Example1 = new List<string>() { "#1 @ 1,3: 4x4", "#2 @ 3,1: 4x4", "#3 @ 5,5: 2x2" };
        private readonly int Example1_Result = 4;
        private readonly string Example1_NoOverlap_id = "#3";
        
        [Fact]
        public void Test_CountOverlappingClothes()
        {
            Day03_Solution sut = new Day03_Solution();
            int result = sut.CountOverlappingClothes(Example1);

            Assert.Equal(Example1_Result, result);            
        }

        [Fact]
        public void Test_FindNonOverlappingCloth()
        {
            Day03_Solution sut = new Day03_Solution();
            string result = sut.FindNonOverlappingCloth(Example1);

            Assert.Equal(Example1_NoOverlap_id, result);
        }
    }
}
