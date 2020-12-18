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
    }
}