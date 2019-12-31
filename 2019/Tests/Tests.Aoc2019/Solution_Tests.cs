using Aoc.AoC2019.Problems;
using AoC.Common;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Aoc.AoC2019.Tests
{
    public class Solution_Tests
    {

        [Theory]
        [ClassData(typeof(Solution_Test_Data))]
        public void Test_Day_Solve_Returns_CorrectAnswers_For_Problems(ISolution sut, string firstResult, string secondResult)
        {
            var data = LoadData(sut);
            List<string> actualResults = sut.Solve(data).ToList();

            Assert.Equal(firstResult, actualResults.First());

            if (secondResult != null)
            {
                Assert.Equal(secondResult, actualResults.Last());
            }
        }

        private IEnumerable<string> LoadData(ISolution solution)
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
                yield return new object[] { new Problems.Day01.Day01_Solution(), "3423279", "5132018" };
                yield return new object[] { new Problems.Day02.Day02_Solution(), "10566835", "2347" };

                yield return new object[] { new Problems.Day03.Day03_Solution(), "303", "11222" };
                yield return new object[] { new Problems.Day04.Day04_Solution(), "895", "591" };
                yield return new object[] { new Problems.Day05.Day05_Solution(), "12234644", "3508186" };
                yield return new object[] { new Problems.Day06.Day06_Solution(), "154386", "346" };
                yield return new object[] { new Problems.Day07.Day07_Solution(), "440880", "3745599" };
                yield return new object[] { new Problems.Day08.Day08_Solution(), "2159", null };
                yield return new object[] { new Problems.Day09.Day09_Solution(), "2316632620", "78869" };
                yield return new object[] { new Problems.Day10.Day10_Solution(), "247", "1919" };
                yield return new object[] { new Problems.Day11.Day11_Solution(), "2594", null };

                yield return new object[] { new Problems.Day12.Day12_Solution(), "12773", "306798770391636" };
                yield return new object[] { new Problems.Day13.Day13_Solution(), "270", "12535" };
                yield return new object[] { new Problems.Day14.Day14_Solution(), "751038", "2074843" };
                yield return new object[] { new Problems.Day15.Day15_Solution(), "308", "328" }; 
                yield return new object[] { new Problems.Day16.Day16_Solution(), "85726502", "92768399" };
                yield return new object[] { new Problems.Day17.Day17_Solution(), "8084", "1119775" };
                yield return new object[] { new Problems.Day18.Day18_Solution(), "6316", null };
                yield return new object[] { new Problems.Day18.Day18_Solution_ParallelRobots(), "1648", null };
                yield return new object[] { new Problems.Day19.Day19_Solution(), "201", "6610984" };
                yield return new object[] { new Problems.Day20.Day20_Solution(), "516", "5966" };
                yield return new object[] { new Problems.Day21.Day21_Solution(), "19360288", "1143814750" };
                yield return new object[] { new Problems.Day22.Day22_Solution(), "4096", "78613970589919" };
                yield return new object[] { new Problems.Day23.Day23_Solution(), "23213", "17874" };
                yield return new object[] { new Problems.Day24.Day24_Solution(), "26840049", "1995" };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }           
        }
    }
}