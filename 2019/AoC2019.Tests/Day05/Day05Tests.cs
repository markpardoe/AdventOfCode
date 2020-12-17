using System;
using System.Collections.Generic;
using System.Text;
using Aoc.AoC2019.Problems.Day05;
using AoC.Common.TestHelpers;

namespace AoC.AoC2019.Tests.Day05
{
    public class Day05Tests : AocSolutionTest<long>
    {
        protected override SolutionData<long> Solution => new SolutionData<long>(new Day05_Solution(), 12234644, 3508186);
    }
}
