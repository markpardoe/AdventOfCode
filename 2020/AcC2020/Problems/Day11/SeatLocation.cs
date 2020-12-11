using System.Collections.Generic;
using System.Linq;
using AoC.Common.Mapping;

namespace AoC.AoC2020.Problems.Day11
{

    public class SeatLocation
    {
        public Position Position { get; }
        public SeatType Status { get; private set; }
        public bool IsOccupied => Status == SeatType.Occupied;
        public bool IsEmpty => Status == SeatType.Empty;
        public bool IsFloor => Status == SeatType.Floor;
        public bool IsSeat => Status != SeatType.Floor;

        public int X => Position.X;
        public int Y => Position.Y;

        private SeatType _newStatus;

        private int _seatLimit;  // how many occupied seats it can see before flipping from occupied to empty

        public SeatLocation(Position position, SeatType type, int limit)
        {
            Status = type;
            _newStatus = type;
            Position = position;
            _seatLimit = limit;
        }

        // We flip all seats simultaneously so we can't update the status 
        // until we've worked out the new status of every seat.
        // So store the new value in a buffer [NewStatus] and then update every seat at the same time.
        public void CalculateNewStatus(int occupiedSeats)
        {
            if (Status == SeatType.Floor) return;  // do nothing

            if (Status == SeatType.Empty && occupiedSeats == 0)
            {
                _newStatus = SeatType.Occupied;
            }
            else if (Status == SeatType.Occupied && occupiedSeats >= _seatLimit)
            {
                _newStatus = SeatType.Empty;
            }
            else
            {
                _newStatus = Status;
            }
        }
        
        public override string ToString()
        {
            return ((char) Status).ToString();
        }

        public void UpdateSeat()
        {
            Status = _newStatus;
        }
    }
}