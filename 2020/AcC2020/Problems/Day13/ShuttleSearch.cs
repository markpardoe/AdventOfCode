using System;
using System.Collections.Generic;
using System.Linq;
using AoC.Common;

namespace AoC.AoC2020.Problems.Day13
{
    public class ShuttleSearch : AoCSolution<long>
    {
        public override int Year => 2020;
        public override int Day => 13;
        public override string Name => "Day 13: Shuttle Search";
        public override string InputFileName => "Day13.txt";

        public override IEnumerable<long> Solve(IEnumerable<string> input)
        {
            var data = input.ToList();
            long departureTime = long.Parse(data[0]);

            var splitInput = data[1].Split(',');
            var busTimes = new List<BusTime>();

            for (int i = 0; i < splitInput.Length; i++)
            {
                if (splitInput[i] != "x")
                {
                    busTimes.Add(new BusTime(int.Parse(splitInput[i]), i));
                }
            }

            yield return FindNextBus(departureTime, busTimes);
            yield return CalculatePart2(busTimes);
        }

        /// Finds the bus with the minimum wait time at the current stop.
        /// Returns busId * wait time (in min.)
        public long FindNextBus(long departTime, IEnumerable<BusTime> busTimes)
        {
            long minWait = long.MaxValue;
            BusTime earliestBus = null;

            foreach (var bus in busTimes)
            {
                long wait =  (((long) Math.Ceiling(departTime / (double) bus.Id)) * bus.Id) - departTime;

                Console.WriteLine($"Bus {bus} = {wait}");

                if (wait < minWait)
                {
                    minWait = wait;
                    earliestBus =  bus;
                }
            }

            return earliestBus.Id * minWait;
        }

        // Solution for part 2.
        // A brute-force would time out so we have to look for patterns in the output.
        // Each bus has a period (the Id) - how long one complete circuit takes. 
        // If we know a bus is in the right positon at time X, then its also in the right position at (X + <period>) and (X + <period * 2>), etc..
        // So for 3 buses (7,0), (13,1), (19,7):
        // The first bus will end at intervals of 7 - so we need to find a multiple of 7 that also works for the 2nd bus = 77.
        // Now we have a period 93 (7*3) [this is the LCM - since both primes we only have to multiple] and an offset (start) of 77.
        // So increment 93 by 77 until we have a match for the 4th bus, and so on...
        public long CalculatePart2(IEnumerable<BusTime> buses)
        {
            var busList = (buses.OrderBy(x => x.Id).ToList());

            long time = 0;      // keep track of the time to start searching for
            long previous = 1;

            foreach (BusTime bus in busList)
            {
                time = FindTimestamp(time, previous, bus);
                previous = previous * bus.Id;
            }

            return time;
        }

        // Returns the next time where the bus arrives [offset] minutes after the 1st bus
        private long FindTimestamp(long timestamp, long skip, BusTime bus)
        {
            long time = timestamp;  // Continue from the last correct time
          
            // keep checking until we get a match for this bus.
            // This will also be a match for all the previous buses as well
            while (!bus.IsCorrectTimestamp(time))
            {
                time += skip;
            }

            Console.WriteLine($"{bus} = {time}");
            return time;
        }

    }

    public class BusTime
    {
        public long Id { get; }  // Period to do one complete cycle of destinations
        public long Offset { get; }  // Minutes after 1st bus it should show up

        public BusTime(long id, long offset)
        {
            Id = id;
            Offset = offset;
        }

        public override string ToString()
        {
            return $"[{Id}] ({Offset})";
        }

        // At the given timestamp - is the bus <offset> minutes from the start
        public bool IsCorrectTimestamp(long timestamp)
        {
            bool result =  (timestamp + Offset ) % Id == 0;
            return result;
        }

        public long GetWaitTime(long timeStamp)
        {
            return (((long)Math.Ceiling(timeStamp / (double)Id)) * Id) - timeStamp;
        }
    }
}