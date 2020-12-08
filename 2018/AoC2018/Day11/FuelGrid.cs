using System;
using System.Collections.Generic;
using System.Text;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day11
{
    /// <summary>
    /// Numeric map with a fixed size.
    /// Use rather than Map<int> as it has custom DrawMap logic that makes it nicer to read.
    /// </summary>
    public class FuelMap : Map<int>
    {
        public readonly int GridSize;

        public FuelMap(int gridSize) : base(0)
        {
            GridSize = gridSize;
        }

        public override string DrawMap()
        {

            StringBuilder map = new StringBuilder();
            for (int y = 1; y <= GridSize ; y++)
            {
                map.Append(Environment.NewLine);

                for (int x = 1 ; x <= GridSize; x++)
                {
                    int value = this[x, y];

                    // if less than zero - no leading space to allow negative sign
                    map.Append(value < 0 ? $"{value} " : $" {value} ");
                }
            }

            return map.ToString();
        }
    }

    public sealed class FuelGrid : FuelMap
    {
        private readonly int _serial;

        // Cached cumulative totals
        public readonly FuelMap RowTotals;
        public readonly FuelMap ColumnTotals; 

        public FuelGrid(int gridSerialNumber, int gridSize) : base(gridSize)
        {
            _serial = gridSerialNumber;
            RowTotals = new FuelMap(gridSize);
            ColumnTotals = new FuelMap(gridSize);

            for (int x = 1; x <= gridSize; x++)
            {
              
                for (int y = 1; y <= gridSize; y++)
                {
                    int power = CalculatePower(x, y, _serial);
                    Add(new Position(x, y), power);  // add to the fuelGrid
                }
            }

            // Get row and column totals
            var (item1, item2) = GetGridTotals();
            RowTotals = item1;
            ColumnTotals = item2;
        }

        
        // Cache the cumulative row & column totals for the grid.
        // For calculating the Sum of an area, we only need to do 2 lookups per row (or column)
        // Eg. Row 1, columns 3 to 5 = RowTotals[5,1] - RowTotals[3, 1]
        // So a 300 search grid is only (300 * 2 ) = 600 lookups
        // The iterative approach would use 300 * 300 = 90,000 lookups!
        //
        // This can then be improved as we move across the Search area (for same size grid) as 
        // we only need to add the new Row / Column totals, and remove the old Row / columns totals - ie 4 lookups
        // 
        // This is only currently optimised for moving across the search area on the Y-Axis. 
        // It regenerates the initial power everytime it moves across the X-Axis.
        public SearchResult FindLargestPowerRegion(int searchSize)
        {
            int maxPower = int.MinValue;
            Position result = new Position(0, 0);

            for (int x = 1; x <= GridSize - searchSize + 1; x++)
            {
                // Use a queue to hold power values per line.  
                // When we move down a line- we can just pop the old value off and add the next value
                Queue<int> powerPerRow = new Queue<int>(searchSize);  
                int power = 0;  // use same power for each column (x stays the same)
                
                // calculate initial power (on top <X> rows)
                // This could be cached and the exact same calculation used (ie. Column totals stored in a queue).
                for (int i = 0; i < searchSize; i++)
                {
                    int row1 = RowTotals[x + searchSize - 1, i+1];  // new row added

                    powerPerRow.Enqueue(row1);
                    power += row1;
                }

                if (power > maxPower)
                {
                    maxPower = power;
                    result = new Position(x, 1);
                }
                
                // As we move down the y axis - we only need to remove the oldest row total
                // Then add the new row total.
                for (int y = 2; y <= GridSize - searchSize + 1; y++)
                {
                    int row1 = RowTotals[x + searchSize - 1, y + searchSize -1];  // new row added
                    int row2 = RowTotals[x - 1, y + searchSize -1];  // old row removed

                    // Remove old value (ie no longer in search space)
                    // and add the new area in the search space)
                    int newPower = row1 - row2;
                    int oldPower = powerPerRow.Dequeue();
                    powerPerRow.Enqueue(newPower);

                    power = power + newPower - oldPower;  // update power

                    if (power > maxPower)
                    {
                        maxPower = power;
                        result = new Position(x, y);
                    }
                }
            }

            return new SearchResult(result, maxPower, searchSize);
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

        // Gets the cumulative totals per row / columns.
        // Eg.  
        // [ 1, 4, -1]
        // [ 5, 0, 2 ]
        // Row Columns:
        // [1, 5, 4]
        // [5, 5, 7]
        // Column Totals:
        // [1, 4, -1]
        // [6, 4, 1]
        private Tuple<FuelMap, FuelMap> GetGridTotals()
        {
            FuelMap rowTotals = new FuelMap(GridSize);
            FuelMap columnTotals = new FuelMap(GridSize);

            for (int x = 1; x <= GridSize; x++)
            {
                int rowTotal = 0;
                int columnTotal = 0;

                for (int y = 1; y <= GridSize; y++)
                {
                    columnTotal += this[x, y];
                    rowTotal += this[y, x];

                    rowTotals.Add(new Position(y, x), rowTotal);
                    columnTotals.Add(new Position(x, y), columnTotal);
                }
            }

            return new Tuple<FuelMap, FuelMap>(rowTotals, columnTotals);
        }
    }

    public class SearchResult
    {
        public int Power { get; }
        public int SearchSize { get; }
        public Position Position { get; }

        public SearchResult(Position p, int pow, int searchSize)
        {
            Power = pow;
            Position = p;
            SearchSize = searchSize;
        }

        public override string ToString()
        {
            return $"{Position.ToString()}, Power = {Power}, Size = {SearchSize}";
        }
    }

}
