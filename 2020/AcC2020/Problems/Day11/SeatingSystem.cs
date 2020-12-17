using AoC.Common;
using System;
using System.Collections.Generic;

namespace AoC.AoC2020.Problems.Day11
{
    public class SeatingSystem :AoCSolution<int>
    {
        public override int Year => 2020;
        public override int Day => 11;
        public override string Name => "Day 11: Seating System";
        public override string InputFileName => "Day11.txt";

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            SeatMap map = new SeatMap(input, 4);
            yield return CountOccupiedSeatsWhenStable(map, map.RunSimpleSimulationTurn);


            SeatMap map2 = new SeatMap(input, 5);
            yield return CountOccupiedSeatsWhenStable(map2, map2.RunComplexSimulationTurn);
        }

        /// <summary>
        /// Counts the number of seats occupied when the simulation becomes stable.
        /// ie. no seats are flipped when the map is updated.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int CountOccupiedSeatsWhenStable(SeatMap map, Action action)
        {
            string currentStatus = map.StatusCode;
            string oldStatus = string.Empty;
            int turn = 0;

            while (!oldStatus.Equals(currentStatus))
            {
                action();
                turn++;
                oldStatus = currentStatus;
                currentStatus = map.StatusCode;
            }

            return map.OccupiedSeats;
        }
    }
}