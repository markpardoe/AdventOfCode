//using AoC.AoC2020.Problems.Day01;
//using AoC.Common;
//using AoC.Common.TestHelpers;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using Xunit;

//namespace AoC.Tests.AoC2020
//{
//    // Test that every solution returns the correct answers
//    public class SolutionTests
//    {
//        [Theory]
//        [ClassData(typeof(Solution_Test_Data))]
//        public void Test_Day_Solve_Returns_CorrectAnswers_For_Problems(ISolution<int> sut, int firstResult, int? secondResult)
//        {
//            var data = LoadData(sut);
//            var actualResults = sut.Solve(data).ToList();

//            Assert.Equal(firstResult, actualResults.First());

//            if (secondResult.HasValue)
//            {
//                Assert.Equal(secondResult.Value, actualResults.Last());
//            }
//        }

//        private IEnumerable<string> LoadData(ISolution<int> solution)
//        {
//            if (solution.InputFileName != null)
//            {
//                string inputFile = Path.Combine(System.Environment.CurrentDirectory, "InputData", solution.InputFileName);
//                return File.ReadAllLines(inputFile);
//            }
//            else
//            {
//                return new List<string>();
//            }
//        }

//        public class Solution_Test_Data : IEnumerable<object[]>
//        {
//            public IEnumerator<object[]> GetEnumerator()
//            { 
//                yield return new object[] { new AoC.AoC2020.Problems.Day01.ReportRepair(), 703131, 272423970 };
//                yield return new object[] { new AoC.AoC2020.Problems.Day02.PasswordPhilosophy(), 638, 699 };
//                yield return new object[] { new AoC.AoC2020.Problems.Day03.TobogganTrajectory(), 214, 8336352024 };
//                yield return new object[] { new AoC.AoC2020.Problems.Day04.PassportProcessing(), 256, 198 };
//                yield return new object[] { new AoC.AoC2020.Problems.Day05.BinaryBoarding(), 885, 623 };
//                yield return new object[] { new AoC.AoC2020.Problems.Day06.CustomCustoms(), 6534, 3402 };
//                yield return new object[] { new AoC.AoC2020.Problems.Day07.HandyHaversacks(), 235, 158493 };
//                yield return new object[] { new AoC.AoC2020.Problems.Day08.HandheldHalting(), 1087, 780 };

//                yield return new object[] { new AoC.AoC2020.Problems.Day09.EncodingError(), 14360655, 1962331 };
//                yield return new object[] { new AoC.AoC2020.Problems.Day10.AdapterArray(), 2244, 3947645370368 };
//                yield return new object[] { new AoC.AoC2020.Problems.Day11.SeatingSystem(), 2468, 2214 };
//                yield return new object[] { new AoC.AoC2020.Problems.Day12.RainRisk(), 445, 42495 };
//                yield return new object[] { new AoC.AoC2020.Problems.Day13.ShuttleSearch(), 222, 408270049879073 };
//                yield return new object[] { new AoC.AoC2020.Problems.Day14.DockingController(), 6317049172545, 3434009980379 };
//                yield return new object[] { new AoC.AoC2020.Problems.Day15.RambunctiousRecitation(), 257, 8546398 };

//                yield return new object[] { new AoC.AoC2020.Problems.Day16.TicketTranslation(), 22977, 998358379943 };
//                yield return new object[] { new AoC.AoC2020.Problems.Day17.ConwayCubes(), 310, 2056 };
//            }

//            [Theory]
//            [MemberData(nameof(Solution))]
//            public void Solve_WithInput_ReturnsCorrectValues(ISolution<int> sut, int result1, int? result2)
//            {
//                var data = InputData.LoadSolutionInput(sut);
//                var actualResults = sut.Solve(data).ToList();

//                Assert.Equal(result1, actualResults.First());

//                if (result2.HasValue)
//                {
//                    Assert.Equal(result2.Value, actualResults.Last());
//                }
//            }

//            public static SolutionData<int> Solution => new SolutionData<int>(new ReportRepair(), 703131, 272423970);

//            IEnumerator IEnumerable.GetEnumerator()
//            {
//                return this.GetEnumerator();
//            }
//        }
//    }
//}
