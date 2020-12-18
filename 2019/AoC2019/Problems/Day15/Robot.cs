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

        public Robot(IVirtualMachine computer)
        {
            _computer = computer ?? throw new ArgumentNullException(nameof(computer));
            Position start = new Position(0, 0);
            _map.Add(start, ShipTile.Start);
            _map.MoveDroid(start);
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

                        List<Position> moves = GetPathToTile(_map.Droid, ShipTile.Unknown);  // find the nearest 'unknown' tile

                        if (moves.Count == 0) { return; }  // no moves found - we must have explored entire grid - so exit.
                        nextLocation = moves[0]; // get first location

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

        public List<Position> GetPathToOxygen()
        {
            return GetPathToTile(new Position(0, 0), ShipTile.OxygenSystem);
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

        // Fill 1 adjacent per minute - so find maximum distance from Oxygen
        public int FindTimeToFillWithOxxygen()
        {
            Position oxygenLocation = _map.Oxygen;
            var openList = new HashSet<MapNode>();  // list of celles to be checked
            var closedList = new HashSet<MapNode>();  // checked locations

            MapNode current = new MapNode(oxygenLocation);
            // Add additional points to check
            openList.Add(current);

            while (openList.Count > 0)
            {
                // get the closest square
                current = openList.OrderBy(l => l.DistanceFromStart).First();

                // move location to closed list
                closedList.Add(current);
                openList.Remove(current);
         
                var neighbours = _map.GetAvailableNeighbors(current);

                foreach (var location in neighbours)
                {
                    MapNode locationPos = new MapNode(location)
                    {
                        Parent = current,
                        DistanceFromStart = current.DistanceFromStart + 1
                    };

                    // if location is in the closed / open lists  - check if we've found a faster way there
                    // & update distance from start if we have a shorter path                    
                    // Otherwise we need to add it as a new move
                    if (!UpdateLocationInList(closedList, locationPos) && !UpdateLocationInList(openList, locationPos))
                    {
                        openList.Add(locationPos);
                    }
                }
            }

            //Console.WriteLine("Map Searched.");
            return closedList.Max(p => p.DistanceFromStart);
        }

        private MapNode FindTarget(Position start, ShipTile targetType)
        {
          //  Console.WriteLine("Calculating next move....");

            // breadth first search to find next available position
            var openList = new HashSet<MapNode>();  // list of cells to be checked
            var closedList = new HashSet<MapNode>();  // checked locations

            MapNode current = new MapNode(start);

            // Add additional points to check
            openList.Add(current);

            while (openList.Count > 0)
            {
                // get the closest square
                current = openList.OrderBy(l => l.DistanceFromStart).First();

                // move location to closed list
                closedList.Add(current);
                openList.Remove(current);

                // check if we've found an empty cell?
                if (_map[current] == targetType) { return current; }

                var neighbours = _map.GetAvailableNeighbors(current);

                foreach (var location in neighbours)
                {
                    MapNode locationPos = new MapNode(location)
                    {
                        Parent = current,
                        DistanceFromStart = current.DistanceFromStart + 1
                    };

                    // if location is in the closed / open lists  - check if we've found a faster way there
                    // & update distance from start if we have a shorter path                    
                    // Otherwise we need to add it as a new move
                    if (!UpdateLocationInList(closedList, locationPos) && !UpdateLocationInList(openList, locationPos))
                    {
                        openList.Add(locationPos);
                    }
                }
            }
          //  Console.WriteLine("Next move found.");
            // nothing found
            return null;
        }

        private List<Position> GetPathToTile(Position start, ShipTile targetType)
        {
            List<Position> path = new List<Position>();
            MapNode target = FindTarget(start, targetType); 

            if (target == null) { return path; }  // no target found - so no moves to make
            
          //  Console.WriteLine("Target found: " + target.Position.ToString());


            while (target != null && !target.Equals(start)) 
            {
                path.Add(target);
                target = target.Parent;
            }
            path.Reverse();  // list is in the wrong direction!

            return path;
        }

        private bool UpdateLocationInList(HashSet<MapNode> list, MapNode currentPosition)
        {
            // update distance from start if we have a shorter path
            if (list.TryGetValue(currentPosition, out MapNode p))
            {
                if (p.DistanceFromStart > currentPosition.DistanceFromStart + 1)
                {
                    p.DistanceFromStart = currentPosition.DistanceFromStart + 1;
                    p.Parent = currentPosition;
                }
                return true;
            }
            else
            {
                return false;
            }
        }       
    }
}
