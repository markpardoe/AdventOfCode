using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.Aoc2018.Day04
{
    public class Day04_Solution : AoC2018Solution
    {
        public override string URL => @"https://adventofcode.com/2018/day/4";

        public override int Day => 4;

        public override string Name => "Day 4: Repose Record";

        public override string InputFileName => "Day04.txt";

        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {
            yield return Strategy1(input).ToString();
            yield return Strategy2(input).ToString();
        }
        
        
        public int Strategy1(IEnumerable<string> input)
        {
            HashSet<SleepEvent> sleep = ParseInputs(input);

            // Find the guard with the most total sleep
            var guardWithMostSleep = sleep.GroupBy(
                p => p.GuardId
                , p => p.MinutesSlept
                , (key, g) => new { GuardId = key, TotalSleep = g.ToList().Sum() })
                .OrderByDescending(p => p.TotalSleep)
                .First();

            int mostSleep = guardWithMostSleep.TotalSleep;
            int guardId = guardWithMostSleep.GuardId;

            var mm = FindMostSleepMinute(sleep.Where(g => g.GuardId == guardId));
            return mm.Item1 * guardId;

        }

        public int Strategy2(IEnumerable<string> input)
        {
            HashSet<SleepEvent> sleep = ParseInputs(input);

            var guards = sleep.Select(s => s.GuardId).Distinct();

            int maxGuardId = 0;
            int maxMinute = 0;
            int maxSleep = 0;
            foreach (int guardId in guards)
            {
                var sleepMinutes = FindMostSleepMinute(sleep.Where(s => s.GuardId == guardId));
                if (sleepMinutes.Item2 > maxSleep)
                {
                    maxSleep = sleepMinutes.Item2;
                    maxGuardId = guardId;
                    maxMinute = sleepMinutes.Item1;
                }
            }

            return maxGuardId * maxMinute;
        }

        private HashSet<SleepEvent> ParseInputs(IEnumerable<string> input)
        {
            HashSet<SleepEvent> sleepEvents = new HashSet<SleepEvent>();
            List<GuardEvent> events = input.Select(s => new GuardEvent(s)).OrderBy(e => e.EventDate).ToList();

            int GuardId = 0;
            for (int i = 0; i < events.Count; i++)
            {
                GuardEvent ev = events[i];

                if (ev.EventType == GuardEventType.BeginShift)
                {
                    GuardId = ev.GuardId.Value;
                    continue;
                }
                else if (ev.EventType == GuardEventType.FallAsleep)
                {
                    sleepEvents.Add(new SleepEvent(GuardId, ev.EventDate, events[i + 1].EventDate));
                    i++;  // skip next record
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            return sleepEvents;
        }

        private Tuple<int, int> FindMostSleepMinute(IEnumerable<SleepEvent> sleepEvents)  // Returnns <minute, NumOftimesAsleep>
        {
            Dictionary<int, int> sleepCount = new Dictionary<int, int>();
            for (int i=0;i<=60;i++)
            {
                sleepCount.Add(i, 0);
            }

            foreach(SleepEvent ev in sleepEvents)
            {
                for (int i = ev.FallsAsleep; i < ev.WakesUp; i++)
                {
                    sleepCount[i]++;
                }
            }

            int maxSleep = 0;
            int maxMin = 0;

            foreach (int min in sleepCount.Keys)
            {
                if (sleepCount[min] > maxSleep)
                {
                    maxSleep = sleepCount[min];
                    maxMin = min;
                }
            }

            return new Tuple<int, int>(maxMin, maxSleep);
        }



    }
}
