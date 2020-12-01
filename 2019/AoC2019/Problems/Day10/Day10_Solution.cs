using AoC.Common;
using AoC.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day10
{
    public class Day10_Solution :AoCSolution<int>
    {
        public override int Year => 2019;

        public override int Day => 10;

        public override string Name => "Day 10: Monitoring Station";

        public override string InputFileName => "Day10.txt";

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            yield return FindBestLocation(input);
            yield return DestroyAsteroids(input, new Position(20,21), 200);

        }

        private int FindBestLocation(IEnumerable<string> input)
        {
            AsteroidMap map = new AsteroidMap(input.ToList());
            int max = Int32.MinValue;
            foreach (Position p in map.Asteroids)
            {
                int total = map.CountVisibleAsteroids(p);
                if (total > max)
                {
                    max = total;
                }
            }
            return max;
        }

        private int DestroyAsteroids(IEnumerable<string> input, Position laserPosition, int asteroidCount)
        {
            AsteroidMap map = new AsteroidMap(input.ToList());
            Laser laser = new Laser(map, laserPosition);

            Position lastDestroyed = new Position(0, 0);
            for (int i = 1; i <= asteroidCount; i++)
            {
                lastDestroyed = laser.DestroyNextAsteroid();             
            }

            return (lastDestroyed.X * 100) + lastDestroyed.Y;
        }
    }
}
