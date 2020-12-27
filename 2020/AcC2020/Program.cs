using System;
using AoC.Common;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using AoC.AoC2020.Problems.Day24;
using AoC.AoC2020.Problems.Day25;

namespace AoC.AoC2020
{
    class Program
    {
        static void Main()
        {
            var problem = new ComboBreaker();
            var data = new List<string>();

            if (problem.InputFileName != null)
            {
                string inputFile = Path.Combine(System.Environment.CurrentDirectory, "InputData", problem.InputFileName);
                data.AddRange(File.ReadAllLines(inputFile).ToList());
            }

            Console.WriteLine(problem.Name);

            // Time problem execution - ignoring time for reading input file
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (var result in problem.Solve(data))
            {
                Console.WriteLine(result);
            }
            stopWatch.Stop();
            Console.WriteLine();
            Console.WriteLine($"Solved in {stopWatch.ElapsedMilliseconds}ms");
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
    }
}