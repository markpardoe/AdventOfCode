using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2019.Problems.Day08
{
    public class SpaceImage : IEnumerable<Layer>
    {
        private readonly List<Layer> _imageData = new List<Layer>();
        private readonly int[,] _displayLayer;
        private readonly int _rows;
        private readonly int _columns;

        public SpaceImage(int rows, int columns, string data)
        {
            _rows = rows;
            _columns = columns;
            _displayLayer = new int[rows, columns];
            InitialiseDisplayData();
           
            int index = 0;
            while (index < data.Length)
            {
                AddLayer(data.Substring(index, (rows * columns)));
                index += (rows * columns);
            }
        }

        private void InitialiseDisplayData()
        {
            for (int row = 0; row < _rows; row++)
            {
                for (int col = 0; col < _columns; col++)
                {
                    _displayLayer[row, col] = 2;
                }
            }
        }

        public IEnumerator<Layer> GetEnumerator()
        {
            return _imageData.GetEnumerator();
        }

        internal Layer GetLayer(int layer)
        {
            return _imageData[layer];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string DrawImage()
        {
            StringBuilder s = new StringBuilder();
            for (int row = 0; row < _rows; row++)                       
            {
                s.Append(Environment.NewLine);
                for (int col = 0; col < _columns; col++)
                {
                    int value = _displayLayer[row, col];
                    if (value == 0)
                    {
                        s.Append(" ");
                    }
                    else
                    {
                        s.Append(value);
                    }
                }
            }
            return s.ToString();
        }

        private void AddLayer(string inputData)
        {
            if (inputData.Length != (_rows * _columns))
            {
                throw new InvalidOperationException("Invalid number of rows & columns in layer data.");
            }

            Dictionary<int, int> pixelCounts = new Dictionary<int, int>(10);
            for (int i = 0; i <= 9; i++)
            {
                pixelCounts.Add(i, 0);
            }
            
            Queue<int> intData = new Queue<int>(inputData.Select(c => Int32.Parse(c.ToString())));
            List<List<int>> pixels = new List<List<int>>();

            for (int i = 0; i < _rows; i++)
            {
                List<int> row = new List<int>();
                for (int j = 0; j < _columns; j++)
                {
                    int value = intData.Dequeue();
                    row.Add(value);
                    pixelCounts[value]++;

                    if (_displayLayer[i,j] == 2 && value != 2)
                    {
                        _displayLayer[i,j] = value;
                    }
                }
                pixels.Add(row);
            }

            _imageData.Add(new Layer(pixels, pixelCounts));
        }
    }
}
