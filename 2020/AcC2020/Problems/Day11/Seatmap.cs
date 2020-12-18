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
    }


    /// <summary>
    /// Seat layout map
    /// Hold positions of seats in a 2d space.
    /// </summary>
    public class SeatMap : Map<SeatType>
    {
        // Cached values for a non-expanding grid
        public override int MaxX { get; }
        public override int MaxY { get; }

        public override int MinX => 0;
        public override int MinY => 0;


        // List of all seats (ie. not floor) in the map.
        // Use a list rather than a hashset as we need to preserve the order of the seats to generate a code for checking statuses.
        // By only listing seats (eg. "###LL##") and ignoring spaces this should be smaller and faster.
        private readonly List<Position> seats = new List<Position>();
        private readonly int _seatLimit;

        // List of seats as a string.  Useful for comparing states.
        public string StatusCode => string.Join("", seats.Select(x =>(char)  Map[x]));

        public int OccupiedSeats => CountValue(SeatType.Occupied);

        public SeatMap(IEnumerable<string> input, int seatLimit) : base(SeatType.Floor)
        {
            _seatLimit = seatLimit;
            var y = 0;
            foreach (var line in input)
            {
                MaxX = line.Length;
                for (var x = 0; x < line.Length; x++)
                {
                    
                    var tile = (SeatType)line[x];
                    Position pos = new Position(x, y);

                    Add(pos, tile);

                    if (tile == SeatType.Occupied || tile== SeatType.Empty)
                    {
                        seats.Add(pos);
                    }
                }
                y++;
            }

            MaxY = y-1;

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
                int occupiedNeighbours = seat.GetNeighboringPositionsIncludingDiagonals().Select(s => this[s]).Count(x => x == SeatType.Occupied);
                AddToBuffer(seat, CalculateNewStatus(Map[seat], occupiedNeighbours));
            }
             UpdateFromBuffer();
        }

        // We flip all seats simultaneously so we can't update the status 
        // until we've worked out the new status of every seat.
        // So store the new value in a buffer [NewStatus] and then update every seat at the same time.
        public SeatType CalculateNewStatus(SeatType seat, int occupiedSeats)
        {
            if (seat == SeatType.Floor) return SeatType.Floor;  // do nothing

            if (seat == SeatType.Empty && occupiedSeats == 0)
            {
               return SeatType.Occupied;
            }
            else if (seat == SeatType.Occupied && occupiedSeats >= _seatLimit)
            {
                return  SeatType.Empty;
            }
            else
            {
                return seat;
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
            foreach (var seat  in seats)
            {

                int occupiedNeighbours =
                    (IsVisibleOccupiedSeatsFromPosition(seat, -1, 0) ? 1 : 0) +
                    (IsVisibleOccupiedSeatsFromPosition(seat, 1, 0) ? 1 : 0) +
                    (IsVisibleOccupiedSeatsFromPosition(seat, -1, 1) ? 1 : 0) +
                    (IsVisibleOccupiedSeatsFromPosition(seat, 1, 1) ? 1 : 0) +
                    (IsVisibleOccupiedSeatsFromPosition(seat, -1, -1) ? 1 : 0) +
                    (IsVisibleOccupiedSeatsFromPosition(seat, 1, -1) ? 1 : 0) +
                    (IsVisibleOccupiedSeatsFromPosition(seat, 0, -1) ? 1 : 0) +
                    (IsVisibleOccupiedSeatsFromPosition(seat, 0, 1) ? 1 : 0);

               AddToBuffer(seat, CalculateNewStatus(Map[seat], occupiedNeighbours));
            }

            UpdateFromBuffer();
        }

        // Checks if we can see any occupied seats in the direction indicated 
        private bool IsVisibleOccupiedSeatsFromPosition(Position start, int xModifier, int yModifier)
        {
            int x = start.X + xModifier;
            int y = start.Y + yModifier;

            var seat = this[x, y];
            while (x >= MinX && x <= MaxX && y >= MinY && y <= MaxY)
            {
                if (seat == SeatType.Occupied)
                {
                    return true;
                }

                if (seat == SeatType.Empty)
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
