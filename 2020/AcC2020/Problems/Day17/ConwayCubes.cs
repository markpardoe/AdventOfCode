using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;

namespace AoC.AoC2020.Problems.Day17
{
    public class ConwayCubes :AoCSolution<int>
    {
        public override int Year => 2020;
        public override int Day => 17;
        public override string Name => "Day 17: Conway Cubes";
        public override string InputFileName => "Day17.txt";

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            var map = new ConwayCubeMap(input);
            // Console.WriteLine(map.DrawMap());
            // Console.WriteLine("-------------------------------------");

            map.RunGeneration(6);
            yield return map.CountValue(CubeStatus.Active);

            var map4d = new Conway4dCubeMap(input);

            map4d.RunGeneration(6);
            yield return map4d.CountValue(CubeStatus.Active);
        }


        public List<string> Example1 = new List<string>()
        {
            ".#.",
            "..#",
            "###"
        };

    }
}
