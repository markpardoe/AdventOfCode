using System.Collections;
using System.Collections.Generic;

namespace AoC.Common.TestHelpers
{
    /// <summary>
    /// Data class for passing strongly typed arguments to a XUnit Theory
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SolutionData<T> : IEnumerable<object[]>
    {
        readonly List<object[]> data = new List<object[]>();

        public SolutionData(ISolution<T> sut, T result1, T result2)
        {
            data.Add(new object[] {sut, result1, result2});
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
