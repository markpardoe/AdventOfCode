using System;
using System.Collections.Generic;
using System.Text;

namespace Aoc.AoC2019.IntCode
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Dequeues  an entire queue into a list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queue"></param>
        /// <returns></returns>
        public static List<T> DequeueToList<T>(this Queue<T> queue)
        {
            List<T> list = new List<T>();
            while (queue.Count > 0)
            {
                list.Add(queue.Dequeue());
            }
            return list;
        }
    }
}
