using System;
using System.Collections.Generic;
using System.Text;
using AoC.Common.Mapping;
using Aoc.AoC2019.IntCode;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Aoc.AoC2019.Problems.Day15
{
    public class Robot
    {
        private readonly IVirtualMachine _computer;
        private readonly ShipMap _map = new ShipMap();
        private readonly Position Start = new Position(0, 0);

        public Robot(IVirtualMachine computer)
        {
            _computer = computer ?? throw new ArgumentNullException(nameof(computer));
            _map.Add(Start, ShipTile.Start);
            _map.MoveDroid(Start);
        }

        public void ExploreShip()
        {
            while(true)
            {
                Position nextLocation = new Position(_map.Droid.X, _map.Droid.Y);
                while (_computer.Outputs.Count < 1 && _computer.Status != ExecutionStatus.Finished)
                {
                    // Awaiting input - so find the next move and input it
                    if (_computer.Status == ExecutionStatus.AwaitingInput)
                    {
                        // Console.WriteLine("Generatining directions");

                        var move = _map.FindPathToNearestValue(_map.Droid, ShipTile.Unknown);
                       
                        if (move == null) { return; }  // no moves found - we must have explored entire grid - so exit.
                        nextLocation = move.GetFirstMove().Position; // get first location

                        var direction = _map.Droid.FindDirection(nextLocation);
                        _computer.Execute((long) direction);
                    }
                    else
                    {
                        // its not awaiting input, and no outputs -so just keep executing.
                        _computer.Execute();
                    }
                }

                long? moveResult = _computer.Outputs.Dequeue();
                if (moveResult.HasValue)
                {
                  //  Console.WriteLine($"Moving to: {nextLocation.ToString()} => Output = " + moveResult.Value);
                    HandleResult(moveResult.Value, nextLocation);
                }
                else
                {
                    // no output or locations found - system must have finished
                 //   Console.WriteLine("FINISHED!!");
                    return;
                        
                }

                //Console.SetCursorPosition(0, 0);
                //Console.WriteLine("Droid Locaion: " + _map.Droid.ToString());
                //Console.WriteLine(_map.DrawMap());
                //Thread.Sleep(25);
              //  Console.Out.WriteLine("Press enter to continue!");
              //  Console.ReadLine();
            }
        }

        public string DrawMap()
        {
            return _map.DrawMap();
        }

        public Position GetOxygenPosition()
        {
            return _map.Oxygen;
        }

        public MapNode GetPathToOxygen()
        {
            return _map.FindPathToPosition(Start, _map.Oxygen);
        }

        // Deal with the returned code for the IntCodeCompiler for the given locations
        private void HandleResult(long moveResult, Position nextLocation)
        {
            switch (moveResult)
            {
                case (0):
                    _map[nextLocation] = ShipTile.Wall;  // we hit a wall.  So don't move
                    break;
                case (1):
                    // Tile is empty - so move droid there.
                    _map[nextLocation] = ShipTile.Empty;
                    _map.MoveDroid(nextLocation);
                    break;
                case (2):
                     // Found the Oxygen supply!
                    _map[nextLocation] = ShipTile.OxygenSystem;
                    _map.MoveDroid(nextLocation);
                    break;
                default:
                    throw new InvalidOperationException("Invalid output value.");
            }
        }

        // Fill 1 adjacent per minute - so find maximum travel distance
        // to an empty square using BFS
        public int FindTimeToFillWithOxygen()
        {

            var path = _map.FindPathToAllValues(_map.Oxygen, ShipTile.Empty);
            return path.Max(s => s.DistanceFromStart);
          
        }        
    }
}
