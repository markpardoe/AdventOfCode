using AoC.AoC2020.Problems.Day23;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AoC.AoC2020.Tests.Day23
{
    public class CrabCupsTest : AocSolutionTest<long>
    {
        protected override SolutionData<long> Solution => new SolutionData<long>(new CrabCups(), 34952786, 505334281774);
    }
}