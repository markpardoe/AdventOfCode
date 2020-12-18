using System;
using System.Collections.Generic;
using System.Text;
using AoC.Common.Mapping;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day17
{

    public enum ScaffoldType
    {
        Scaffold = '#',
        Empty = '.',
        Unknown = '?' ,
        Cleaned = '+'
    }

    public class ScaffoldMap : Map<ScaffoldType>
    {
        public ScaffoldMap() :base(ScaffoldType.Unknown) { }
        
        public Position Robot { get; internal set; }
        public Direction RobotFacing { get; internal set; }

        public List<Position> FindAllIntersections()
        {
            List<Position> intersections = new List<Position>();           

            foreach (Position p in _map.Keys)
            {
                if (this[p] == ScaffoldType.Scaffold)
                {
                    var neighbors = p.GetNeighboringPositions();
                    if (neighbors.All(p => this[p] == ScaffoldType.Scaffold))
                    {
                        intersections.Add(p);
                    }
                }
            }          
            return intersections;
        }

        // Mapping to the character set for the robot
        private readonly Dictionary<Direction, char> _robotFacingCharacterMap = new Dictionary<Direction, char>()
        {
            {Direction.Up, '^'},
            {Direction.Down, 'V'},
            {Direction.Left, '<'},
            {Direction.Right, '>'}
        };

        protected override char? ConvertValueToChar(Position position, ScaffoldType value)
        {
            if (Robot.Equals(position))
            {
                return _robotFacingCharacterMap[RobotFacing];
            }

            return (char)value;
        }
    }
}