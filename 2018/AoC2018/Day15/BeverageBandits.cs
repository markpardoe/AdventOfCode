using System;
using System.Collections.Generic;
using System.Linq;
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
            var result = RunSimulation(input.ToList());
            yield return result.FinalTurn * result.HP;

            yield return GetOptimumElfAttackValue(input);

        }

        public SimulationResult RunSimulation(IEnumerable<string> input, int elfAttackValue = 3)
        {
            ArenaMap map = new ArenaMap(input, elfAttackValue);
         //   Console.WriteLine(map.DrawMap());

            int originalGoblinQty = map.GoblinCount;
            int originalElfQty = map.ElfCount;

            GameStatus status = GameStatus.Running;
            while (status == GameStatus.Running)
            {
                status = map.RunTurn();
            }

            //  Console.Out.WriteLine(map.DrawMap());

            var hp = map.SumRemainingHitpoints();

            return new SimulationResult()
            {
                HP = map.SumRemainingHitpoints(),
                Winner = status,
                FinalTurn = map.Turn,
                FinalState = map.DrawMap(),
                GoblinsLost = originalGoblinQty - map.GoblinCount,
                ElvesLost = originalElfQty - map.ElfCount
            };
        }


        public int GetOptimumElfAttackValue(IEnumerable<string> input)
        {
            int attackValue = 4;

            while (true)
            {
                var result = RunSimulation(input.ToList(), attackValue);

                if (result.ElvesLost == 0)
                {
                    Console.WriteLine($"Attack Value = {attackValue}.  HP remaining = {result.HP}");
                    Console.WriteLine(result.FinalState);

                    return result.HP * result.FinalTurn;

                }

                attackValue++;
            }
        }


        // Return class
        public class SimulationResult
        {
            public int HP { get; set; }
            public int FinalTurn { get; set; }
            public GameStatus Winner { get; set; }
            public string FinalState { get; set; }

            public int ElvesLost { get; set; }
            public int GoblinsLost { get; set; }
        }

        
        // Elves win in 37 rounds
        // 982 hp = 36334
        private static readonly List<string> Example1 = new List<string>
        {
            "#######",
            "#G..#E#",
            "#E#E.E#",
            "#G.##.#",
            "#...#E#",
            "#...E.#",
            "#######"
        };


        // Elves win in 46 rounds. 859 hp = 39514
        private static readonly List<string> Example2 = new List<string>
        {
            "#######",
            "#E..EG#",
            "#.#G.E#",
            "#E.##E#",
            "#G..#.#",
            "#..E#.#",
            "#######"
        };

        // Goblins win in round 35.  793 hp.  = 27755
        private static readonly List<string> Example3 = new List<string>
        {
            "#######",
            "#E.G#.#",
            "#.#G..#",
            "#G.#.G#",
            "#G..#.#",
            "#...E.#",
            "#######"
        };

        // Goblins win in 54 rounds.  536 hp = 28944
        private static readonly List<string> Example4 = new List<string>
        {
            "#######",
            "#.E...#",
            "#.#..G#",
            "#.###.#",
            "#E#G#G#",
            "#...#G#",
            "#######"
        };

        // Goblins win in 20 rounds.  937 hp = 18740
        private static readonly List<string> Example5 = new List<string>
        {
            "#########",
            "#G......#",
            "#.E.#...#",
            "#..##..G#",
            "#...##..#",
            "#...#...#",
            "#.G...G.#",
            "#.....G.#",
            "#########"
        };
    }
}