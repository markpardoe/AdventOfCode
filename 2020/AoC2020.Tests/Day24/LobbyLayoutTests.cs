using AoC.AoC2020.Problems.Day24;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AoC.AoC2020.Tests.Day24
{
    public class LobbyLayoutTests : AocSolutionTest<int>
    {
        protected override SolutionData<int> Solution => new SolutionData<int>(new LobbyLayout(), 351, 3869);
    }
}