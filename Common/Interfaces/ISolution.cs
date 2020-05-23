using System;
using System.Collections.Generic;
using System.Text;

namespace AoC.Common
{
    public interface ISolution
    {
        IEnumerable<string> Solve(IEnumerable<string> input);
        int Year { get; }
        int Day { get; }
        string Name { get; }
        string InputFileName { get; }
        string URL { get; }        
    }
}
