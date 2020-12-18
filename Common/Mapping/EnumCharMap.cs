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
        public EnumCharMap(T defaultValueValue) : base(defaultValueValue) { }
        
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

        protected override char? ConvertValueToChar(Position position, T value)
        { 
            // Convert the Enum to a char
            // We can't explicity cast an Enum to a Char - so use GetHashCode
            // which should be the numberic value for the enum
            var c = (T)this[position];
            return  (char)c.GetHashCode();
        }
    }
}
