﻿using AoC.Common;
using AoC.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day03
{

    public class Day03_Solution : AoCSolution<int>
    {
        public override int Year => 2019; 

        public override int Day => 3;

        public override string Name => "Day 3: Crossed Wires";

        public override string InputFileName => "Day03.txt";

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            yield return CalculateDistanceFromOriginAtNearestIntersection(input);
            yield return CalculateCumulativeWireLengthAtFirstIntersection(input);
        }       

        public int CalculateDistanceFromOriginAtNearestIntersection(IEnumerable<string> data)
        {
            return CalculateDistance(data.ToList(), DistanceFromOrigin);
        }

        public int CalculateCumulativeWireLengthAtFirstIntersection(IEnumerable<string> data)
        {
            return CalculateDistance(data.ToList(), CumulativeWireLength);
        }

        private int DistanceFromOrigin(LineWithDistance line1, LineWithDistance line2, Position collision)
        {
            return collision.DistanceFromOrigin();
        }

        private int CumulativeWireLength(LineWithDistance line1, LineWithDistance line2, Position collision)
        {
            return line1.TotalDistance + line2.TotalDistance + collision.DistanceTo(line1.StartPoint) + collision.DistanceTo(line2.StartPoint);
        }

        private int CalculateDistance(List<string> data, Func<LineWithDistance, LineWithDistance, Position, int> distanceCalculator)
        {
            var wire1Path = ChartLine(new Position(0, 0), ParseInput(data[0]));
            var wire2Path = ChartLine(new Position(0, 0), ParseInput(data[1]));
            int minDistance = Int32.MaxValue;

            foreach (LineWithDistance line1 in wire1Path)
            {
                foreach (LineWithDistance line2 in wire2Path)
                {
                    Position? collide = line1.GetCollision(line2);
                    if (collide.HasValue)
                    {
                        if (collide.Value.X != 0 || collide.Value.Y != 0)  // ignore the start position
                        {
                            int distance = distanceCalculator(line1, line2, collide.Value);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                            }
                        }
                    }
                }
            }
            return minDistance;
        }

        /// <summary>
        /// Converts input string data into a list of vectors making up the wires' paths.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private List<Vector> ParseInput(string inputdata)
        {
           return inputdata.Split(',').Select(x => new Vector(x)).ToList();  // convert to list of vectors
        }

        // Converts a start position & list of vectors into a List of lines
        // ie. connects up all the vectors into a continuous line
        private List<LineWithDistance> ChartLine(Position start, List<Vector> vectors)
        {
            var path = new List<LineWithDistance>();
            int total = 0;
            Position p = start;
            foreach (var v in vectors)
            {
                var line = new LineWithDistance(p, v, total);
                path.Add(line);
                total += line.Distance;
                p = line.EndPoint;
            }
            return path;
        }       
    }        
}