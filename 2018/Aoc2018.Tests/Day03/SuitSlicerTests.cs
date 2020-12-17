using System.Collections.Generic;
using Aoc.Aoc2018.Day03;
using AoC.Common.TestHelpers;
using Xunit;

namespace AoC.AoC2018.Tests.Day03
{
    public class SuitSlicerTests : AocSolutionTest<string>
    {
        protected override SolutionData<string> Solution => new SolutionData<string>(new SuitSlicer(), "101565", "#656");

        private readonly List<string> Example1 = new List<string>() { "#1 @ 1,3: 4x4", "#2 @ 3,1: 4x4", "#3 @ 5,5: 2x2" };
        private readonly int Example1_Result = 4;
        private readonly string Example1_NoOverlap_id = "#3";
        
        [Fact]
        public void Test_CountOverlappingClothes()
        {
            SuitSlicer sut = new SuitSlicer();
            int result = sut.CountOverlappingClothes(Example1);

            Assert.Equal(Example1_Result, result);            
        }

        [Fact]
        public void Test_FindNonOverlappingCloth()
        {
            SuitSlicer sut = new SuitSlicer();
            string result = sut.FindNonOverlappingCloth(Example1);

            Assert.Equal(Example1_NoOverlap_id, result);
        }
    }
}
