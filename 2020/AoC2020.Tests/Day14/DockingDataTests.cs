using AoC.AoC2020.Problems.Day14;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using System.Linq;
using Xunit;

namespace AoC.AoC2020.Tests.Day14
{
    public class DockingDataTests : AocSolutionTest<long>
    {
        protected override SolutionData<long> Solution => new SolutionData<long>(new DockingData(), 6317049172545, 3434009980379);
    }
}