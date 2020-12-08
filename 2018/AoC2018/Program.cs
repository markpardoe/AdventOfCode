using AoC.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aoc.Aoc2018;

namespace Aoc.Aoc2018
{
    class Program
    {
        static void Main()
        {
            var problem = new Day09.MarbleMania();
          
            List<string> data = new List<string>();

            if (problem.InputFileName != null)
            {
                string inputFile =
                    Path.Combine(System.Environment.CurrentDirectory, "InputData", problem.InputFileName);
                data = File.ReadAllLines(inputFile).ToList();

            }
           

            foreach (var result in problem.Solve(data))
            {
                Console.WriteLine(result);
            }

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
    }
}
