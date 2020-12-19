using System;
using System.Collections.Generic;
using System.Text;
using Aoc.AoC2019.IntCode;
using AoC.Common.Mapping;

namespace Aoc.AoC2019.Problems.Day11
{
    public enum PaintColor
    {
        Black = 0,
        White = 1
    }

    public class PaintRobot
    {
        private readonly DirectionalPosition _startLocation;
        private DirectionalPosition _currentPosition;
        public PaintMap Map { get; } = new PaintMap();
        private readonly IVirtualMachine _computer;
        private readonly List<Move> _moves = new List<Move>();

        public PaintRobot(IVirtualMachine vm, PaintColor startPositionColor = PaintColor.Black)
        {
            _computer = vm ?? throw new ArgumentNullException(nameof(vm));
            _startLocation = new DirectionalPosition(0, 0, Direction.Up);
            _currentPosition = _startLocation;
            Map[_startLocation.Position] = startPositionColor;
        }

        public void Paint()
        {
            Move move;
            while ((move = GetNextMove()) != null)
            {
              //  Console.Out.WriteLine("Moving: " + move.ToString());
                _moves.Add(move);

                // paint current location
                Map[_currentPosition.Position] = move.Color;

                // move to new location
                _currentPosition = _currentPosition.Turn(move.Turn);
            }
        }

        public int CountPanels()
        {
            return Map.PanelCount();
        }

        private Move GetNextMove()
        {           
            while (_computer.Outputs.Count < 2 && _computer.Status != ExecutionStatus.Finished)
            {
                if (_computer.Status == ExecutionStatus.AwaitingInput)
                {
                    _computer.Execute((long) Map[_currentPosition.Position]);
                }
                else 
                {
                    _computer.Execute();
                }
            }

            long? colorOutput = null;
            long? turnOutput = null;
            if (_computer.Outputs.Count >= 2)
            {
                colorOutput = _computer.Outputs.Dequeue();
                turnOutput = _computer.Outputs.Dequeue();
            }


            // Program finished - no more instructions
            if (!colorOutput.HasValue || !turnOutput.HasValue)
            {
                return null;
            }
            else
            {
                return new Move((PaintColor) colorOutput.Value, (TurnDirection) turnOutput);
            }
        }
    }
}
