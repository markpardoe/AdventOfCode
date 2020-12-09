using System;
using System.Collections.Generic;
using System.Text;
using AoC.Common;

namespace Aoc.Aoc2018.Day15
{
    public class BeverageBandits : AoCSolution<int>
    {

        public override int Year => 2018;
        public override int Day => 15;
        public override string Name => "Day 15: Beverage Bandits";
        public override string InputFileName => "Day15.txt";

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            ArenaMap map = new ArenaMap(Example1);
            Console.WriteLine(map.DrawMap());
            Console.ReadKey();

            GameStatus status = GameStatus.Running;
            while (status == GameStatus.Running)
            {
                status = map.RunTurn();

                Console.SetCursorPosition(0,0);

              // Console.Clear();
                Console.WriteLine(map.DrawMap());
                
                Console.ReadKey();
            }


            yield return -1;
        }



        private static readonly List<string> Example1 = new List<string>
        {
            "#######",
            "#.G...#",
            "#...EG#",
            "#.#.#G#",
            "#..G#E#",
            "#.....#",
            "#######"
        };
    }
}