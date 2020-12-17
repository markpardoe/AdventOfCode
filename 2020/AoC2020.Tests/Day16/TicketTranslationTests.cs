using AoC.AoC2020.Problems.Day16;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using System.Linq;
using Xunit;

namespace AoC.AoC2020.Tests.Day16
{
    public class TicketTranslationTests : AocSolutionTest<long>
    {
        protected override SolutionData<long> Solution => new SolutionData<long>(new TicketTranslation(), 22977, 998358379943);
    }
}