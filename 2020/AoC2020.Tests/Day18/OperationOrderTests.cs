using AoC.AoC2020.Problems.Day18;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AoC.AoC2020.Tests.Day18
{
    public class OperationOrderTests :AocSolutionTest<long>
    {
        protected override SolutionData<long> Solution => new SolutionData<long>(new OperationOrder(), 650217205854, 20394514442037);
    }
}