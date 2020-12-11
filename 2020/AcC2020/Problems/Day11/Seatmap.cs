using System;
using System.Collections.Generic;
using System.Text;
using AoC.Common.Mapping;
using System.Linq;

namespace AoC.AoC2020.Problems.Day11
{

    public enum SeatType
    {
        Floor = '.',
        Occupied = '#',
        Empty = 'L',
        Unknown = ' '
    }


    /// <summary>
    /// Seat layout map
    /// Hold positions of seats in a 2d space.
    /// </summary>
    public class SeatMap : Map<SeatLocation>
    {
        // List of all seats (ie. not floor) in the map.
        // USe a list rather than a hashset as we need to preserve the order of the seats to generate a code for checking statuses.
        // By only listing seats (eg. "###LL##") and ignoring spaces this should be smaller and faster.
        private readonly List<SeatLocation> seats = new List<SeatLocation>();

        // List of seats as a string.  Useful for comparing states.
        public string StatusCode => string.Join("", seats);
        public int OccupiedSeats => seats.Count(s => s.IsOccupied);

        public SeatMap(IEnumerable<string> input, int seatLimit) : base(null)
        {
            var y = 0;
            foreach (var line in input)
            {
                for (var x = 0; x < line.Length; x++)
                {

                    var tile = (SeatType)line[x];
                    Position pos = new Position(x, y);

                    SeatLocation seat = new SeatLocation(pos, tile, seatLimit);
                    Add(pos, seat);

                    if (seat.IsSeat)
                    {
                        seats.Add(seat);
                    }
                }

                y++;
            }
        }

        /// <summary>
        /// Runs 1 turn of the seating simulation.
        /// Checks the 8 neighboring seats for each seat and updates its status.
        /// All updates are run simultaneously.
        /// </summary>
        public void RunSimpleSimulationTurn()
        {
            // Calculate the new status for each location and buffer it.
            foreach (var seat in seats)
            {
                int occupiedNeighbours = seat.Position.GetNeighboringPositionsIncludingDiagonals().Select(s => this[s]).Count(x => x != null && x.IsOccupied);
                seat.CalculateNewStatus(occupiedNeighbours);
            }

            // Update seat value from buffer
            foreach (var seat in seats)
            {
                seat.UpdateSeat();
            }
        }

        /// <summary>
        /// Runs 1 turn of the seating simulation.
        /// Checks how many seats it can see in each direction ( 8 including diagonals)
        /// All updates are run simultaneously.
        /// </summary>
        public void RunComplexSimulationTurn()
        {
            // Calculate the new status for each location and buffer it.
            foreach (var seat in seats)
            {
                int occupiedNeighbours =
                    (IsVisibleOccupiedSeatsFromPosition(seat.Position, -1, 0) ? 1 : 0) +
                    (IsVisibleOccupiedSeatsFromPosition(seat.Position, 1, 0) ? 1 : 0) +
                    (IsVisibleOccupiedSeatsFromPosition(seat.Position, -1, 1) ? 1 : 0) +
                    (IsVisibleOccupiedSeatsFromPosition(seat.Position, 1, 1) ? 1 : 0) +
                    (IsVisibleOccupiedSeatsFromPosition(seat.Position, -1, -1) ? 1 : 0) +
                    (IsVisibleOccupiedSeatsFromPosition(seat.Position, 1, -1) ? 1 : 0) +
                    (IsVisibleOccupiedSeatsFromPosition(seat.Position, 0, -1) ? 1 : 0) +
                    (IsVisibleOccupiedSeatsFromPosition(seat.Position, 0, 1) ? 1 : 0);

                seat.CalculateNewStatus(occupiedNeighbours);
            }

            // Update seat value from buffer
            foreach (var seat in seats)
            {
                seat.UpdateSeat();
            }
        }

        // Checks if we can see any occupied seats in the direction indicated 
        private bool IsVisibleOccupiedSeatsFromPosition(Position start, int xModifier, int yModifier)
        {
            int x = start.X + xModifier;
            int y = start.Y + yModifier;

            SeatLocation seat = this[x, y];

            while (seat != null)
            {
                if (seat.IsOccupied)
                {
                    return true;
                }

                if (seat.IsEmpty)
                {
                    return false;
                }

                x = x + xModifier;
                y = y + yModifier;
                seat = this[x, y];
            }

            return false;
        }

        public override string DrawMap()
        {
            int max_X = MaxX;
            int max_Y = MaxY;

            StringBuilder map = new StringBuilder();
            for (int y = 0; y <= max_Y; y++)
            {
                map.Append(Environment.NewLine);

                for (int x = 0; x <= max_X + 2; x++)
                {
                    map.Append(this[x, y]);
                }
            }
            return map.ToString();
        }
    }
}
