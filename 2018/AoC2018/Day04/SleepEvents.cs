using System;
using System.Collections.Generic;
using System.Text;

namespace Aoc.Aoc2018.Day04
{
    public class SleepEvent
    {
        public int GuardId { get; }
        public DateTime ShiftDate { get; }

        public int FallsAsleep { get; }
        public int WakesUp { get; }
        public int MinutesSlept => WakesUp - FallsAsleep;

        public SleepEvent(int id, DateTime sleep, DateTime wake)
        {
            if (sleep > wake)
            {
                throw new InvalidOperationException("Wake up time must be after they fell asleep!");
            }
            GuardId = id;
            ShiftDate = wake.Date;
            FallsAsleep = sleep.Minute;
            WakesUp = wake.Minute;
        }        
    }
}