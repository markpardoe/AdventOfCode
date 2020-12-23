using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common.DataStructures;

namespace AoC.AoC2020.Problems.Day23
{
    public class CrabGame
    {
        //private readonly LinkedList<int> _gameData = new LinkedList<int>();
        //private readonly LinkedListNode<int>[] _nodes;
        private readonly HashedLinkedList<int> _gameData = new HashedLinkedList<int>();
        private int _move = 1;
        
        private LinkedListNode<int> _current;

        public int CupOrderValue()
        {
            var sb = new StringBuilder();
            var node = _gameData[1];
            node = node.Next ?? _gameData.First;

            while (node.Value != 1)
            {
                sb.Append(node.Value);
                node = node.Next ?? _gameData.First;
            }

            return int.Parse(sb.ToString());
        }

        public long GetCalculatedValue()
        {
            var node = _gameData[1];
            node = node.Next ?? _gameData.First;

            long total = node.Value;
            node = node.Next ?? _gameData.First;

            return total * node.Value;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var node in _gameData)
            {
                if (node == _current.Value)
                {
                    sb.Append($" ({node}) ");
                }
                else
                {
                    sb.Append($"  {node}  ");
                }
            }

            return sb.ToString();
        }

        public CrabGame(string rawData, int numCups = 0)
        {
            int maxValue = 0;

            int length = numCups;
            if (numCups < rawData.Length)
            {
                length = rawData.Length;
            }

            foreach (var c in rawData)
            {
                int value = int.Parse(c.ToString());
                var node = _gameData.AddLast(value);

                if (value > maxValue)
                {
                    maxValue = value;
                }
            }

            // Add remaining cups
            if (numCups > _gameData.Count)
            {
                int num = maxValue + 1;
                while (_gameData.Count < numCups)
                {
                    var node = _gameData.AddLast(num);
                    num++;
                }
            }

            _current = _gameData.First;
        }

        public void Move()
        {
            var removedCups = new List<int>();

            // remove next 3 cups
            for (int i = 0; i < 3; i++)
            {
                var cup = _current.Next ?? _gameData.First;
                removedCups.Add(cup.Value);
                _gameData.Remove(cup);
            }

            LinkedListNode<int> destination = FindNode(_current.Value -1);

            // add removed cups
            AddNodes(destination, removedCups);

            _current = _current.Next ?? _gameData.First;
            _move++;
        }

        // Find the node with given value.
        // If it doesn't exist - subtract one and try again.
        // in theory every number should exist in the gameData - so should never need to loop more than once
        private LinkedListNode<int> FindNode(int value)
        {
            while (true)
            {
                // loop around
                if (value < 0)
                {
                    value = _gameData.Keys.Max();
                }

                if (_gameData.Contains(value))
                {
                    return _gameData[value];
                }
                else
                {
                    value--;
                }
            }
        }

        private void AddNodes(LinkedListNode<int> node, List<int> cups)
        {
            // Add cups in reverse order as each new node will be inserted after the current node 
            // pushing the others along 1 space
            for (int i = cups.Count - 1; i >= 0;i--)
            {
                _gameData.AddAfter(node, cups[i]);
            }
        }
    }
}