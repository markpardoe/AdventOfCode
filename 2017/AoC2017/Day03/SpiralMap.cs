using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC.Common.Mapping;

namespace AoC2017.Day03
{
    public class SpiralMap : Map<int>
    {
        private readonly Dictionary<int, Position> _valuePositions = new Dictionary<int, Position>();

        public SpiralMap() : base(0) { }

        public void BuildMap(int maxValue)
        {
            Position currentPosition = new Position(0, 0);
            // start with down as this will check space to right (which is empty) and put first value there
            Direction currentDirection = Direction.Down;
            int value = 1;

            this.Add(currentPosition, value);  // Set first position at 0,0

            while (value <= maxValue)
            {
                // move to next position
                var nextDirection = _nextDirection[currentDirection];
                // check if we can change direction
                if (!Map.ContainsKey(currentPosition.Move(nextDirection)))
                {
                    currentDirection = nextDirection;
                }

                currentPosition = currentPosition.Move(currentDirection);

                // Get value to put in current position - and add it
                value = GetCurrentValue(value, currentPosition);

                this.Add(currentPosition, value);
                _valuePositions.Add(value, currentPosition);
            }
        }

        protected virtual int GetCurrentValue(int value, Position position)
        {
            return value + 1;
        }

        public Position FindValueDistanceFromOrigin(int value)
        {
            return _valuePositions[value];
        }

        private IReadOnlyDictionary<Direction, Direction> _nextDirection = new Dictionary<Direction, Direction>()
        {
            {Direction.Left, Direction.Down},
            {Direction.Right, Direction.Up},
            {Direction.Up, Direction.Left},
            {Direction.Down, Direction.Right}
        };
    }
}
