using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AoC.Common
{
    public class MathFunctions
    {
        public static BigInteger LCM(params long[] values)
        {
            BigInteger result = 1;
            long max = values.Max();
            List<long> currentValues = values.ToList();

            for (int i = 2; i <= max; i++)
            {
                while (currentValues.Any(l => l % i == 0))
                {
                    result = result * i;
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
                result = result * l;
            }
            return result;
        }
    }
}
