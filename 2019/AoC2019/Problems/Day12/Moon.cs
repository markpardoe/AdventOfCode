using System;

namespace Aoc.AoC2019.Problems.Day12
{
    public class Moon
    {
        internal Position3d Position { get; }
        internal Position3d InitialPosition { get; }
        internal Position3d Velocity { get; }

        public string Id { get; }

        public Moon(int x, int y, int z)
        {
            Position = new Position3d(x, y, z);
            InitialPosition = new Position3d(x, y, z);
            Velocity = new Position3d(0, 0, 0);
            Id = Guid.NewGuid().ToString();
        }

        public void ApplyVelocity()
        {
            this.Position.X += this.Velocity.X;
            this.Position.Y += this.Velocity.Y;
            this.Position.Z += this.Velocity.Z;
        }

        public int PotentialEnergy => Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z);

        public int KineticEnergy => Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) + Math.Abs(Velocity.Z);

        public int TotalEnergy => PotentialEnergy * KineticEnergy;


        public static void ApplyGravity(Moon a, Moon b)
        {
            if (a.Position.X > b.Position.X)
            {
                a.Velocity.X--;
                b.Velocity.X++;
            }
            else if (a.Position.X < b.Position.X)
            {
                a.Velocity.X++;
                b.Velocity.X--;
            }

            if (a.Position.Y > b.Position.Y)
            {
                a.Velocity.Y--;
                b.Velocity.Y++;
            }
            else if (a.Position.Y < b.Position.Y)
            {
                a.Velocity.Y++;
                b.Velocity.Y--;
            }

            if (a.Position.Z > b.Position.Z)
            {
                a.Velocity.Z--;
                b.Velocity.Z++;
            }
            else if (a.Position.Z < b.Position.Z)
            {
                a.Velocity.Z++;
                b.Velocity.Z--;
            }
        }

        public bool AtInitialX ()
        {
            return Position.X == InitialPosition.X && Velocity.X == 0;
        }

        public bool AtInitialY()
        {
            return Position.Y == InitialPosition.Y && Velocity.Y == 0;
        }

        public bool AtInitialZ()
        {
            return Position.Z == InitialPosition.Z && Velocity.Z == 0;
        }
    }   
}
