using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AoC.Common;

namespace AoC.AoC2020.Problems.Day16
{
    public class TicketTranslation : AoCSolution<long>
    {
        public override int Year => 2020;
        public override int Day => 16;
        public override string Name => "Day 16: Ticket Translation";
        public override string InputFileName => "Day16.txt";

        public override IEnumerable<long> Solve(IEnumerable<string> input)
        {
            LoadTicketData(input.ToList());

            List<List<int>> validTickets = new List<List<int>>();

            int total = 0;
            foreach (var ticket in _tickets)
            {
                var error =  IsTicketValid(ticket);
                if (error.IsValid)
                {
                    validTickets.Add(ticket);
                }
                else
                {
                    total += error.ErrorRate;
                }
            }

            yield return total;

            validTickets.Append(_yourTicket);

            var ticketMap =  MatchFieldToTicketIndex(validTickets);

            var departures = ticketMap
                .Where(x => x.Key.Name.StartsWith("departure"))
                .Select(x => _yourTicket[x.Value]);    


            long totalValue = 1;

            foreach (var v in departures)
            {
                totalValue *= v;
            }
            
            yield return totalValue;

        }

        // Input data
        private readonly List<TicketField> _ticketFields = new List<TicketField>();
        private readonly List<List<int>> _tickets = new List<List<int>>();
        private List<int> _yourTicket = new List<int>();


        // Load the input
        private void LoadTicketData(IReadOnlyList<string> input)
        {
            int index = 0;

            // Load ticket fields from top of input until we hit an empty line
            while (!string.IsNullOrWhiteSpace(input[index]))
            {
                _ticketFields.Add(new TicketField(input[index]));
                index++;
            }
            
            index +=2;  // skip  'your ticket' lines
            _yourTicket = input[index].Split(',').Select(x => int.Parse(x)).ToList();

            index += 3;  // skip to start of actual Tickets after 'your ticket' data.  Skip empty line and 'nearby tickets'

            for (; index < input.Count; index++)
            {
                _tickets.Add(input[index].Split(',').Select(x => int.Parse(x)).ToList());
            }
        }

        // Checks if a ticket is valid.  If not, then it returns the ErrorRate (sum of all invalid numbers)
        // We do it this way as some tickets contain '0' as an invalid value, so we can't just return the ErrorRate and check for zero
        private (bool IsValid, int ErrorRate) IsTicketValid(IEnumerable<int> ticket)
        {
            int total = 0;
            bool result = true;
            // For every number on the ticket - we need to check if it is valid for any of the ticket fields
            foreach (var value in ticket)
            {
                if (!_ticketFields.Any(x => x.IsNumberInValidRange(value)))
                {
                    // value can't be used for any field - so must be invalid.
                    total += value;
                    result = false;
                }
            }
            return (result, total);
        }

        private Dictionary<TicketField, int> MatchFieldToTicketIndex(List<List<int>> tickets)
        {
            // List of which TicketField(s) are valid for each ticket indexes (0 - 20) 
            Dictionary<int, List<TicketField>> ticketFieldIndexes = new Dictionary<int, List<TicketField>>();
            int numIndexes = tickets[0].Count;

            for (int i = 0; i < tickets[0].Count; i++)  
            {
                var fieldValues = tickets.Select(t => t[i]);  // Get the [i]th element from each ticket

                // Get the TicketFields where the range of numbers (for one position on the ticket) all match the rules for a specific field.
                var matchedFields = _ticketFields.Where(f => f.AreNumbersInValidRange(fieldValues));

                ticketFieldIndexes.Add(i, matchedFields.ToList());
            }


            return ReduceIndexToFieldMapping(ticketFieldIndexes);
        }

        private Dictionary<TicketField, int> ReduceIndexToFieldMapping(Dictionary<int, List<TicketField>> map)
        {
            // Holds one-to-one mapping of the index (on a ticket) to the ticketField
            Dictionary<TicketField, int> result = new Dictionary<TicketField, int>();
            int index = map.Keys.Count;

            for (int i = 0; i <index; i++)// loop till all keys matched
            {
                // Find any pairs where the index maps to exactly 1 TicketField.
                // Remove that TicketField from all other entries and find the next index with only 1 match.
                // And so on until all Fields mapped 
                var match = map.First(x => x.Value.Count == 1);

                var ticketField = match.Value.First();  // should be exactly one value
                result.Add(ticketField, match.Key);

                foreach (var ticketFieldList in map.Values)
                {
                    ticketFieldList.RemoveAll(x => x.Name == ticketField.Name);
                }

                map.Remove(match.Key);
            }

            return result;
        }


        private class TicketField
        {
            // RegEx pattern for extracting ranges + names for a ticket field.  
            // We cheat slightly as all inputs have exactly 2 ranges
            private static readonly string RangePattern = @"^(?<name>\D+):\s*(?<AStart>\d+)-(?<AEnd>\d+)\s*or\s*(?<BStart>\d+)-(?<BEnd>\d+)\s*$";

            private readonly HashSet<Range> _ranges = new HashSet<Range>();
            public string Name { get; }

            public TicketField(string input)
            {
                var match = Regex.Match(input, RangePattern);

                Name = match.Groups["name"].Value.Trim();
                
                // Append the 2 ranges
                _ranges.Add(new Range(int.Parse(match.Groups["AStart"].Value),int.Parse(match.Groups["AEnd"].Value)));
                _ranges.Add(new Range(int.Parse(match.Groups["BStart"].Value), int.Parse(match.Groups["BEnd"].Value)));

            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"{Name}:");
                foreach (var r in _ranges)
                {
                    sb.Append($" {r}");
                }

                return sb.ToString();
            }

            // Is the given number valid for any of the input ranges
            public bool IsNumberInValidRange(int number)
            {
                return _ranges.Any(x => x.IsValid(number));
            }

            // Are all the numbers in the input valid for this ticket field (ie. in a range)
            public bool AreNumbersInValidRange(IEnumerable<int> numbers)
            {
                return numbers.All(x => IsNumberInValidRange(x));
            }

            private class Range
            {
                private readonly int _end;
                private readonly int _start;

                public Range(int start, int end)
                {
                    _end = end;
                    _start = start;
                }

                public bool IsValid(int num)
                {
                    return num >= _start && num <= _end;

                }

                public override string ToString()
                {
                    return $"({_start} - {_end})";
                }
            }
        }
    }
}
