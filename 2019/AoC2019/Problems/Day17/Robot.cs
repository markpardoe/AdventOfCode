using Aoc.AoC2019.IntCode;
using AoC.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Aoc.AoC2019.Problems.Day17
{
    public class Robot
    {
        private readonly ScaffoldMap _scaffoldMap = new ScaffoldMap();
        private readonly IVirtualMachine _computer;

        public Robot(IVirtualMachine computer)
        {
            _computer = computer ?? throw new ArgumentNullException(nameof(computer));
            ScanMap();
        }

        public List<Position> FindIntersections()
        {
            return _scaffoldMap.FindAllIntersections();
        }

        public string DrawMap()
        {
            return _scaffoldMap.DrawMap();
        }

        private void ScanMap()
        {
            _computer.Execute();
            int x = 0;
            int y = 0;

            List<long> outputs = _computer.Outputs.DequeueToList();
            foreach (char tile in outputs)
            {
                Position p = new Position(x, y);
                bool newLine = false;

                switch (tile)
                {
                    case ('#'):
                        _scaffoldMap.Add(p, ScaffoldType.Scaffold);
                        break;
                    case ('.'):
                        _scaffoldMap.Add(p, ScaffoldType.Empty);
                        break;
                    case ('^'):
                        _scaffoldMap.Add(p, ScaffoldType.Scaffold);
                        _scaffoldMap.Robot = p;
                        _scaffoldMap.RobotFacing = Direction.Up;
                        break;
                    case ('V'):
                    case ('v'):
                        _scaffoldMap.Add(p, ScaffoldType.Scaffold);
                        _scaffoldMap.Robot = p;
                        _scaffoldMap.RobotFacing = Direction.Down;
                        break;
                    case ('>'):
                        _scaffoldMap.Add(p, ScaffoldType.Scaffold);
                        _scaffoldMap.Robot = p;
                        _scaffoldMap.RobotFacing = Direction.Right;
                        break;
                    case ('<'):
                        _scaffoldMap.Add(p, ScaffoldType.Scaffold);
                        _scaffoldMap.Robot = p;
                        _scaffoldMap.RobotFacing = Direction.Left;
                        break;
                    case ((char)10):
                        newLine = true;
                        break;
                }

                if (newLine)
                {
                    x = 0;
                    y++;
                }
                else
                {
                    x++;
                }
            }
        }

        public List<string> FindPath()
        {
            List<string> path = new List<string>();
            Position current = _scaffoldMap.Robot;
            Direction direction = _scaffoldMap.RobotFacing;
            int movement = 0;

            while (true)
            {
                TurnDirection newDirection = GetNextDirection(current, direction);
                if (newDirection == null) 
                { 
                    break; 
                }

                if (newDirection.Turn != Turn.NoTurn && movement > 0)
                {
                    path.Add(movement.ToString());
                    movement = 0;
                }
                switch (newDirection.Turn)
                {
                    case (Turn.Left):                       
                        path.Add("L");                       
                        break;
                    case (Turn.Right):
                        path.Add("R");
                    break;
                }

                _scaffoldMap[current] = ScaffoldType.Cleaned;
                direction = newDirection.Direction;

                current = current.Move(newDirection.Direction);
                movement++;

                _scaffoldMap.RobotFacing = direction;
                _scaffoldMap.Robot = current;
                
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(_scaffoldMap.DrawMap());
                Thread.Sleep(25);                     
            }

            path.Add(movement.ToString());
            return path;
        }
        

        private TurnDirection GetNextDirection(Position pos, Direction currentDirection)
        {
            List<TurnDirection> directions = directionMap[currentDirection];

            foreach (TurnDirection d in directions)
            {
                if (CheckDirection(pos, d.Direction))
                {
                    return d;
                }
            }

            return null;  // no where else to go - must be at end of maze

        }

        private bool CheckDirection(Position pos, Direction direction)
        {
            Position next = pos.Move(direction);
            return _scaffoldMap[next] == ScaffoldType.Scaffold || _scaffoldMap[next] == ScaffoldType.Cleaned;
        }

        private static readonly Dictionary<Direction, List<TurnDirection>> directionMap = new Dictionary<Direction, List<TurnDirection>>()
        {
            {Direction.Up, new List<TurnDirection>() {new TurnDirection(Turn.NoTurn, Direction.Up), new TurnDirection(Turn.Right, Direction.Right), new TurnDirection(Turn.Left, Direction.Left) } },
            {Direction.Down, new List<TurnDirection>() {new TurnDirection(Turn.NoTurn, Direction.Down), new TurnDirection(Turn.Right, Direction.Left), new TurnDirection(Turn.Left, Direction.Right) } },
            {Direction.Left, new List<TurnDirection>() {new TurnDirection(Turn.NoTurn, Direction.Left), new TurnDirection(Turn.Right, Direction.Up), new TurnDirection(Turn.Left, Direction.Down) } },
            {Direction.Right, new List<TurnDirection>() {new TurnDirection(Turn.NoTurn, Direction.Right), new TurnDirection(Turn.Right, Direction.Down), new TurnDirection(Turn.Left, Direction.Up) } }
        };

        private class TurnDirection
        {
            public Turn Turn { get; }
            public Direction Direction { get; }

            public TurnDirection(Turn turn, Direction direction)
            {
                this.Turn = turn;
                this.Direction = direction;
            }
        }


        private enum Turn
        {
            NoTurn = 0,
            Left = 1,
            Right = 2
        }
    }
}