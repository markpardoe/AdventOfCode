using System;
using System.Collections.Generic;
using System.Text;

namespace AoC.Common
{
    public interface ISolution<out T>
    {
        string Site { get; }
        string Category { get; }
        string SubCategory { get; }
        string Name { get; }
        string InputFileName { get; }
        string URL { get; }

   //     string SolveSingle(IEnumerable<string> Input);

        IEnumerable<T> Solve(IEnumerable<string> input);
        int Year { get; }
        int Day { get; }          
    }
}