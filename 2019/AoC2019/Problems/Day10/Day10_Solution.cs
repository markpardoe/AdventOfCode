using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AoC.Common;
using AoC.Common.Mapping;

namespace AoC2019.Problems.Day10
{
    public class Day10_Solution :ISolution
    {
        public int Year => 2019;

        public int Day => 10;

        public string Name => "Day 10: Monitoring Station";

        public string InputFileName => "Day10.txt";

        public IEnumerable<string> Solve(IEnumerable<string> input)
        {
            yield return FindBestLocation(input).ToString();
            yield return DestroyAsteroids(input, new Position(20,21), 200).ToString();

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
            Laser laser = new Laser(map, new Position(20, 21));

            Position lastDestroyed = new Position(0, 0);
            for (int i = 1; i <= asteroidCount; i++)
            {
                lastDestroyed = laser.DestroyNextAsteroid();             
            }

            return (lastDestroyed.X * 100) + lastDestroyed.Y;
        }
    }
}
