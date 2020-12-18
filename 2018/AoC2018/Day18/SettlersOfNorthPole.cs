using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using AoC.Common;

namespace Aoc.Aoc2018.Day18
{
    public class SettlersOfNorthPole : AoCSolution<long>
    {
        public override int Year => 2018;
        public override int Day => 18;
        public override string Name => "Day 18: Settlers of The North Pole";
        public override string InputFileName => "Day18.txt";

        public override IEnumerable<long> Solve(IEnumerable<string> input)
        {
            yield return RunSimulation(input, 10);
            yield return RunLargeSimulation(input, 1000000000);
        }

        private long RunSimulation(IEnumerable<string> input, int steps)
        {
            SettlerMap map = new SettlerMap(input);

            map.RunSimulation(steps);
            Console.WriteLine(map.DrawMap());
            File.WriteAllLines(@"E:\simple.txt", new List<string>(){map.DrawMap()});

            int trees = map.CountSettlers(SettlerType.Tree);
            int lumberYards = map.CountSettlers(SettlerType.Lumberyard);

            Console.WriteLine();
            Console.WriteLine($"Trees: {trees}.  LumberYards: {lumberYards}");
            return trees * lumberYards;
        }


        // Find the 2 turns where the state repeats.
        // Since the simulation is deterministic - we know that it will continue to cycle between these 2 stats.
        private Tuple<int, int> FindRepeatState(IEnumerable<string> input)
        {
            Dictionary<string, int>  states = new Dictionary<string, int>();
            SettlerMap map =  new SettlerMap(input);
            int turn = 0;

            string state = map.DrawMap();

            while (!states.ContainsKey(state))
            {
                states.Add(state, turn);
                map.RunSimulation(1);

                turn++;
                state = map.DrawMap();
            }

            Console.WriteLine($"Duplicate State Found On turns {turn} & {states[state]}");
            return new Tuple<int, int>(states[state], turn);
        }

        // Since we know there is a repeating cycle - we only have to move to the start of the cycle
        // and then from the end of the cycle to the end.  
        // So we can skip steps / <cycle size> steps in the middle
        private long RunLargeSimulation(IEnumerable<string> input, long steps)
        {
            // find the repeating sequence
            Tuple<int, int> repeats = FindRepeatState(input);
            int start = repeats.Item1;
            int end = repeats.Item2;
            int range = end - start;

            SettlerMap map = new SettlerMap(input);
            
            // move to start of repeating sequence.
            map.RunSimulation(start);

            // Since we're now in a repeating cycle - we just 'pretend' to run the cycle until we reach the last cycle...

            // Calculate how many steps to run at the end 
            long loopsSkipped = (steps - start) / range;  // how many loops of <range> we can skip
            long currentStep = start + (loopsSkipped * range);
            long remainingSteps = steps - currentStep;

            // Run last steps
            map.RunSimulation(remainingSteps);
            int trees = map.CountSettlers(SettlerType.Tree);
            int lumberYards = map.CountSettlers(SettlerType.Lumberyard);


            return trees * lumberYards;
        }
        
        private readonly List<string> example1 = new List<string>()
        {
            ".#.#...|#.",
            ".....#|##|",
            ".|..|...#.",
            "..|#.....#",
            "#.#|||#|#|",
            "...#.||...",
            ".|....|...",
            "||...#|.#|",
            "|.||||..|.",
            "...#.|..|."
        };
    }
}
