using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc.AoC2019.Problems.Day12
{
    public class MoonMap
    {
        public List<Moon> Moons { get; }

        public MoonMap(List<Position3d> initialPostions)
        {
            Moons = new List<Moon>(initialPostions.Count);
            foreach (Position3d p in initialPostions)
            {
                Moons.Add(new Moon(p.X, p.Y, p.Z));
            }
        }

        public string GetMap()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Moon moon in Moons)
            {
                sb.Append(Environment.NewLine);
                sb.Append($"POS = <X = {moon.Position.X}, Y = {moon.Position.Y}, Z = {moon.Position.Z}>,   VEL = <X = {moon.Velocity.X}, Y = {moon.Velocity.Y}, Z = {moon.Velocity.Z}>");
            }

            return sb.ToString();
        }

        public void Move(int iterations = 1)
        {
            for (int x = 0; x < iterations; x++)
            {
                // Apply Gravity
                // get all pairs of moons
                for (int i = 0; i < Moons.Count; i++)
                {
                    for (int j = i + 1; j < Moons.Count; j++)
                    {
                        Moon.ApplyGravity(Moons[i], Moons[j]);
                    }
                }

                foreach (Moon m in Moons)
                {
                    m.ApplyVelocity();
                }
            }
        }

        public int GetTotalEnergy()
        {
            return Moons.Sum(a => a.TotalEnergy);
        }
    }
}