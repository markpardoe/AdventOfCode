using Aoc.AoC2019.IntCode;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace Tests.IntCodeComputerTests
{
    public class IntCodeVM_Tests
    {       

        [Theory]
        [MemberData(nameof(GetData))]
        public void IntCodeComputer_ReturnCorrectValues(List<int> input, List<int> expectedResults)
        {
            IVirtualMachine parser = new IntCodeVM(input);
            parser.Execute();
            List<int> actualResults = parser.Data.Select(a => (int)a).ToList();
            Assert.Equal(expectedResults, actualResults);
        }

        [Theory]
        [InlineData(10566835, 1, 12, 2, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 13, 1, 19, 1, 6, 19, 23, 2, 6, 23, 27, 1, 5, 27, 31, 2, 31, 9, 35, 1, 35, 5, 39, 1, 39, 5, 43, 1, 43, 10, 47, 2, 6, 47, 51, 1, 51, 5, 55, 2, 55, 6, 59, 1, 5, 59, 63, 2, 63, 6, 67, 1, 5, 67, 71, 1, 71, 6, 75, 2, 75, 10, 79, 1, 79, 5, 83, 2, 83, 6, 87, 1, 87, 5, 91, 2, 9, 91, 95, 1, 95, 6, 99, 2, 9, 99, 103, 2, 9, 103, 107, 1, 5, 107, 111, 1, 111, 5, 115, 1, 115, 13, 119, 1, 13, 119, 123, 2, 6, 123, 127, 1, 5, 127, 131, 1, 9, 131, 135, 1, 135, 9, 139, 2, 139, 6, 143, 1, 143, 5, 147, 2, 147, 6, 151, 1, 5, 151, 155, 2, 6, 155, 159, 1, 159, 2, 163, 1, 9, 163, 0, 99, 2, 0, 14, 0)]
        [InlineData(19690720, 1, 23, 47, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 13, 1, 19, 1, 6, 19, 23, 2, 6, 23, 27, 1, 5, 27, 31, 2, 31, 9, 35, 1, 35, 5, 39, 1, 39, 5, 43, 1, 43, 10, 47, 2, 6, 47, 51, 1, 51, 5, 55, 2, 55, 6, 59, 1, 5, 59, 63, 2, 63, 6, 67, 1, 5, 67, 71, 1, 71, 6, 75, 2, 75, 10, 79, 1, 79, 5, 83, 2, 83, 6, 87, 1, 87, 5, 91, 2, 9, 91, 95, 1, 95, 6, 99, 2, 9, 99, 103, 2, 9, 103, 107, 1, 5, 107, 111, 1, 111, 5, 115, 1, 115, 13, 119, 1, 13, 119, 123, 2, 6, 123, 127, 1, 5, 127, 131, 1, 9, 131, 135, 1, 135, 9, 139, 2, 139, 6, 143, 1, 143, 5, 147, 2, 147, 6, 151, 1, 5, 151, 155, 2, 6, 155, 159, 1, 159, 2, 163, 1, 9, 163, 0, 99, 2, 0, 14, 0)]
        public void CheckLeftMostValue(int expectedResult, params int[] input)
        {
            IntCodeVM parser = new IntCodeVM(input);
            parser.Execute();
            Assert.Equal(expectedResult, parser.Data[0]);
        }

        [Theory]
        [InlineData(7, 999, 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99)]
        [InlineData(1, 999, 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99)]
        [InlineData(8, 1000, 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99)]
        [InlineData(9, 1001, 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99)]
        [InlineData(127, 1001, 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99)]
        public void Test_JumpAndEqualsOperations(int input, int expectedResult, params int[] values)
        {
            IntCodeVM parser = new IntCodeVM(values, input);
            parser.Execute();

            Assert.Equal(expectedResult, parser.Outputs.DequeueToList().Last());
        }

        [Theory]
        [InlineData(8, 1, 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8)]
        [InlineData(6, 0, 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8)]
        [InlineData(9, 0, 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8)]
        [InlineData(8, 1, 3, 3, 1108, -1, 8, 3, 4, 3, 99)]
        [InlineData(7, 0, 3, 3, 1108, -1, 8, 3, 4, 3, 99)]
        [InlineData(9, 0, 3, 3, 1108, -1, 8, 3, 4, 3, 99)]
        public void Test_EqualsOperations(int input, int expectedResult, params int[] values)
        {
            IntCodeVM parser = new IntCodeVM(values, input);
            parser.Execute();
            Assert.Equal(expectedResult, parser.Outputs.DequeueToList().Last());
        }

        [Theory]
        [InlineData(0,0, 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9)]
        [InlineData(1, 1, 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9)]
        [InlineData(2, 1, 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9)]
        [InlineData(0, 0, 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1)]
        [InlineData(1, 1, 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1)]
        [InlineData(2, 1, 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1)]
        public void Test_JumpOperations(int input, int expectedResult, params int[] values)
        {
            IntCodeVM parser = new IntCodeVM(values, input);
            parser.Execute();
            Assert.Equal(expectedResult, parser.Outputs.DequeueToList().Last());
        }

        [Theory]
        [InlineData(7, 1, 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8)]
        [InlineData(9, 0, 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8)]
        [InlineData(8, 0, 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8)]
        [InlineData(7, 1, 3, 3, 1107, -1, 8, 3, 4, 3, 99)]
        [InlineData(9, 0, 3, 3, 1107, -1, 8, 3, 4, 3, 99)]
        [InlineData(8, 0, 3, 3, 1107, -1, 8, 3, 4, 3, 99)]
        public void Test_LessThanOperations(int input, int expectedResult, params int[] values)
        {
            IntCodeVM parser = new IntCodeVM(values, input);
            parser.Execute();
            Assert.Equal(expectedResult, parser.Outputs.DequeueToList().Last());
        }

        [Theory]
        [InlineData(109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99)]
        public void Test_RelativeBaseOperation_ReturnsItself(params int[] values)
        {
            IntCodeVM parser = new IntCodeVM(values);
            parser.Execute();
            List<long> result = parser.Outputs.DequeueToList();
            List<long> expectedResult = values.Select(a => (long)a).ToList();
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(1125899906842624)]
        [InlineData(Int64.MaxValue)]
        [InlineData(Int64.MinValue)]
        [InlineData(0)]
        public void Test_Test_HandlesLargeNumbers(long value)
        {
            List<long> input = new List<long>() { 104, value, 99 };
            IntCodeVM parser = new IntCodeVM(input);
            parser.Execute();

            long result = parser.Outputs.DequeueToList().Last();
            Assert.Equal(value, result);
        }


        public static IEnumerable<object[]> GetData()
        {
            yield return new object[] { new List<int> { 1, 0, 0, 0, 99 }, new List<int> { 2, 0, 0, 0, 99 } };
            yield return new object[] { new List<int> { 2, 3, 0, 3, 99 }, new List<int> { 2, 3, 0, 6, 99 } };
            yield return new object[] { new List<int> { 2, 4, 4, 5, 99, 0 }, new List<int> { 2, 4, 4, 5, 99, 9801 } };
            yield return new object[] { new List<int> { 1, 1, 1, 4, 99, 5, 6, 0, 99 }, new List<int> { 30, 1, 1, 4, 2, 5, 6, 0, 99 } };
        }
    }
}
