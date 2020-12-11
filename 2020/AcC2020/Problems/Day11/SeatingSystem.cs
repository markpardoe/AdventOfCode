using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;

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
            // Console.WriteLine("Turn " + turn);
            // Console.WriteLine(map.DrawMap());

            while (!oldStatus.Equals(currentStatus))
            {
                action();

                turn++;
                //Console.WriteLine();
                //Console.WriteLine("Turn " + turn);
                //Console.WriteLine(map.DrawMap());

                //Console.ReadLine();

                oldStatus = currentStatus;
                currentStatus = map.StatusCode;
            }

            //Console.WriteLine("Turn " + turn);
            //Console.WriteLine(map.DrawMap());
            return map.OccupiedSeats;
        }


        private readonly IEnumerable<string> example1 = new List<string>
        {
            "L.LL.LL.LL",
            "LLLLLLL.LL",
            "L.L.L..L..",
            "LLLL.LL.LL",
            "L.LL.LL.LL",
            "L.LLLLL.LL",
            "..L.L.....",
            "LLLLLLLLLL",
            "L.LLLLLL.L",
            "L.LLLLL.LL"
        };
    }
}
