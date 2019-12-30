using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;

namespace Aoc.Aoc2018.Day04
{
    internal enum GuardEventType
    {
        BeginShift = 0,
        FallAsleep = 1,
        WakeUp = 2
    }
    internal class GuardEvent
    {
        internal GuardEventType EventType { get; }
        internal DateTime EventDate { get; }

        internal int? GuardId { get; }

        public GuardEvent(string inputDate)
        {
            string[] splitData = inputDate.Split("]");

            string ev = splitData[1].Trim();

            if (ev[0] == 'G')
            {
                EventType = GuardEventType.BeginShift;
                var Id = ev.Split(' ');  // split it by string
                GuardId = Int32.Parse(Id[1].Trim().Remove(0, 1));
            }
            else if (ev[0] == 'f')
            {
                EventType = GuardEventType.FallAsleep;
            }
            else if (ev[0] == 'w')
            {
                EventType = GuardEventType.WakeUp;
            }
            else
            {
                throw new InvalidOperationException("Invalid Event: " + ev);
            }

            string dte = splitData[0].Trim().Substring(1);  // Trim and drop first character
            dte = dte.Replace(" - ", "");
            dte = dte.Replace("-", "");
            EventDate = DateTime.ParseExact(dte, "yyyyMMdd HH:mm", CultureInfo.InvariantCulture );

        }
    }
}
