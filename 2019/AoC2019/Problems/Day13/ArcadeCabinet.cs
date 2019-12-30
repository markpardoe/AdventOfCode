using AoC.Common.Mapping;
using Aoc.AoC2019.IntCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Aoc.AoC2019.Problems.Day13
{
    public enum TileType
    {
        Empty = 0,
        Wall = 1,
        Block = 2,
        Paddle = 3,
        Ball = 4
    }       

    public class ArcadeCabinet
    {
        public long FinalScore { get; private set; }

        private readonly IVirtualMachine _computer;

        public GameMap Map { get; } = new GameMap();

        public int? PaddleTarget { get; private set; } = null;

        public ArcadeCabinet(IVirtualMachine vm)
        {
            _computer = vm ?? throw new ArgumentNullException(nameof(vm));
        }

        public int CountBlocks(TileType type)
        {
            return Map.Values.Where(a => a == type).ToList().Count;
        }

        public void RunGame()
        {
            while (true)
            {
                // Keep executing until we have 3 outputs - or the game is finished.
                while (_computer.Outputs.Count < 3 && _computer.Status != ExecutionStatus.Finished)
                {
                    RunGameLoop();
                }

                ProcessOutputs();

                if (_computer.Status == ExecutionStatus.Finished)
                {
                    return;
                }
            }
        }

        // Deal with the IntCodeVM's output queue.
        private void ProcessOutputs()
        {
            // We process outputs in groups of 3
            while (_computer.Outputs.Count >= 3)
            {
                long X = _computer.Outputs.Dequeue();
                long Y = _computer.Outputs.Dequeue();
                long tileId = _computer.Outputs.Dequeue();

                if (X == -1 && Y == 0)
                {
                    // Update the score
                    FinalScore = tileId;
                }
                else
                {
                    // Update the tile in the given position
                    Map.Add(new Position((int)X, (int)Y), (TileType)((int)tileId));
                }
            }
        }

        private void RunGameLoop()
        {
            if (_computer.Status == ExecutionStatus.AwaitingInput)
            {
                int direction = 0;
                if (Map.Ball.X == Map.Paddle.X)
                {
                    direction = 0;
                }
                else if (Map.Ball.X > Map.Paddle.X)
                {
                    direction = 1;
                }
                else if (Map.Ball.X < Map.Paddle.X)
                {
                    direction = -1;
                }

                //Console.Clear();
                //Console.SetCursorPosition(0, 0); 
                //Console.WriteLine(Map.DrawMap());
                //Thread.Sleep(25);
                //Console.WriteLine("Ball = " + Map.Ball.ToString());
                //  Console.WriteLine("Paddle = " + Map.Paddle.ToString());
                // Console.ReadLine();
                _computer.Execute(direction);
            }
            else
            {
                _computer.Execute();
            }
        }
    }
}
