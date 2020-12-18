using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AoC.Common.Mapping;

namespace AoC.AoC2020.Problems.Day03
{
    public enum TreeStatus
    {
        Open = 0,
        Tree = 1,
        Closed = 2, // edge of the map
    }

    public class TreeMap : Map<TreeStatus>
    {
        public TreeMap(IEnumerable<string> inputData) : base(TreeStatus.Closed)
        {
            int row = 0;
            foreach (var rowData in inputData)
            {
                int col = 0;
                foreach (var pos in rowData)
                {
                    TreeStatus status;
                    if (pos == '.')
                    {
                        status = TreeStatus.Open;
                    }
                    else if (pos == '#')
                    {
                        status = TreeStatus.Tree;
                    }
                    else
                    {
                        throw new InvalidDataException($"Invalid Map Character in input data: {pos}" );
                    }

                    this.Add(new Position(col, row), status);
                    col++;
                }
                row++;
            }
        }

        public int CountTreesOnPath(int right, int down)
        {
            int treeCount = 0;  // number of trees found

            // Cache max values
            int maxX = this.MaxX;
            int maxY = this.MaxY;

            // Current position
            int x = 0;
            int y = 0;

            while (y <= maxY) // loop until bottom row
            {

                // Normalise the x co-ordinate if we've gone off the edge of the map! 
                // Tree pattern repeats to right so we can just roll around to left side
                if (x > maxX)
                {   // Add 1 to MaxX as its a zero-based index.
                    // ie. 31 items has range 0 --> 30
                    x = x % (maxX + 1);
                }

                if (this[x, y] == TreeStatus.Tree)
                {
                    treeCount++;
                }
                x += right;
                y += down;
            }

            return treeCount;
        }
    }
}
