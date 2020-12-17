using System.Linq;
using AoC.AoC2020.Problems.Day09;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using Xunit;

namespace AoC.AoC2020.Tests.Day09
{
    public class EncodingErrorTests : AocSolutionTest<long>
    {
        protected override SolutionData<long> Solution => new SolutionData<long>(new EncodingError(), 14360655, 1962331);
    }
}
