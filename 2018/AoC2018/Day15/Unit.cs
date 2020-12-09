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


        public int AttackValue { get; } = 3;
        public int HitPoints { get; private set; } = 200;

        public int X { get; private set; }
        public int Y { get; private set; }

        public string Id { get; } = Guid.NewGuid().ToString();  // unique Id for the unit

        public UnitStatus Status => HitPoints > 0 ? UnitStatus.Alive : UnitStatus.Dead;
        public UnitType Type { get; }

        public Position Position => new Position(X, Y);

        public Unit(int x, int y, UnitType type, int attackValue = 3)
        {
            this.X = x;
            this.Y = y;
            this.Type = type;
            AttackValue = attackValue;
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
        
        public void Hit(int attack)
        {
            this.HitPoints -= attack;
        }

        public override string ToString()
        {
            return $"{Character}({HitPoints:D3})";
        }
    }
}