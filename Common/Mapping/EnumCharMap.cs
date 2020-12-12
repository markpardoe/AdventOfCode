using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace AoC.Common.Mapping
{

    /// <summary>
    /// Creates a map of enums - where the enum is set to a char value.
    /// This works because chars are stored a int values, we just need to cast them to & from the enum correctly.
    /// T
    /// Eg.
    /// public enum Currency
    /// {
    ///     Pound = '£',
    ///     Dollar = '$'
    /// }
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumCharMap<T> : Map<T> where T : struct, Enum
    {
        // How many 'Default' rows / columns to add around the map when drawing it
        protected int DrawPadding { get; set; } = 2;

        public EnumCharMap(T defaultValue) : base(defaultValue)
        { }
        
        public virtual void LoadData(IEnumerable<string> input)
        {
            var y = 0;
            foreach (var line in input)
            {
                for (var x = 0; x < line.Length; x++)
                {
                    // Have to cast as object first or it won't compile.
                    var tile = (T)(object)line[x];
                    Add(new Position(x, y), tile);
                }
                y++;
            }
        }

        public override string DrawMap()
        {
            int min_X = MinX;
            int min_Y = MinY;
            int max_X = MaxX;
            int max_Y = MaxY;

            StringBuilder map = new StringBuilder();
            for (int y = min_Y - DrawPadding; y <= max_Y + DrawPadding; y++)
            {
                map.Append(Environment.NewLine);

                for (int x = min_X - DrawPadding; x <= max_X + DrawPadding; x++)
                {
                    var c = (T) this[x, y];
                    map.Append((char)c.GetHashCode());
                }
            }

            return map.ToString();
        }
    }
}
