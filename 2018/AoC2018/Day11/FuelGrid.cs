using System;
using System.Collections.Generic;
using System.Text;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day11
{
    public sealed class FuelGrid : Map<int>
    {
        private readonly int _serial;
        private readonly int _gridSize;

        public FuelGrid(int gridSerialNumber, int gridSize) : base(0)
        {
            _gridSize = gridSize;
            _serial = gridSerialNumber;

            for (int y = 1; y <= gridSize; y++)
            {
                for (int x = 1; x <= gridSize; x++)
                {
                    Add(new Position(x, y), CalculatePower(x, y, _serial));
                }
            }
        }


        public Position FindLargestPowerRegion()
        {
            int maxPower = int.MinValue;
            Position result = new Position(0, 0);

            for (int y = 0; y <= _gridSize - 3; y++)
            {
                for (int x = 0; x <= _gridSize - 3; x++)
                {
                    int power = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        power += this[x, y + i];
                        power += this[x + 1, y + i];
                        power += this[x + 2, y + i];
                    }

                    if (power > maxPower)
                    {
                        maxPower = power;
                        result = new Position(x, y);
                    }
                }
            }

            Console.Out.WriteLine($"MaxPower = {maxPower} ==> {result}");
            return result;
        }

        public int CalculatePower(int x, int y, int gridSerial)
        {
            /*Find the fuel cell's rack ID, which is its X coordinate plus 10.
                - Begin with a power level of the rack ID times the Y coordinate.
                - Increase the power level by the value of the grid serial number(your puzzle input).
                - Set the power level to itself multiplied by the rack ID.
                - Keep only the hundreds digit of the power level(so 12345 becomes 3; numbers with no hundreds digit become 0).
                - Subtract 5 from the power level. */
            int rackId = x + 10;

            int power = rackId * y;
            power += gridSerial;

            power = power * rackId;

            if (power < 100)
            {
                power = 0;
            }
            else
            {
                // Divide by 100 - then take mod 10 to get the 'hundreds digit)
                power = power / 100;
                power = power % 10;
            }

            return power - 5;
        }
    }
}
