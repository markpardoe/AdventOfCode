using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AoC2017
{
    public class Program
    {
        static void Main()
        {
            var problem = new Day03.SpiralMemory();
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
