using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day17
{

    public sealed class FloodMap : EnumCharMap<FloodTile>
    {
        private readonly string regexPattern = @"^\s*(?<axis>[xy])=(?<start>\d+)\.*(?<end>\d*)\s*$";

        public int WaterTiles => Map.Count(x => (x.Value == FloodTile.WaterFalling || x.Value == FloodTile.WaterResting) && x.Key.Y >= minYBeforeSource);
        public int RestingWaterTiles => Map.Values.Count(x => x == FloodTile.WaterResting);
        
        private readonly int minYBeforeSource;


        public FloodMap(IEnumerable<string> input, Position spring) : base(FloodTile.Sand)
        {
            LoadData(input);
            minYBeforeSource = MinY;  // Get this before we add the spring value

            Add(spring, FloodTile.Spring);
        }
        
        // Pour water downwards until it hits clay - or other water
        public void PourWater(Position startPosition)
        {
            startPosition = startPosition.Move(Direction.Down);
            // use Stack for BFS
            Queue<Position> waterSources = new Queue<Position>();
            waterSources.Enqueue(startPosition);

            HashSet<Position> checkedSources = new HashSet<Position>();  // holds all checked sources - prevents us from simulating them twice
            

            int maxY = MaxY; // cache this as it shouldn't change

            while (waterSources.Count > 0)
            {
                // Console.WriteLine($"Pouring water from: {startPositon}");
                Position pos = waterSources.Dequeue();

                if (!checkedSources.Add(pos)) continue; // check if we've already processed this water source.

                Position next = pos.Move(Direction.Down);
                
                FloodTile nextTile = this[next];

                // Keep water falling until it hits a non-sand tile
                // Can also fall through existing resting water.
                // Doesn't matter if its water
                while (nextTile == FloodTile.Sand)
                {
                    if (pos.Y > maxY) break; // reached bottom of the map so no point continuing

                    // nothing below us - so fill with water
                    this[pos] = FloodTile.WaterFalling;

                    pos = next;
                    next = pos.Move(Direction.Down);
                    nextTile = this[next];
                }

                if (nextTile == FloodTile.WaterFalling)
                {
                    this[pos] = FloodTile.WaterFalling;
                }

                // If we hit clay then we've found an empty bucket.
                // If its resting water - then we've found an existing full bucket.  We can then start filling it again from current position
                // (we know there's no hole below current level or it would be WaterFalling rather than WaterResting)
                if (nextTile == FloodTile.Clay || nextTile == FloodTile.WaterResting)
                {
                    // fill the new bucket - and then try to get 
                    var newSources = FillBucket(pos);
                    foreach (Position p in newSources)
                    {

                        waterSources.Enqueue(p);
                    }
                }
            }
        }

        // Floods water left and right until the level is full of water
        // If there's hole(s) at the bottom - return locations of new water sources.
        // Otherwise keep filling upwards until we overflow the bucket.

        private ICollection<Position> FillBucket(Position startPostion)
        {
            
            // Console.WriteLine($"Filling from {startPostion}");
            HashSet<Position> fallPositions = new HashSet<Position>();  // Set of positions where the water starts falling again.
            HashSet<Position> fillList = new HashSet<Position>();

            Queue<Position> queue = new Queue<Position>();
            queue.Enqueue(startPostion);

            while (queue.Count > 0)
            {
                Position current = queue.Dequeue();
                FloodTile currentTile = this[current];

                // Only do something if the position is not a clay tile
                if (currentTile != FloodTile.Clay)
                {
                    // have we already checked this tile?
                    if (fillList.Add(current))
                    {
                        // Check cell below for 'hole'
                        Position below = current.Move(Direction.Down);
                        if (this[below] == FloodTile.Sand)
                        {
                            fallPositions.Add(current);
                        }
                        else
                        {
                            // move to neighboring positions
                            // Get left and right positions
                            queue.Enqueue(current.Move(Direction.Left));
                            queue.Enqueue(current.Move(Direction.Right));
                        }
                    }
                }
            }

            // Paint the current level with water.
            FloodTile fillType = fallPositions.Count >0 ? FloodTile.WaterFalling :  FloodTile.WaterResting;

            // Paint this layer
            foreach (var pos in fillList)
            {
                this[pos] = fillType;
            }


            // if we've got at least one position where water falls
            // this layer must be set as [WaterFalling] and we can fall off each of the found locations
            if (fallPositions.Count > 0)
            {
                return fallPositions;
            }
            else
            {
                // Fill next row up
                return FillBucket(startPostion.Move(Direction.Up));
            }
        }


        public override string DrawMap(int padding = 0)
        {
            MapBoundary b = GetMapBoundaries(padding);

            StringBuilder map = new StringBuilder();
            for (int y = b.MinY ; y <= b.MaxY ; y++)
            {
                map.Append(Environment.NewLine);
                map.Append($"{y:D4}");

                for (int x = b.MinX; x <= b.MaxX; x++)
                {
                    map.Append((char) this[x,y]);
                }
            }

            return map.ToString();
        }        


        #region Load input Data & create initial map
        public override void LoadData(IEnumerable<string> input)
        {
            foreach (var line in input)
            {
                LoadInputRange(line);
            }
        }

     

        // Loads one line of the input.
        // Splits into 2 ranges - one per axis and iterates over them to add the clay tiles
        private void LoadInputRange(string input)
        {
            // Split into the 2 ranges
            var split = input.Split(",").Select(s => s.Trim()).ToList();

            var ranges = new HashSet<MapRange>
            {
                LoadMapRange(split[0]), 
                LoadMapRange(split[1])
            };


            var xRange = ranges.First(x => x.Axis == Axis.X);
            var yRange = ranges.First(x => x.Axis == Axis.Y);

            foreach (var x in xRange)
            {
                foreach (var y in yRange)
                {
                    this.Add(new Position(x,y), FloodTile.Clay);
                }
            }
        }

        // Breaks down an input string of format '<xy>=<start>...<end> into a range and which axis it applies to
        private MapRange LoadMapRange(string input)
        {
            input = input.Trim();

            var match = Regex.Match(input, regexPattern);

            Axis axis = (Axis) match.Groups["axis"].Value[0];
            int startRange = int.Parse(match.Groups["start"].Value);
            int endRange = startRange;

            var endMatch = match.Groups["end"];

            // We may not have an end range - in which case we simply use the start for a range of 1 item
            if (endMatch.Success && !string.IsNullOrEmpty(endMatch.Value))
            {
                endRange = int.Parse(endMatch.Value);
            }

            
            return new MapRange(axis, startRange, endRange);
        }

        private class MapRange : IEnumerable<int>
        {
            private readonly int _endIndex;
            private readonly int _startIndex;

            public readonly Axis Axis;

            public MapRange(Axis axis, int start, int end)
            {
                _startIndex = start;
                _endIndex = end;
                Axis = axis;
            }

            public IEnumerator<int> GetEnumerator()
            {
                for (int i = _startIndex; i <= _endIndex; i++)
                {
                    yield return i;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private enum Axis
        {
            X = 'x',
            Y = 'y'
        }
        #endregion
    }
}
