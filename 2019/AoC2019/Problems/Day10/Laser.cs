using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AoC.Common.Mapping;

namespace Aoc.AoC2019.Problems.Day10
{
    public class Laser
    {
        private readonly AsteroidMap _map;
        private readonly Position _laserPosition;
        private readonly Queue<Position> _visibleAsteroids = new Queue<Position>();

        public Laser (AsteroidMap map, Position laserPosition)
        {
            _map = map;
            _laserPosition = laserPosition;
        }

        public Position DestroyNextAsteroid()
        {
            // reset the queue if needed.
            if (_visibleAsteroids.Count == 0)
            {
                List<Position> visible = _map.FindVisibleAsteroids(_laserPosition);
                List<Position> RelativePositions = visible
                    .Select(p => new Position(p.X - _laserPosition.X, p.Y - _laserPosition.Y))
                    .OrderBy(p => Math.Atan2(p.X, p.Y))
                    .Reverse()
                    .ToList();

                foreach (Position p in RelativePositions)
                {
                    //  Console.Out.WriteLine(p.ToString() + " - Angle = " + Math.Round(Math.Atan2(p.X, p.Y), 5));
                    _visibleAsteroids.Enqueue(p);
                }
            }

            if (_visibleAsteroids.Count > 0) 
            { 
            Position relativeDestroyed = _visibleAsteroids.Dequeue();
            Position destroyed = new Position(_laserPosition.X + relativeDestroyed.X, _laserPosition.Y + relativeDestroyed.Y);
            _map.DestroyAsteroid(destroyed);
            return destroyed;
            }
            else
            {
                return new Position(-1, -1);
            }
        }
    }
}
