using System;
using System.Collections.Generic;
using System.Text;

namespace AoC.Common
{
    public interface IInputData
    {
        string InputFileName { get; }
    }

    public interface ISolution<out T> : IInputData
    {
        string Site { get; }
        string Category { get; }
        string SubCategory { get; }
        string Name { get; }
        string URL { get; }

        IEnumerable<T> Solve(IEnumerable<string> input);
        int Year { get; }
        int Day { get; }          
    }
}