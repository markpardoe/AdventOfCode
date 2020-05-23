using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day08
{
    public class Day08_Solution :AoC2019Solution
    {
        public override string URL => @"https://adventofcode.com/2019/day/8";

        public override int Day => 8;

        public override string Name => "Day 8: Space Image Format";

        public override string InputFileName => "Day08.txt";

        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {
            yield return FindLayerWithLeastDigits(GenerateImage(6,25, input.First())).ToString();
            yield return GenerateImage(6, 25, input.First()).DrawImage();
        }


        private SpaceImage GenerateImage(int rows, int columns, string messageData)
        {
            return new SpaceImage(rows, columns, messageData);
        }

        private int FindLayerWithLeastDigits(SpaceImage img)
        {
            int minCount = Int32.MaxValue;
            Layer minLayer = null;

            foreach (Layer layer in img)
            {
                int value = layer.GetPixelCount(0);
                if (value < minCount)
                {
                    minCount = value;
                    minLayer = layer;
                }
            }

            if (minLayer != null)
            {
                int result = minLayer.GetPixelCount(1) * minLayer.GetPixelCount(2);
                return result;
            }
            else
            {
                return -1;
            }
        }
    }
}
