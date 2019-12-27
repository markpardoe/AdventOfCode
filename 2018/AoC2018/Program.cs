using System;
using AoC.Common;
using System.IO;
using System.Linq;
using AoC2018.Day03;
using AoC2018.Day04;
using AoC2018.Day05;
using AoC2018.Day07;
using System.Collections.Generic;

namespace AoC2018
{
    class Program
    {
        static void Main(string[] args)
        {
            ISolution problem = new Day07_Solution();

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
