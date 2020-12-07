using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc.Aoc2018.Day08
{
    public class Node
    {

        private readonly List<Node> _childNodes = new List<Node>();
        private readonly List<int> _metadata = new List<int>();

        public Node(Queue<int> inputs)
        {
            // First 2 entries are count of children & count of metadata entries.
            int childQty = inputs.Dequeue();
            int metaDataCount = inputs.Dequeue();

            for (int i = 0; i < childQty; i++)
            {
                Node child = new Node(inputs);
                _childNodes.Add(child);
            }

            for (int i = 0; i < metaDataCount; i++)
            {
                _metadata.Add(inputs.Dequeue());
            }
        }

        public int SumMetadata()
        {
            return _metadata.Sum() + _childNodes.Sum(c => c.SumMetadata());
        }

        public int GetNodeValue()
        {
            if (_childNodes.Count == 0)
            {
                return _metadata.Sum();
            }
            else
            {
                int total = 0;
                foreach (var m in _metadata)
                {
                    if (m <= _childNodes.Count)
                    {
                        total += _childNodes[m - 1].GetNodeValue();
                    }
                }

                return total;
            }
        }
    }
}