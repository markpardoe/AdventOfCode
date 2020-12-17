using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AoC.Common;
using Xunit;
using Aoc.Aoc2018;
using Shouldly;

namespace Aoc.Aoc2018.Tests
{
    // Test that every solution returns the correct answers
    public class SolutionTests
    {
        [Theory]
        [ClassData(typeof(Solution_Test_Data))]
        public void Test_Day_Solve_Returns_CorrectAnswers_For_Problems(ISolution<int> sut, int firstResult, int? secondResult)
        {
            var data = LoadData(sut);
            var actualResults = sut.Solve(data).ToList();

            firstResult.ShouldBe(actualResults.First());

            if (secondResult.HasValue)
            {
                secondResult.ShouldBe(actualResults.Last());
            }
        }

        private IEnumerable<string> LoadData(ISolution<int> solution)
        {
            if (solution.InputFileName != null)
            {
                string inputFile = Path.Combine(System.Environment.CurrentDirectory, "InputData", solution.InputFileName);
                return File.ReadAllLines(inputFile);
            }
            else
            {
                return new List<string>();
            }
        }

        private class Solution_Test_Data : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new Aoc.Aoc2018.Day01.ChronalCalibration(), 400, 232 };
                yield return new object[] { new Aoc.Aoc2018.Day02.InventoryManagementSystem(), "4712", "lufjygedpvfbhftxiwnaorzmq" };
                yield return new object[] { new Aoc.Aoc2018.Day03.SuitSlicer(), 101565, 656 };
                yield return new object[] { new Aoc.Aoc2018.Day04.ReposeRecord(), 118599, 33949 };
                yield return new object[] { new Aoc.Aoc2018.Day05.AlchemicalReduction(), 9154, 4556 };
                yield return new object[] { new Aoc.Aoc2018.Day06.ChronalCoordinates(), 4186, 45509 };
                yield return new object[] { new Aoc.Aoc2018.Day07.SumOfItsParts(), "BGJCNLQUYIFMOEZTADKSPVXRHW", null };
                yield return new object[] { new Aoc.Aoc2018.Day08.MemoryManeuver(), 46781, 21405 };
                yield return new object[] { new Aoc.Aoc2018.Day09.MarbleMania(), 371284, 3038972494 };
                // Day 10 was a manual inspection of result
                yield return new object[] { new Aoc.Aoc2018.Day11.ChronalCharge(), "235, 20", "237,223,14" };
                yield return new object[] { new Aoc.Aoc2018.Day12.SubterraneanSustainability(), 2823, 2900000001856 };
                yield return new object[] { new Aoc.Aoc2018.Day13.MineCartMadness(), "74,87", "29,74" };
                yield return new object[] { new Aoc.Aoc2018.Day14.ChocolateCharts(), 2103141159, 20165733 };
                yield return new object[] { new Aoc.Aoc2018.Day15.BeverageBandits(), 197025, 44423 };

                yield return new object[] { new Aoc.Aoc2018.Day16.ChronalClassification(), 624, 584 };
                yield return new object[] { new Aoc.Aoc2018.Day17.ReservoirResearch(), 28246, 23107 };
                yield return new object[] { new Aoc.Aoc2018.Day18.SettlersOfNorthPole(), 483840, 219919 };
                yield return new object[] { new Aoc.Aoc2018.Day19.FlowControl(), 2040, 25165632 };
                // Day 20 missing

                yield return new object[] { new Aoc.Aoc2018.Day21.ChronalConversion(), 11592302, 313035 };
                yield return new object[] { new Aoc.Aoc2018.Day22.ModeMaze(), 7915, 980 };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    }
}
