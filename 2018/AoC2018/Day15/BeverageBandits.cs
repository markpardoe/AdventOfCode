using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AoC.Common;

namespace Aoc.Aoc2018.Day15
{
    public class BeverageBandits : AoCSolution<long>
    {

        public override int Year => 2018;
        public override int Day => 15;
        public override string Name => "Day 15: Beverage Bandits";
        public override string InputFileName => "Day15.txt";

        public override IEnumerable<long> Solve(IEnumerable<string> input)
        {
            ArenaMap map = new ArenaMap(input);
            Console.WriteLine(map.DrawMap());
          //  Console.ReadKey();

            GameStatus status = GameStatus.Running;
            while (status == GameStatus.Running)
            {
                status = map.RunTurn();

                Console.SetCursorPosition(0,0);
                Console.WriteLine(map.DrawMap());
                
                Thread.Sleep(200);
             //   Console.ReadKey();
            }

            var hp = map.RemainingHitpoints();
            Console.WriteLine($"HP: {hp}");
            yield return hp * (map.Turn - 1);
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

        private static readonly List<string> Example2 = new List<string>
        {
            "#######",
            "#G..#E#",
            "#E#E.E#",
            "#G.##.#",
            "#...#E#",
            "#...E.#",
            "#######"
        };
    }
}