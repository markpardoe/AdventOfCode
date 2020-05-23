using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AoC.Common
{
    public class MathFunctions
    {
        /// <summary>
        /// Calculates the Leaset Common Multiplier (LCM) for a set of long values
        /// https://en.wikipedia.org/wiki/Least_common_multiple
        /// Returns a BigInteger as we can't predict the size of the found value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns>BigInteger</returns>
        public static BigInteger LCM(params long[] values)
        {
            BigInteger result = 1;
            long max = values.Max();
            List<long> currentValues = values.ToList();

            for (int i = 2; i <= max; i++)
            {
                while (currentValues.Any(l => l % i == 0))
                {
                    result *= i;
                    List<long> newList = new List<long>(currentValues.Count);
                    foreach(long l in currentValues)
                    {
                        if (l % i ==0)
                        {
                            newList.Add(l/i);
                        }
                        else
                        {
                            newList.Add(l);
                        }
                    }
                    currentValues = newList;
                }
            }

            foreach (long l in currentValues)
            {
                result *= l;
            }
            return result;
        }
    }
}