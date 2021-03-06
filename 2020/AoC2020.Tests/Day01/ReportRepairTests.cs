﻿using System.Collections.Generic;
using System.Linq;
using AoC.AoC2020.Problems.Day01;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using Xunit;

namespace AoC.AoC2020.Tests.Day01
{
    public class ReportRepairTests : AocSolutionTest<int>
    {

        [Fact]
        public void RepairReport_WithExample_ReturnsCorrectValue()
        {
            var sut = new ReportRepair();
            sut.RepairReport(ExampleTarget, ExampleData).ShouldBe(514579);
        }

        [Fact]
        public void RepairReportThreeNumbers_WithExample_ReturnsCorrectValue()
        {
            var sut = new ReportRepair();
            sut.RepairReportThreeNumbers(ExampleTarget, ExampleData).ShouldBe(241861950);
        }
        
        protected override SolutionData<int> Solution => new SolutionData<int>(new ReportRepair(), 703131, 272423970);
        
        private readonly int ExampleTarget = 2020;
        private readonly List<string> ExampleData = new List<string>()
        {
            "1721",
            "979",
            "366",
            "299",
            "675",
            "1456"
        };
    }
}
