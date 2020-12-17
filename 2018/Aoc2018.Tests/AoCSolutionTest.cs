using AoC.Common.TestHelpers;
using Shouldly;
using System.Linq;
using Xunit;

namespace AoC.AoC2018.Tests
{
    public abstract class AocSolutionTest<T>
    {
        [Fact]
        public void Solve_WithInput_ReturnsCorrectValues()
        {
            var sut = Solution.SolutionUnderTest;
            var data = InputData.LoadSolutionInput(sut);
            var actualResults = sut.Solve(data).ToList();

            actualResults.First().ShouldBe(Solution.Result1);
            actualResults.Last().ShouldBe(Solution.Result2);
        }

        protected abstract SolutionData<T> Solution { get; }
    }
}
