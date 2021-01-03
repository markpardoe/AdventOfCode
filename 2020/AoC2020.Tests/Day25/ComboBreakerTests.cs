using AoC.AoC2020.Problems.Day25;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AoC.AoC2020.Tests.Day25
{
    public class ComboBreakerTests : AocSolutionTest<long>
    {
        protected override SolutionData<long> Solution => new SolutionData<long>(new ComboBreaker(), 7936032, 7936032);
    }
}