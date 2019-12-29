using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AoC.Common;
using AoC2019.IntCodeComputer;

namespace AoC2019.Problems.Day13
{
    public class Day13_Solution : ISolution
    {
        public int Year => 2019;

        public int Day => 13;

        public string Name => "Day 13: Care Package";

        public string InputFileName => "Day13.txt";

        public IEnumerable<string> Solve(IEnumerable<string> input)
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
