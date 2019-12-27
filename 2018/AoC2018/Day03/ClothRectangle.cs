using System;
using System.Collections.Generic;
using System.Text;

namespace AoC2018.Day03
{
    public class ClothRectangle
    {
        public string Id { get; }
        public int X { get; }
        public int Y { get; }

        public int Width { get; }
        public int Height { get; }

        public ClothRectangle(string data)
        {
            string[] s = data.Split('@', ',', ':', 'x');
            if (s.Length  != 5)
            {
                throw new InvalidOperationException("Input is invalid:" + data);
            }

            Id = s[0].Trim();
            X = Int32.Parse(s[1].Trim());
            Y = Int32.Parse(s[2].Trim());
            Width = Int32.Parse(s[3].Trim());
            Height = Int32.Parse(s[4].Trim());
        }

    }
}
