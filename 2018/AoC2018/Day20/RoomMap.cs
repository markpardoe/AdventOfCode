using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Aoc.Aoc2018.Day20;
using AoC.Common.DataStructures;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day20
{
    public enum MapTile
    {
        Room = '.', 
        Wall = '#',
        DoorHoziontal = '-',
        DoorVertical = '|',
        Unknown = '?'
    }
    

    public sealed class RoomMap : Map<MapTile>
    {
        private readonly Position _start = new Position(0,0);
        private readonly HashSet<Position> _endPoints = new HashSet<Position>();
        

        public RoomMap(string input) : base(MapTile.Unknown)
        {
           var inputData = input.Skip(1).ToList();
           Add(_start, MapTile.Room);
           var start = new HashSet<Position>() {_start};
           var data = new Queue<char>(input.ToCharArray());
           _endPoints = BuildMap(start, data);  // store endpoints in the maze

           // replace unknown with walls
           foreach (var p in GetBoundedEnumerator())
           {
               if (p.Value == MapTile.Unknown)
               {
                   this[p.Key] = MapTile.Wall;
               }
           }
        }
        
        public int FindMaxDoorsToEndPoint()
        {
            int maxDistance = 0;
            //foreach (var endPoint in _endPoints)
            //{
            //    Console.WriteLine($"Finding Target {endPoint} of {_endPoints.Count}");
            //    var node = FindPathTo(_start, endPoint);
            //    if (node != null && node.DistanceFromStart > maxDistance)
            //    {
            //        maxDistance = node.DistanceFromStart;
            //    }

            //    Console.WriteLine($"Target {endPoint} Found: {node.DistanceFromStart}");
            //}

            var paths = PathToTargets(_start, _endPoints);
            return paths.Max(x => x.DistanceFromStart);

        }

        public HashSet<MapNode> FindPathToAllTargets()
        {
            var rooms = Map.Where(x => x.Value == MapTile.Room).Select(x => x.Key);
            var result = PathToTargets(_start, rooms);

            return result;
        }

        public HashSet<Position> BuildMap(HashSet<Position> startPositions, Queue<char> inputData)
        {
            // Where we end up at the end each path
            HashSet<Position> finalPositions = new HashSet<Position>();
            List<Position> currentPositions = startPositions.ToList();

            while (inputData.Count > 0)
            {
                char token = inputData.Dequeue();

                if (_validDirections.Keys.Contains(token))
                {
                    CompassDirection direction = _validDirections[token];

                    // Use iterator as we're modifying currentpositions as we go
                    for (int i = 0; i < currentPositions.Count; i++)
                    {
                        // Get current position - and replace with the position once we've moved
                        var current = currentPositions[i];
                        currentPositions[i] = Move(direction, current);
                    }
                }

                else if (token == ')') // End of a group of directions
                {
                    break;
                }

                else if (token == '(')
                {
                    // replace the current list of positions with positions after moving in the bracketed group
                    currentPositions = BuildMap(currentPositions.ToHashSet(), inputData).ToList();
                }
                else if (token == '|')
                {
                    // Add current positions to the finals list, and start a new 'current' list from the start
                    finalPositions.UnionWith(currentPositions);
                    currentPositions = startPositions.ToList();
                }
                else if (token == '^' || token == '$')
                {
                    // ignore as this indicates start or end of input
                    continue;
                }
                else
                {
                    throw new DataException($"Invalid token: {token}");
                }
            }

            finalPositions.UnionWith(currentPositions);
            return finalPositions;
        }

        /// <summary>
        /// Performs a BFS to find shortest path to every target in the input
        /// Assumes an unweighted map (ie. distance/cost between points always = 1).
        /// </summary>
        /// <param name="targets"></param>
        /// <returns></returns>
        public HashSet<MapNode> PathToTargets(Position start, IEnumerable<Position> targets)
        {
            // Create a copy of targets to avoid modifying in place
            HashSet<Position> targetList = new HashSet<Position>(targets);
            HashSet<MapNode> output = new HashSet<MapNode>();

            Queue<MapNode> openList = new Queue<MapNode>();
            HashSet<MapNode> closedList = new HashSet<MapNode>();

            openList.Enqueue(new MapNode(start));

            while (openList.Count > 0)
            {
                // get the 1st node
                var currentPosition = openList.Dequeue();

                var neighbors = GetAvailableNeighbors(currentPosition)
                    .Select(x => new MapNode(x, currentPosition, currentPosition.DistanceFromStart + 1));

                foreach (var n in neighbors)
                {
                    // We've reached a target - BFS guarentees its the shortest path
                    if (targetList.Contains(currentPosition))
                    {
                        output.Add(currentPosition);
                        targetList.Remove(currentPosition);

                        // found all the targets - so quit
                        if (targetList.Count == 0)
                        {
                            return output;
                        }
                    }

                    // Check if we've already visited the node (or about to visit it)
                    // BFS guarantee shortest-path, so we don't have to check if the new distance is shorter
                    if (closedList.Contains(n) || openList.Contains(n))
                    {
                        // Already got a shorter path to this position
                        continue;
                    }
                    openList.Enqueue(n);
                }
                closedList.Add(currentPosition);
            }
            return output;
        }

        /// <summary>
        /// Finds the sghortest path from start to target positions using A* search algorithm
        /// https://www.geeksforgeeks.org/a-search-algorithm/
        /// </summary>
        /// <param name="start"></param>
        /// <param name="target"></param>
        public MapNode FindPathTo(Position start, Position target)
        {
            HashSet<MapNode> openList = new HashSet<MapNode>() {new MapNode(start)};
            HashSet<MapNode> closedList = new HashSet<MapNode>();

            while (openList.Count > 0)
            {
                // get the node with the shortest distance
                var currentPosition = openList.OrderBy(x => x.TotalDistance).First();
                openList.Remove(currentPosition);

                // Console.WriteLine($"Checking {currentPosition}");

                var neighbors = GetAvailableNeighbors(currentPosition)
                    .Select(x => new MapNode(x, currentPosition, currentPosition.DistanceFromStart + 1)).ToList();
                
                foreach (var n in neighbors)
                {
                    n.CalculatedDistanceToTarget = n.DistanceTo(target);
                    if (n.X == target.X && n.Y == target.Y)
                    {
                        return n;
                    }

                    // Check if node already in open list with a lower TotalDistance
                    if (openList.Any(x => x.X == n.X && x.Y == n.Y && x.TotalDistance <= n.TotalDistance))
                    {
                        // Already got a shorter path to this position
                        continue;
                    }

                    // Check if node with same position in closedList with lower distance (ie. we've already found a faster way to this position)
                    // Check if node already in open list with a lower TotalDistance
                    if (closedList.Any(x => x.X == n.X && x.Y == n.Y && x.TotalDistance <= n.TotalDistance))
                    {
                        // Already got a shorter path to this position
                        continue;
                    }

                    openList.Add(n);
                }

                closedList.Add(currentPosition);
            }

            return null; // no destination found
        }

        /// <summary>
        /// Returns neighboring rooms.
        /// We could return the poistion of the doors (as they are technically adjourning)
        /// but faster to skip search step of a position that will only ever have one exit.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public override IEnumerable<Position> GetAvailableNeighbors(Position position)
        {
            var result = new HashSet<Position>();
            if (this[position] != MapTile.Room) return result; 

            // Get neighboring doors
            var neighbors = position.GetNeighboringPositions()
                .Where(x => this[x] == MapTile.DoorHoziontal || this[x] == MapTile.DoorVertical);

            // We don't want the location of the door - we want the room AFTER the door.  So move another square in that direction
            foreach (var door in neighbors)
            {
                Direction direction = position.FindDirection(door);
                result.Add(door.Move(direction));
            }
            return result;
        }

        // easy way to tell if its a move command, or a special character
        private readonly Dictionary<char, CompassDirection> _validDirections = new Dictionary<char, CompassDirection>()
        {
            {'N', CompassDirection.North},
            {'S', CompassDirection.South},
            {'W', CompassDirection.West},
            {'E', CompassDirection.East},
        };

        // Moves current mapping position in the specified direction.
        // Adding the door and any adjoining walls to the map
        private Position Move(CompassDirection direction, Position position)
        {
            Position doorPosition = position.Move(direction);

            // Add door and 2 adjoining wall tiles
            if (direction == CompassDirection.East || direction == CompassDirection.West)
            {
                this.Add(doorPosition, MapTile.DoorVertical);
                this.Add(doorPosition.Move(CompassDirection.North), MapTile.Wall);
                this.Add(doorPosition.Move(CompassDirection.South), MapTile.Wall);
            }
            else
            {
                this.Add(doorPosition, MapTile.DoorHoziontal);
                this.Add(doorPosition.Move(CompassDirection.East), MapTile.Wall);
                this.Add(doorPosition.Move(CompassDirection.West), MapTile.Wall);
            }

            var finalPosition = doorPosition.Move(direction);
            this.Add(finalPosition, MapTile.Room);

            return finalPosition;
        }


        protected override char? ConvertValueToChar(Position position, MapTile value)
        {
            if (position.Equals(_start))
            {
                return 'X';
            }

            return (char) value;
        }
    }
}