using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private readonly Position _startPositon = new Position(0,0);


        public RoomMap(string input) : base(MapTile.Unknown)
        {
           var inputData = input.Skip(1).ToList();
           GenerateMap(_startPositon, inputData);
        }


        private void GenerateMap(Position currentPosition, List<char> inputData)
        {
            List<IPath> paths = new List<IPath>();

            List<CompassDirection> directions = new List<CompassDirection>();
           
            for (int i = 0; i < inputData.Count; i++)
            {
                char current = inputData[i];

                if (_validDirections.Contains(current))
                {
                    directions.Add((CompassDirection) current);
                }
                else if (current == '(')
                {
                    paths.Add(new Path(directions));
                    directions.Clear();

                    //i = GenerateMap(currentPosition, index);
                    // start of a child group - so drop down a level
                }
            }
        }

        private int FindEndBracket(List<char> search, int startIndex)
        {
            if (search[startIndex] != '(') throw new ArgumentException();
            int bracketLevel = 1;

            for (int i = startIndex + 1; i <= search.Count; i++)
            {
                char current = search[i];

                if (current == ')')
                {
                    bracketLevel--;
                    if (bracketLevel == 0)
                    {
                        return i;   // found the close bracket
                    }
                }
                else if (current == '(')
                {
                    bracketLevel++;
                }
            }

            throw new InvalidDataException("No close bracket found"); // end of the list - no bracket found
        }

       

        // easy way to tell if its a move command, or a special character
        private readonly HashSet<char> _validDirections = new HashSet<char>() {'N', 'S', 'E', 'W'};

        // Moves current mapping position in the specified direction.
        // Adding the door and any adjoining walls
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

            var finalPostion = doorPosition.Move(direction);
            this.Add(finalPostion, MapTile.Room);

            return finalPostion;
        }
        

        public override string DrawMap()
        {
            int minX = MinX;
            int minY = MinY;
            int maxX = MaxX;
            int maxY = MaxY;

            StringBuilder map = new StringBuilder();
            for (int y = minY; y <= maxY; y++)
            {
                map.Append(Environment.NewLine);

                for (int x = minX; x <= maxX; x++)
                {
                    if (_startPositon.X == x && _startPositon.Y == y)
                    {
                        // Draw current location on map
                        map.Append('X');
                    }
                    else
                    {
                        var c = (MapTile) this[x, y];
                        map.Append((char) c);
                    }
                }
            }

            return map.ToString();
        }
    }
}