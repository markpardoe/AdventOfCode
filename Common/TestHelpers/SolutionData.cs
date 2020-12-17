using System.Collections;
using System.Collections.Generic;

namespace AoC.Common.TestHelpers
{
    /// <summary>
    /// Data class for passing strongly typed solution results to a XUnit Theory
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SolutionData<T>
    {
        public ISolution<T> SolutionUnderTest { get; }
        public T Result1 { get; }
        public T Result2 { get; }

        public SolutionData(ISolution<T> solutionUnderTest, T result1, T result2)
        {
            SolutionUnderTest = solutionUnderTest;
            Result1 = result1;
            Result2 = result2;
        }
    }
}