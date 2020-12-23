using System;

namespace Aoc.Aoc2018.Day24
{
    public class UnitGroup
    {
        public int Id { get; }
        private readonly Unit _unit;
        public int Quantity { get; private set; }
        public int EffectivePower => Quantity * _unit.AttackDamage;
        public Army Side { get; }
        public string AttackType => _unit.AttackType;
        public int Initiative => _unit.Initiative;

        public UnitGroup(Unit unit, int qty, Army side, int id)
        {
            Side = side;
            _unit = unit ?? throw new ArgumentNullException(nameof(unit));
            if (qty <=0)  throw new ArgumentOutOfRangeException(nameof(qty));
            Quantity = qty;
            Id = id;
        }

        public void AddImmunityBoost(int boost)
        {
            if (Side == Army.Infection) throw new ArgumentException();

            _unit.AttackDamage += boost;
        }

        public override string ToString()
        {
            return $"{Side} group {Id} = {Quantity} units";
        }

        /// <summary>
        /// Calculates how much damage (total hp lost) a given attack would do to a unit
        /// </summary>
        /// <param name="attacker"></param>
        /// <returns></returns>
        public int EstimateDamage(UnitGroup attacker)
        {
            var total = 0;
            if (_unit.IsImmuneTo(attacker.AttackType)) total = 0;
            else if (_unit.IsWeakTo(attacker.AttackType)) total = attacker.EffectivePower * 2;
            else total = attacker.EffectivePower;

            //Console.WriteLine($"{attacker} would deal {this}  {total} damage");
            return total;
        }

        public int AttackedBy(UnitGroup attacker)
        {
            int damage = EstimateDamage(attacker);
            int deaths = damage / _unit.HP;

            if (deaths > Quantity)
            {
                deaths = Quantity;
            }

            //Console.WriteLine($"{attacker} attacks deal {this}, killing {deaths} units");

            this.Quantity -= deaths;
            return deaths;
        }
    }
}