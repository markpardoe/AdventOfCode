using AoC.Common;
using System;
using System.IO;
using System.Linq;

namespace Aoc.Aoc2018
{
    class Program
    {
        static void Main()
        {
            var problem = new Aoc2018.Day07.SumOfItsParts();

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
