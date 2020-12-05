using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using AoC.Common;

namespace AoC.AoC2020.Problems.Day05
{
    public class BinaryBoarding : AoCSolution<int>
    {
        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            HashSet<int> SeatIds = input.Select(x => GetSeatId(x)).ToHashSet();

            int minId = SeatIds.Min();
            int maxId = SeatIds.Max();

            // Part A - maximum SeatId
            yield return maxId;


            // Find missing seat Id.  
            // We know the start (min) and final (max) seat Ids - so simply check that range for a missing Id.
            for (int i = minId + 1; i < maxId - 1; i++)
            {
                if (!SeatIds.Contains(i))
                {
                    yield return i;
                    break;
                }
            }
        }

        public override int Year => 2020;
        public override int Day => 5;
        public override string Name => "Day 5: Binary Boarding";
        public override string InputFileName => "Day05.txt";


        public int GetSeatId(string input)
        {
            Debug.Assert(input.Length == 10);

            // use sub-string instead of take as we know the input is alway 10 characters
            int rowNumber = GetSeatPosition(input.Substring(0, 7), 128, SeatPosition.FrontBack);
            int columnNumber = GetSeatPosition(input.Substring(7, 3), 8, SeatPosition.LeftRight);

            return (rowNumber * 8) + columnNumber;
        }

        private enum SeatPosition
        {
            FrontBack,
            LeftRight
        }

        private int GetSeatPosition(string input, int numSeats, SeatPosition position)
        {
            char firstChar = position == SeatPosition.FrontBack? 'F' : 'L';
            char lastChar = position == SeatPosition.FrontBack ? 'B' : 'R';

            int minVal = 0;
            int maxVal = numSeats -1;

            foreach (char row in input)
            {
                int midValue = (maxVal - minVal) / 2;

                // Console.WriteLine($"{row}: {minVal} ==> {maxVal}.   Mid = {midValue}");

                if (row == firstChar) // seat in front half
                {
                    maxVal = maxVal - midValue - 1;
                }
                else if (row == lastChar) // seat in back half
                {
                    minVal = minVal + midValue + 1;
                }
            }

            if (maxVal != minVal)
            {
                throw new InvalidDataException($"Seat not found: {minVal} - {maxVal}");
            }

            return maxVal;
        }
    }
}
