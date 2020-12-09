using System;
using System.IO;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day15
{
   

    public class Unit
    {

        public enum UnitStatus
        {
            Alive,
            Dead
        }

        public enum UnitType
        {
            Elf,
            Goblin
        }


        public int AttackValue => 3;
        public int HitPoints { get; private set; } = 200;

        public int X { get; private set; }
        public int Y { get; private set; }

        public string Id { get; } = Guid.NewGuid().ToString();  // unique Id for the unit

        public UnitStatus Status => HitPoints > 0 ? UnitStatus.Alive : UnitStatus.Dead;
        public UnitType Type { get; }

        public Position Position => new Position(X, Y);

        public Unit(int x, int y, UnitType type)
        {
            this.X = x;
            this.Y = y;
            this.Type = type;
        }

        public char Character => Type == UnitType.Elf ? 'E' : 'G';

        // Returns the type of unit it will attack
        public UnitType EnemyType => Type == UnitType.Elf ? UnitType.Goblin : UnitType.Elf;

        // The tileType for enemy units
        public ArenaTile EnemyTileType => Type == UnitType.Elf ? ArenaTile.Goblin : ArenaTile.Elf;

        // The tileType for itself 
        public ArenaTile TileType => Type == UnitType.Elf ? ArenaTile.Elf : ArenaTile.Goblin;

        public void Move(Position position)
        {
            X = position.X;
            Y = position.Y;
        }

        public void AttackUnit(Unit enemy)
        {

            if (enemy == null)
            {
                throw new ArgumentNullException(nameof(enemy));
            }

            if (this.Position.DistanceTo(enemy.Position) != 1)
            {
                throw new InvalidDataException("Units must be 1 tile apart to fight!");;
            }

            if (this.Status == UnitStatus.Dead || enemy.Status == UnitStatus.Dead)
            {
                throw new InvalidDataException("Deceased units can't fight!"); ;
            }

            enemy.Hit(this.AttackValue);
        }

        public void Hit(int attack)
        {
            this.HitPoints -= attack;
        }
    }
}