using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using AoC.Common;
using AoC.Common.DataStructures;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day06
{
    public class ChronalCoordinates : AoCSolution<long>
    {
        public override int Year => 2018;
        public override int Day => 6;
        public override string Name => "Day 6: Chronal Coordinates";
        public override string InputFileName => "Day06.txt";

        public override IEnumerable<long> Solve(IEnumerable<string> input)
        {
            ICollection<Position> positions = GenerateCoOrdinates(input);
          
            var result = GetLargestBoundedArea(positions);
            yield return result;
            yield return FindSizeOfSafeRegion(positions, 10000);

        }

        // Convert input into collection of Positions
        private ICollection<Position> GenerateCoOrdinates(IEnumerable<string> input)
        {
            HashSet<Position> positions = new HashSet<Position>();
            foreach (var line in input)
            {
                var splitLine = line.Split(", ", StringSplitOptions.RemoveEmptyEntries);
                positions.Add(new Position(int.Parse(splitLine[0]), int.Parse(splitLine[1])));
            }

            return positions;
        }


        /// <summary>
        /// Find the larges area (number of co-ordinates) around the co-ordinates
        /// where the area isn't infinite.
        /// 
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        public long GetLargestBoundedArea(ICollection<Position> positions)
        {
            // Count squares per starting area
            ItemCounter<Position> area = new ItemCounter<Position>();


            int maxX = positions.Max(p => p.X);
            int maxY = positions.Max(p => p.Y);
            int minX = positions.Min(p => p.X);
            int minY = positions.Min(p => p.Y);

            // For every position in the search space - find which location its closest to.
            for (int x = minX; x <= maxX; x++)
            {
                
                for (int y = minY; y <= maxY; y++)
                {
                    int minDistance = int.MaxValue;

                    // Store nearest location to the position.
                    // There may be a tie - in which case the position isn't counted as part of a region
                    HashSet<Position> shortest = new HashSet<Position>();

                    foreach (Position p in positions)
                    {
                        int distance = p.DistanceTo(x, y);
                        if (distance < minDistance)
                        {
                            shortest = new HashSet<Position> {p};
                            minDistance = distance;
                        }
                        else if (distance == minDistance)
                        {
                            shortest.Add(p);
                        }
                    }

                    // Only count the position when one distinct closest value.
                    if (shortest.Count == 1)
                    {
                        area.Add(shortest.First());
                    }
                }
            }

            // Assume that infinite areas are created by locations that are on the edge of the space
            // and remove them from the count
            var remove = positions.Where(p => p.X == maxX || p.X == minX || p.Y == maxX || p.Y == minY);

            foreach (var p in remove)
            {
                area.RemoveKey(p);
            }

            return area.Values.Max();
        }

        // Get the number of positions that are within <safeDistance> of the Sum(distance to each location)
        // Uses a flood fill (BFS) search to find all valid positions.
        public long FindSizeOfSafeRegion(ICollection<Position> locations, int safeDistance)
        {
            long count = 0;  // initial position must be in safe range

            // Do a flood fill (to safeDistance) from known good point(s)
            // Start by using the known co-ordinates we're given in input.
            // Hopefully at least one will be a valid start point!
            HashSet<Position> checkedPositions = new HashSet<Position>(locations);
            Queue<Position> toCheck = new Queue<Position>();

            // not all of the initial locations will be in range...
            // so only add the valid ones to the queue.
            foreach (var location in locations)
            {
                if (InRange(location, locations, safeDistance))
                {
                    toCheck.Enqueue(location);
                    count++;
                }
            }

            // BFS
            while (toCheck.Count > 0)
            {
                Position pos = toCheck.Dequeue();
                
                var neighbors = pos.GetNeighbouringPositions();

                foreach (var n in neighbors)
                {
                    if (checkedPositions.Add(n))
                    {
                        // not already checked 
                        if (InRange(n, locations, safeDistance))
                        {
                            toCheck.Enqueue(n);
                            count++;
                        }
                    }
                }
            }
            
            return count;
        }

        private bool InRange(Position position, ICollection<Position> locations, int safeDistance)
        {
            int total = 0;
            foreach (var location in locations)
            {
                total += position.DistanceTo(location);

                if (total >= safeDistance) return false;
            }

            return total < safeDistance;
        }

        public List<string> Example1 = new List<string>
        {
            "1, 1",
            "1, 6",
            "8, 3",
            "3, 4",
            "5, 5",
            "8, 9"
        };
    }

  
}
