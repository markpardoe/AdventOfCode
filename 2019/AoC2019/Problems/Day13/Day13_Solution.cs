using Aoc.AoC2019.IntCode;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day13
{
    public class Day13_Solution : AoC2019Solution
    {
        public override string URL => @"https://adventofcode.com/2019/day/13";

        public override int Day => 13;

        public override string Name => "Day 13: Care Package";

        public override string InputFileName => "Day13.txt";

        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {
            yield return RunGame(input.First()).CountBlocks(TileType.Block).ToString();
            yield return RunGame(input.First(), 2).FinalScore.ToString();
        }

        public ArcadeCabinet RunGame(string input, int? initialInstruction = null)
        {
            var data = IntCodeVM.ParseStringData(input);
            if (initialInstruction.HasValue)
            {
                data[0] = initialInstruction.Value;
            }
            IVirtualMachine vm = new IntCodeVM(data);
            ArcadeCabinet cabinet = new ArcadeCabinet(vm);

            cabinet.RunGame();
           // Console.WriteLine(cabinet.Map.DrawMap());
            return cabinet;
        }
    }
}
