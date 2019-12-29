using AoC.Common.Mapping;
using AoC2019.IntCodeComputer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AoC2019.Problems.Day13
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
      //  private Dictionary<Position, TileType> _tiles = new Dictionary<Position, TileType>();
        public GameMap Map { get; } = new GameMap();

        public int? PaddleTarget { get; private set; } = null;

        public ArcadeCabinet(IVirtualMachine vm)
        {
            _computer = vm ?? throw new ArgumentNullException(nameof(vm));
        }

        internal string PrintMap()
        {
            return Map.DrawMap();
        }


        public int CountBlocks(TileType type)
        {
            return Map.Values.Where(a => a == type).ToList().Count;
        }

        public void RunGame()
        {
            while (true)
            {
                while (_computer.Outputs.Count < 3 && _computer.Status != ExecutionStatus.Finished)
                {
                    if (_computer.Status == ExecutionStatus.AwaitingInput)
                    {

                        int direction = 0;
                        if (Map.Ball.X  == Map.Paddle.X)
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

                long? X = _computer.Outputs.Dequeue();
                long? Y = _computer.Outputs.Dequeue();
                long? tileId = _computer.Outputs.Dequeue();

                if (X.HasValue && X.Value == -1)
                {
                    FinalScore =  tileId.Value;                   
                }

                // Program finished - no more instructions
                if (X.HasValue && Y.HasValue && tileId.HasValue)
                {
                    Map.Add(new Position((int)X.Value, (int)Y.Value), (TileType)((int)tileId.Value));
                }
                else
                {
                    return;
                }

                if (_computer.Status == ExecutionStatus.Finished)
                {
                    return;
                }
            }
        }
    }
}
