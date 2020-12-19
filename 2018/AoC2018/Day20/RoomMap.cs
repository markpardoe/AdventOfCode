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
    

    public class RoomMap : Map<MapTile>
    {
        private readonly Position _start = new Position(0,0);


        public RoomMap(string input) : base(MapTile.Unknown)
        {
           var inputData = input.Skip(1).ToList();

           var start = new HashSet<Position>() {_start};
           var data = new Queue<char>(input.ToCharArray());
           BuildMap(start, data);

           // replace unknown with walls
           foreach (var p in GetBoundedEnumerator())
           {
               if (p.Value == MapTile.Unknown)
               {
                   this[p.Key] = MapTile.Wall;
               }
           }
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
                    finalPositions.UnionWith(currentPositions);
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
                    // ignore as this indicates start or end of RegEx
                    continue;
                }
                else
                {
                    throw new DataException($"Invalid token: {token}");
                }
            }

            return finalPositions;
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