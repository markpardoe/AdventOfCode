using AoC.AoC2020.Problems.Day20;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AoC.AoC2020.Tests.Day20
{
    public class JurassicJigsawTests :AocSolutionTest<ulong>
    {
        protected override SolutionData<ulong> Solution => new SolutionData<ulong>(new JurassicJigsaw(), 23386616781851, 2376);
    }
}