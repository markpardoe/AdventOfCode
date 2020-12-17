using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AoC.Common.TestHelpers
{
    /// <summary>
    /// Helper functions for processing input data
    /// </summary>
    public static class InputData
    {
        public static IEnumerable<string> LoadSolutionInput(IInputData solution)
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
    }
}
