using System;
using AoC.Common;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using AoC.AoC2020.Problems.Day01;

namespace AoC.AoC2020
{
    class Program
    {
        static void Main()
        {
            AoCSolution problem = new Problems.Day01.Day01_Solution();
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