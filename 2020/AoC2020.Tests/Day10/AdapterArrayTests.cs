using System.Collections.Generic;
using System.Linq;
using AoC.AoC2020.Problems.Day10;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using Xunit;

namespace AoC.AoC2020.Tests.Day10
{
    public class AdapterArrayTests: AocSolutionTest<long>
    {
        [Theory]
        [MemberData(nameof(Part1Data))]
        public void CalculateGapDifferences_WithExamples_Calculates(IEnumerable<int> input, int expected1, int expected2)
        {
            var sut = new AdapterArray();
            var actual = sut.CalculateGapDifferences(input.ToList());

            actual.Item1.ShouldBe(expected1);
            actual.Item2.ShouldBe(expected2);
        }

        [Theory]
        [MemberData(nameof(Part2Data))]
        public void CountAdaptorPaths_WithExamples_Calculates(IEnumerable<int> input, long expected)
        {
            var sut = new AdapterArray();
            var actual = sut.CountAdaptorPaths(input.ToList());

            actual.ShouldBe(expected);
        }

        protected override SolutionData<long> Solution => new SolutionData<long>(new AdapterArray(), 2244, 3947645370368);

        private static readonly IReadOnlyList<int> Example1 = new List<int>
            {16, 10, 15, 5, 1, 11, 7, 19, 6, 12, 4};


        private static readonly IReadOnlyList<int> Example2 = new List<int>
        {
            28, 33, 18, 42, 31, 14, 46, 20, 48, 47, 24, 23, 49, 45, 19, 38, 39, 11, 1, 32, 25, 35, 8, 17, 7, 9, 4, 2,
            34, 10, 3
        };


        public static IEnumerable<object[]> Part1Data => 
            new List<object[]>
            {
                new object[] { Example1 , 7, 5 },
                new object[] { Example2 , 22, 10},
            };

        public static IEnumerable<object[]> Part2Data =>
            new List<object[]>
            {
                new object[] { Example1 , 8},
                new object[] { Example2 , 19208},
            };
    }
}
