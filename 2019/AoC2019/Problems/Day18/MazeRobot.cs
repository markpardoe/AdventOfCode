using AoC.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day18
{
    public class MazeRobot
    {
        public Maze Maze { get; }
        public MazeTile StartLocation { get; }

        private readonly bool _ignoreDoors = true;
        
        // List of all keys reachable for this robot (ignoring doors). 
        // this is the target set of keys - once they're all collected the maze is complete.
        private readonly  List<MazeTile> _keysReachable;

        public MazeRobot(Maze maze, MazeTile startLocation, bool ignoreDoors = false)
        {
            Maze = maze ?? throw new ArgumentNullException(nameof(maze)); ;
            StartLocation = startLocation ?? throw new ArgumentNullException(nameof(startLocation));
            _ignoreDoors = ignoreDoors;
            _keysReachable = maze.FindKeys(startLocation).Select(k => k.Destination).ToList();
        }

        /// <summary>
        /// Finds the shortest path through the maze picking up all keys.
        /// </summary>
        /// <returns></returns>
        public Path FindShortestPath()
        {
            HashSet<Path> openList = GetInitialPaths();  // Get initial moves for the robot.
            HashSet<Path> closedList = new HashSet<Path>();

            while (openList.Count > 0)
            {
                Path current = openList.OrderBy(p => p.TotalDistance).ThenByDescending(p => p.KeysCollected.Count).First();

                // move location to closed list
                closedList.Add(current);
                openList.Remove(current);

                if (current.KeysCollected.Count == _keysReachable.Count)
                {
                    // If we've collected all the keys - then we're done.
                    return current;
                }

                HashSet<KeyDistance> neighbours = Maze.KeyDistances[new Position(current.X, current.Y)];

                foreach (var kd in neighbours)
                {
                    // We can only move to a valid destination - ie. we have the keys for any doors on the way.
                    if (DestinationIsValid(kd, current))
                    {
                        var keysFound = current.KeysCollected.Append(kd.Destination.KeyId).ToArray();
                        Path newPath = new Path(kd.Destination, keysFound)
                        {
                            Parent = current,
                            TotalDistance = current.TotalDistance + kd.Distance,
                        };

                        foreach (KeyDistance d in current.PathTraveled)
                        {
                            newPath.PathTraveled.Add(d);
                        }
                        newPath.PathTraveled.Add(kd);

                        // if location is in the closed / open lists  - check if we've found a faster way there
                        // & update distance from start if we have a shorter path                    
                        // Otherwise we need to add it as a new move
                        if (!UpdateLocationInList(closedList, newPath) && !UpdateLocationInList(openList, newPath))
                        {
                            openList.Add(newPath);
                        }
                    }                               
                }
            }

            // nothing found - should never happen
            return null;
        }

        // Check if the Path already exists in the HashSet.  If so, update the existing Path (if shorter) and return true;
        private bool UpdateLocationInList(HashSet<Path> list, Path currentPosition)
        {
            // update distance from start if we have a shorter path
            if (list.TryGetValue(currentPosition, out Path p))
            {
                if (p.TotalDistance > currentPosition.TotalDistance )
                {
                    p.TotalDistance = currentPosition.TotalDistance;
                    p.Parent = currentPosition.Parent;
                    p.KeysCollected = currentPosition.KeysCollected;
                    p.PathTraveled = currentPosition.PathTraveled;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        // Check if we actually want to move to the new destination.
        // It may have already been found, or there are locked doors on the way.
        private bool DestinationIsValid(KeyDistance kd, Path current)
        {
            string keyId = kd.Destination.KeyId;

            // check if this key has already been found
            if (current.KeysCollected.Contains(keyId))
            {
                return false;
            }

            if (!_ignoreDoors)
            {
                // check if the path is blocked to the key
                foreach (string door in kd.Doors)
                {
                    if (!current.KeysCollected.Contains(door))
                    {
                        return false;
                    }
                }
            }

            // check if we've got any keys on the way - that haven't already been collected.
            foreach (string key in kd.ExtraKeys)
            {
                if (!current.KeysCollected.Contains(key))
                {
                    // we've got a key we collect on the way - so don't use this route.
                    // Eg.  A --> C --> B.
                    // We don't need to bother with route A --> B as A --> C will always be shorter & C --> B will then be checked later.
                    return false;
                }
            }          

            return true;
        }

        public string DrawMap()
        {
            return Maze.DrawMap(this.StartLocation.Position);
        }


        // Used to draw the path of the robot through the maze step-by-step.
        public IEnumerable<string> DrawPath(Path path)
        {
            
           foreach (KeyDistance kd in path.PathTraveled)
           {
                foreach(Position p in kd.Path)
                {
                    yield return Maze.DrawMap(p);
                }
           }
           yield break;
        }

        // Generates the initial paths for the robot - from the start location
        private HashSet<Path> GetInitialPaths()
        {
            HashSet<Path> paths = new HashSet<Path>();

            // We need to generate the first moves from the start location
            HashSet<KeyDistance> startMoves = Maze.FindKeys(this.StartLocation);

            List<string> initialKeys = Maze.KeyPositions.Select(p => p.KeyId).ToList();

            List<KeyDistance> moves = new List<KeyDistance>();

            // If only 1 robot - we have to ignore paths with doors as we can't get through them.
            // If multiple robots - assume that another robot will open them for us.
            if (_ignoreDoors)
            {
                moves = startMoves.Where(p => p.ExtraKeys.Count == 0).ToList() ;
            }
            else
            {
                moves = startMoves.Where(p => (p.Doors.Count == 0 && p.ExtraKeys.Count == 0)).ToList();
            }

            // We can ignore any moves that require doors opened - or have extra keys for the 1st move.
            foreach (KeyDistance kd in moves)
            {

                Path p = new Path(kd.Destination, kd.Destination.KeyId)
                {
                    TotalDistance = kd.Distance                  
                };
                p.PathTraveled.Add(kd);
                paths.Add(p);

            }
            return paths;
        }
    }
}
