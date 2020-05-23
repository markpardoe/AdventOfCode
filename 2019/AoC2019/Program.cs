using System;
using Aoc.AoC2019.Problems;
using AoC.Common;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Aoc.AoC2019
{
    class Program
    {
        static void Main()
        {
            AoCSolution problem = new Problems.Day16.Day16_Solution();
            List<string> data = new List<string>();
            if (problem.InputFileName != null)
            {
                string inputFile = Path.Combine(System.Environment.CurrentDirectory, "InputData", problem.InputFileName);
                data.AddRange(File.ReadAllLines(inputFile).ToList());
            }

            Console.WriteLine(problem.Name);

            foreach (var result in problem.Solve(data))
            {
                Console.WriteLine(result);
            }

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
    }
}
