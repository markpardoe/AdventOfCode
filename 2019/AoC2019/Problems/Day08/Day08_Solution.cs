using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;

namespace Aoc.AoC2019.Problems.Day08
{
    public class Day08_Solution :ISolution
    {
        public int Year => 2019;

        public int Day => 8;

        public string Name => "Day 8: Space Image Format";

        public string InputFileName => "Day08.txt";

        public IEnumerable<string> Solve(IEnumerable<string> input)
        {
            yield return FindLayerWithLeastDigits(GenerateImage(6,25, input.First()), 0).ToString();
            yield return GenerateImage(6, 25, input.First()).DrawImage();
        }


        private SpaceImage GenerateImage(int rows, int columns, string messageData)
        {
            return new SpaceImage(rows, columns, messageData);
        }

        private int FindLayerWithLeastDigits(SpaceImage img, int digit)
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
