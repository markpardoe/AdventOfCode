using AoC.AoC2020.Problems.Day15;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using System.Linq;
using Xunit;

namespace AoC.AoC2020.Tests.Day15
{
    public class RambunctiousRecitationTests : AocSolutionTest<int>
    { 
        protected override SolutionData<int> Solution => new SolutionData<int>(new RambunctiousRecitation(), 257, 8546398);
    }
}