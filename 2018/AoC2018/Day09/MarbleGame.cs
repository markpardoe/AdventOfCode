using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day09
{
    /// <summary>
    /// Basically a linked list - but if we move past last item then move to the first item (or vice-versa)
    /// </summary>
    public class MarbleGame
    {
        private LinkedListNode<int> _current;

        private readonly LinkedList<int> _data = new LinkedList<int>();

        public MarbleGame()
        {
            LinkedListNode<int> node = new LinkedListNode<int>(0);
            _data.AddFirst(node);
            _current = node;
        }

        public void Clear()
        {
            _data.Clear();
        }


        /// <summary>
        /// Adds a new marble and returns the score (if any)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int AddMarble(int value) 
        {
            // If a multiple of 23 - we don't place marble - but score instead
            if (value % 23 == 0)
            {
                // rotate 7 marbles counter-clockwise
                for (int i = 0; i < 7; i++)
                {
                    _current = _current.Previous ?? _data.Last;
                }

                int score = _current.Value + value;

                var next = _current.Next;
                _data.Remove(_current);
                _current = next;

                return score;
            }
            else  // rotate 1 place clockwise and insert the new marble
            {
                _current = _current.Next ?? _data.First;  // move forwards one space

                var node = new LinkedListNode<int>(value);
                _data.AddAfter(_current, node);
                _current = node;
                
                return 0;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var node in _data)
            {
                sb.Append(_current.Value == node ? $"({node})" : $" {node} ");
            }

            return sb.ToString();
        }
    }
}
