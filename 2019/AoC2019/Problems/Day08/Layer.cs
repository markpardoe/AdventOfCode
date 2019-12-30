using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day08
{
    public class Layer
    {
        private readonly List<List<int>> _layerPixels = new List<List<int>>();
        private readonly Dictionary<int, int> _pixelCounts = new Dictionary<int, int>();

        public Layer(List<List<int>> data, Dictionary<int, int> pixelCounts)
        {
            _layerPixels = data ?? throw new ArgumentNullException(nameof(data));
            _pixelCounts = pixelCounts ?? throw new ArgumentNullException(nameof(pixelCounts));
        }

        public int GetPixelCount(int value)
        {
            return _pixelCounts[value];
        }

        public List<int> GetRow(int row)
        {
            return _layerPixels[row];
        }
    }
}
