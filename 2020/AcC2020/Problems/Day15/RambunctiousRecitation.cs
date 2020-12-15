using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AoC.Common;

namespace AoC.AoC2020.Problems.Day15
{
    public class RambunctiousRecitation :AoCSolution<int>
    {
        public override int Year => 2020;
        public override int Day => 15;
        public override string Name => "Day 15: Rambunctious Recitation";
        public override string InputFileName => null;

        private readonly List<int> PuzzleInput = new List<int>() {0, 14, 6, 20, 1, 4};

        private readonly List<int> Example1 = new List<int>() { 0, 3, 6 };

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            NumberGame game = new NumberGame(PuzzleInput);


            // Brute force the answer to part B.  Not ideal, but runs in approx. 10 seconds
            while (game.Turn <= 30000000)
            {
                int value = game.TakeTurn();
                if (game.Turn == 2020)
                {
                    yield return game.LastNumber;
                }
            }

            yield return game.LastNumber;
        }
    }


    public class NumberGame
    {
        public int Turn { get; private set; }

        // Holds the turn(s) in which each number was spoken
        private readonly Dictionary<int, List<int>> _previousNumbers = new Dictionary<int, List<int>>();
        private readonly List<int> _initialTurns;
        private int _lastNumber;

        public int LastNumber => _lastNumber;
       
        public NumberGame(List<int> initialValues)
        {
            Turn = 1;
            _initialTurns = initialValues;

            // add initial values in constructor - not the best way to do this,
            // but avoids checking if we're still using them in the main game loop.
            // this knocked about 2 seconds off execution time
            foreach (var value in initialValues)
            {
                AddNumber(value, Turn);
                _lastNumber = value;
                Turn++;
            }
        }

        /// <summary>
        /// Add a turn to the cached list of turns per value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="turn"></param>
        public void AddNumber(int value, int turn)
        {
            if (!_previousNumbers.ContainsKey(value))
            {
                _previousNumbers.Add(value, new List<int>());
            }

            _previousNumbers[value].Add(turn);
        }

        public int TakeTurn()
        {
            // either take number from initial list, or calculate it from the last number spoken
            int value = CalculateNumber(_lastNumber);

            AddNumber(value, Turn);
            _lastNumber = value;
            Turn++;
            return value;
        }


        // Returns how many times this number has been said
        private int CalculateNumber(int lastNumber)
        {
            var turns = _previousNumbers[lastNumber];
            int count = turns.Count;
            if (count == 1)
            {
                return 0;  // this number has either been said before, or only said once
            }
            else if (count > 1)
            {
                // Get difference between last 2 turns spoken
                return turns[count - 1] - turns[count - 2];
            }
            else
            {
                throw new InvalidDataException($"Number {lastNumber} has never been spoken.");
            }

        }
    }
}
