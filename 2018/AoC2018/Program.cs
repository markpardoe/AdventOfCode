using System;
using AoC.Common;
using System.IO;
using System.Linq;
using Aoc.Aoc2018;
using System.Collections.Generic;

namespace Aoc.Aoc2018
{
    class Program
    {
        static void Main()
        {
            ISolution problem = new Aoc2018.Day07.Day07_Solution();

            string inputFile = Path.Combine(System.Environment.CurrentDirectory, "InputData", problem.InputFileName);
            var data = File.ReadAllLines(inputFile).ToList();

            foreach (var result in problem.Solve(data))
            {
                Console.WriteLine(result);
            }

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
    }
}
