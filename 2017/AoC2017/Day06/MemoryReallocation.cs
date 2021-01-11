using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC.Common;

namespace AoC2017.Day06
{
    public class MemoryReallocation : AoCSolution<int>
    {
        public override int Year => 2017;
        public override int Day => 6;
        public override string Name => "Day 6: Memory Reallocation";
        public override string InputFileName => null;

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            Memory mem = new Memory(_rawData.ToList());
            var results =  mem.RedistributeMemory();
            yield return results.finalTurn;
            yield return results.loopSize;
        }

        private readonly IEnumerable<int> _rawData = new List<int>() {14, 0, 15, 12, 11, 11, 3, 5, 1, 6, 8, 4, 9, 1, 8, 4};

        private readonly IEnumerable<int> _example = new List<int>() { 0,2,7,0 };
    }

    public class Memory
    {
        private readonly int[] _memoryBanks;

        public Memory(IReadOnlyList<int> rawData)
        {
            _memoryBanks = rawData.ToArray();
        }

        public (int finalTurn, int loopSize) RedistributeMemory()
        {
            int turn = 0;
           // var PreviousStates = new HashSet<string>();
            Dictionary<string, int> PreviousStates = new Dictionary<string, int>();

            string state = this.ToString();

            while (!PreviousStates.ContainsKey(state))
            {
                PreviousStates.Add(state, turn);

                // Console.WriteLine($"{turn}: {state}");
                var blocksToAllocate = _memoryBanks.Max();

                // Get index of the value
                var currentIndex = Array.IndexOf(_memoryBanks, blocksToAllocate);
                var index = currentIndex;
                
                while (blocksToAllocate > 0)
                {
                    index += 1;
                    if (index >= _memoryBanks.Length)
                    {
                        index = 0;
                    }

                    _memoryBanks[currentIndex]--;
                    _memoryBanks[index]++;

                    blocksToAllocate -= 1;
                }


                state = this.ToString();
                turn++;
            }

            int loopSize = turn - PreviousStates[state];


            return (turn, loopSize);
        }

        public override string ToString()
        {
            return string.Join(',', _memoryBanks);
        }


    }

    public class MemoryBank
    {
        public int Blocks { get; private set; }

        public MemoryBank(int blocks)
        {
            Blocks = blocks;
        }

        public void AddBlock()
        {
            Blocks++;
        }

        public void RemoveBlock()
        {
            Blocks--;
        }
    }
}
