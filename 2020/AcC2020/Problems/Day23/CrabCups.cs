using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;

namespace AoC.AoC2020.Problems.Day23
{
    public class CrabCups :AoCSolution<long>
    {
        public override int Year => 2020;
        public override int Day => 23;
        public override string Name => "Day 23: Crab Cups";
        public override string InputFileName => null;
        

        public override IEnumerable<long> Solve(IEnumerable<string> input)
        {
            CrabGame game = new CrabGame(puzzleInput);
            RunGame(game, 100);
            yield return game.CupOrderValue();

            CrabGame game2 = new CrabGame(puzzleInput, 1000000);
            RunGame(game2, 10000000);
            yield return game2.GetCalculatedValue();

        }

        private readonly string puzzleInput = "253149867";

        private void RunGame(CrabGame game, int numTurns)
        {
            for (int i = 0; i < numTurns; i++)
            {
                game.Move();
            }
        }
    }

    public class CrabGame
    {
        private readonly LinkedList<int> _gameData = new LinkedList<int>();
      //  private readonly Dictionary<int, LinkedListNode<int>> _valueLookup = new Dictionary<int,LinkedListNode<int>>();
       private readonly LinkedListNode<int>[] _nodes;
       //private readonly int HighestValue;
       // private readonly int LowestValue;
        private int _move = 1;
        
        private LinkedListNode<int> _current;

        public int CupOrderValue()
        {
            var sb = new StringBuilder();
            var node = _nodes[1];
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
            var node = _nodes[1];
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
            // Create array to cache lookups per value
            _nodes = new LinkedListNode<int>[length + 1];

            foreach (var c in rawData)
            {
                int value = int.Parse(c.ToString());
                var node = _gameData.AddLast(value);
                _nodes[value] = node;

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
                    _nodes[num] = node;
                    num++;
                }
            }

            _current = _gameData.First;
        }

        public void Move()
        {
            //Console.Out.WriteLine($"-- move {_move} --");
            //Console.WriteLine(this.ToString());

            var removedCups = new List<int>();

            // remove next 3 cups
            for (int i = 0; i < 3; i++)
            {
                var cup = _current.Next ?? _gameData.First;
                removedCups.Add(cup.Value);
                Remove(cup);
            }

            //Console.WriteLine($"pick up: {string.Join(", ",removedCups)}");

            LinkedListNode<int> destination = FindNode(_current.Value -1);
            //Console.WriteLine($"Destination: {destination.Value}");

            // add removed cups
            AddNodes(destination, removedCups);

            _current = _current.Next ?? _gameData.First;
            _move++;
            //Console.Out.WriteLine();
        }

        // Find the node with given value.
        // If it doesn't exist - subtract one and try again.
        private LinkedListNode<int> FindNode(int value)
        {
            while (true)
            {
                // loop around
                if (value < 0)
                {
                    value = _nodes.Length-1;
                }

                if (_nodes[value] != null)
                {
                   return _nodes[value];
                }
                else
                {
                    value--;
                }
            }
        }

        private void Remove(LinkedListNode<int> node)
        {
            _gameData.Remove(node);
            _nodes[node.Value] = null;
        }

        private void AddNodes(LinkedListNode<int> node, List<int> cups)
        {
            // Add cups in reverse order as each new node will be inserted after the current node 
            // pushing the others along 1 space
            for (int i = cups.Count - 1; i >= 0;i--)
            {
                int value = cups[i];
                var newNode = _gameData.AddAfter(node, value);
                _nodes[value] = newNode;
            }
        }
    }
}
