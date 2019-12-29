using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC.Common
{
    public static class ExtensionMethods
    {
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public static int Abs(this int value)
        {
            return System.Math.Abs(value);
        }
    }
}
