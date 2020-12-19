using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day15
{
    public enum ArenaTile
    {
        Wall = '#',
        Open = '.',
        Goblin = 'G',
        Elf = 'E'
    }

    public enum GameStatus
    {
        Running,
        ElfWin,
        GoblinWin
    }

    public class ArenaMap : Map<ArenaTile>
    {
        // Could use a dictionary <Position, Unit> but units are moving so would need to constantly update.
        // Should only be a max of 30 units - so it should be ok to just search for them by position 
        private readonly HashSet<Unit> _units = new HashSet<Unit>();

        private readonly Dictionary<char, ArenaTile> _tileMapper = new Dictionary<char, ArenaTile>
        {
            {'#', ArenaTile.Wall},
            {'.', ArenaTile.Open},
            {'G', ArenaTile.Goblin},
            {'E', ArenaTile.Elf}
        };

        public ArenaMap(IEnumerable<string> input, int elfAttackValue = 3) : base(ArenaTile.Wall)
        {
            var y = 0;
            foreach (var line in input)
            {
                for (var x = 0; x < line.Length; x++)
                {
                    var c = line[x];
                    var t = _tileMapper[c];
                    this.Add(new Position(x, y), _tileMapper[c]);

                    if (t == ArenaTile.Elf)
                    {
                        var elf = new Unit(x, y, Unit.UnitType.Elf, elfAttackValue);
                        _units.Add(elf);
                    }
                    else if (t == ArenaTile.Goblin)
                    {
                        var goblin = new Unit(x, y, Unit.UnitType.Goblin);
                        _units.Add(goblin);
                    }
                }

                y++;
            }
        }

        public int Turn { get; private set; }

        public int GoblinCount => GetUnitCount(Unit.UnitType.Goblin);
        public int ElfCount => GetUnitCount(Unit.UnitType.Elf);


        public int GetUnitCount(Unit.UnitType unitType)
        {
            return _units.Count(x => x.Status == Unit.UnitStatus.Alive && x.Type == unitType);
        }

        public override string DrawMap(int padding = 0)
        {
            var maxX = MaxX;
            var maxY = MaxY;

            var sb = new StringBuilder();
            sb.Append($"Turn: {Turn}");

            for (var y = 0; y <= maxY; y++)
            {
                sb.Append(Environment.NewLine);

                for (var x = 0; x <= maxX; x++) sb.Append((char) this[x, y]);

                // Get units on the same row (Y) ordered by position
                var units = _units.Where(u => u.Y == y).OrderBy(u => u.X).ToList();
                if (units.Count > 0)
                {
                    sb.Append("     "); // add spaces before hp
                    foreach (var unit in units) sb.Append($" {unit},");

                    sb.Remove(sb.Length - 1, 1);
                    // Extra spaces because we don't clear the screen - just overwrite.
                    // So we need to make sure we wipe any leftover HPs from last turn
                    sb.Append("                                ");
                }
                else
                {
                    sb.Append("                                ");
                }
            }

            return sb.ToString();
        }

        public GameStatus RunTurn()
        {
            // Get units in order
            var units = _units.OrderBy(u => u.Y).ThenBy(u => u.X).ToList();

            foreach (var unit in units)
            {
                // Units may have been killed before moving...
                if (unit.Status == Unit.UnitStatus.Alive)
                {
                    MoveUnit(unit);
                    AttackWithUnit(unit);

                    // Check if the game has finished
                    if (GoblinCount == 0) return GameStatus.ElfWin;
                    if (ElfCount == 0) return GameStatus.GoblinWin;
                }
            }

            // remove dead units - we can't do this in the loop as we can't remove items during Enumration
            // The map will be updated when they originally die.
            _units.RemoveWhere(x => x.Status == Unit.UnitStatus.Dead);


            // Check if the game has finished
            if (GoblinCount == 0) return GameStatus.ElfWin;
            if (ElfCount == 0) return GameStatus.GoblinWin;

            Turn++;
            return GameStatus.Running;
        }

       
        // Simulate attack move with specified unit
        // Checks for a valid unit to attack - and removes HP from it.
        private void AttackWithUnit(Unit unit)
        {
            var enemy = GetUnitToAttack(unit);
            if (enemy != null)
            {

                // Remove HP from unit
                enemy.Hit(unit.AttackValue);

                // remove the dead enemy
                if (enemy.Status == Unit.UnitStatus.Dead)
                {
                    this[enemy.Position] = ArenaTile.Open;
                    _units.Remove(enemy);
                }
            }
        }

        private void MoveUnit(Unit unit)
        {
            if (unit.Status == Unit.UnitStatus.Dead) throw new InvalidDataException("Unable to move a dead unit!");

            // the target will be a square next to an enemy.
            // but we only want the first node on the path to it...
            var target = GetMoveTarget(unit)?.GetFirstNode();

            // If target is null - we didn't find a valid square to move to
            if (target != null)
            {
                if (unit.Position.DistanceTo(target.Position) != 1)
                    throw new InvalidDataException("Distance to target must be one!");

                // Move unit and update map
                this[unit.Position] = ArenaTile.Open;
                this[target.Position] = unit.TileType;
                unit.Move(target.Position);
            }
        }

        // Find the move for a given unit.
        // We want to find the nearest space neighboring an enemy unit.
        // Since ties are resolved using 'Reading Order' (top to bottom, left to right)
        // we can do a breadth first search, adding neighbors in the correct order.
        // We then just need to return the first target found.
        private MapNode GetMoveTarget(Unit unit)
        {
            var targetType = unit.EnemyTileType; // Find shortest path to units of this type

            // squares we've checked
            var checkedPositions = new HashSet<MapNode>();

            // Start with a list of neighbours in ReadingOrder (ie. top, left, right, down)
            var neighbors = unit.Position.GetNeighboringPositionsInReadingOrder();

            var locationsToCheck = new Queue<MapNode>(neighbors.Select(n => new MapNode(n)));

            while (locationsToCheck.Count > 0)
            {
                var current = locationsToCheck.Dequeue();
                var tile = this[current.Position];

                if (tile == targetType)
                {
                    // reached destination
                    // we don't want the enemy square -we want the position next to it - so return parent.
                    // if this is null, it means we're next to the enemyTarget - so no need to move.
                    // We don;t want to return the enemy itself as if there are multiple adjacent - the target is picked by HP rather than ReadingOrder
                    return current.Parent;
                }

                if (tile == ArenaTile.Open && !checkedPositions.Contains(current))
                {
                    var next = current.Position.GetNeighboringPositionsInReadingOrder();
                    foreach (var p in next)
                        locationsToCheck.Enqueue(new MapNode(p, current, current.DistanceFromStart + 1));
                }
                // otherwise ignore it as we don't need to do anything.
                // No need to check if we've got shortest path as we're  using a breadth first search - so if its in checkPositions already, it must be faster

                checkedPositions.Add(current);
            }

            return null;
        }

        // Gets the unit to attack.
        private Unit GetUnitToAttack(Unit attacker)
        {
            // Get the positions on the map where there is an enemy unit (based on ArenaTileType) in a neighboring square
            // We could also do a search per neighboring position on the _units collection - but this should be faster if no enemy units (which is the norm)
            // as only 4 lookups + 1 lookup per unit (so 8 max) as opposed to 4 * <numUnits>
            var enemyPositions = attacker.Position.GetNeighboringPositions()
                .Where(p => this[p] == attacker.EnemyTileType);

            var enemies = new HashSet<Unit>();
            foreach (var pos in enemyPositions)
                // add the unit.  There should always be one - if not something has gone horribly wrong and we want the error
                enemies.Add(_units.First(x =>
                    x.Status == Unit.UnitStatus.Alive && x.Type == attacker.EnemyType && x.X == pos.X && x.Y == pos.Y));

            // return unit with lowest hitpoints, then in ReadingOrder
            return enemies.OrderBy(x => x.HitPoints).ThenBy(u => u.Y).ThenBy(u => u.X).FirstOrDefault();
        }

        public int SumRemainingHitpoints()
        {
            return _units.Where(x => x.Status == Unit.UnitStatus.Alive).Sum(x => x.HitPoints);
        }
    }
}