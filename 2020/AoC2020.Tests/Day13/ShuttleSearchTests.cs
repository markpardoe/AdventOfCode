using AoC.AoC2020.Problems.Day13;
using AoC.Common;
using AoC.Common.TestHelpers;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AoC.AoC2020.Tests.Day13
{

    public class ShuttleSearchTests
    {
        [Fact]
        public void FindNextBus_WithExampleData_ReturnsCorrectValues()
        {
            var sut = new ShuttleSearch();;

            long departureTime = long.Parse(ExampleData[0]);
            var buses = ParseBusTimes(ExampleData[1]);

            var actual = sut.FindNextBus(departureTime, buses);
            actual.ShouldBe(ExampleResult);
        }

        [Theory]
        [MemberData(nameof(ExamplesPart2))]
        public void CalculatePart2_WithExampleData_ReturnsCorrectValues(string input, long expectedResult)
        {
            var sut = new ShuttleSearch();
            var buses = ParseBusTimes(input);

            var actualResult = sut.CalculatePart2(buses);

            actualResult.ShouldBe(expectedResult);
        }


        [Theory]
        [MemberData(nameof(Solution))]
        public void Solve_WithInput_ReturnsCorrectValues(ISolution<int> sut, int result1, int result2)
        {
            var data = InputData.LoadSolutionInput(sut);
            var actualResults = sut.Solve(data).ToList();

            actualResults.First().ShouldBe(result1);
            actualResults.Last().ShouldBe(result2);
        }

        public static SolutionData<long> Solution => new SolutionData<long>(new ShuttleSearch(), 222, 408270049879073);

        private IEnumerable<BusTime> ParseBusTimes(string input)
        {
            var splitInput = input.Split(',');
            var busTimes = new List<BusTime>();

            for (int i = 0; i < splitInput.Length; i++)
            {
                if (splitInput[i] != "x")
                {
                    busTimes.Add(new BusTime(int.Parse(splitInput[i]), i));
                }
            }

            return busTimes;
        }
 
        public static IEnumerable<object[]> ExamplesPart2 =>
            new List<object[]>
            {
                new object[] { "7,13,x,x,59,x,31,19" , 1068781},
                new object[] { "17,x,13,19", 3417 },
                new object[] { "67,7,59,61", 754018 },
                new object[] { "67,x,7,59,61", 779210 },
                new object[] { "67,7,x,59,61", 1261476 },
                new object[] { "1789,37,47,1889", 1202161486 }

            };

        private readonly long ExampleResult = 295;
        private readonly List<string> ExampleData = new List<string>()
        {
            "939",
            "7,13,x,x,59,x,31,19"
        };

    }
}
