using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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
        private readonly Dictionary<char, ArenaTile> TileMapper = new Dictionary<char, ArenaTile>
        {
            {'#', ArenaTile.Wall},
            {'.', ArenaTile.Open},
            {'G', ArenaTile.Goblin},
            {'E', ArenaTile.Elf},
        };

        // Could use a dictionary <Position, Unit> but units are moving so would need to constantly update.
        // Should only be a max of 30 units - so it should be ok to just search for them by position 
        private readonly HashSet<Unit> _units = new HashSet<Unit>();

        public int Turn { get; private set; } = 0;

        public int GoblinCount => GetUnitCount(Unit.UnitType.Goblin);
        public int ElfCount => GetUnitCount(Unit.UnitType.Elf);


        public int GetUnitCount(Unit.UnitType unitType)
        {
            return _units.Count(x => x.Status == Unit.UnitStatus.Alive && x.Type == unitType);
        }

        public ArenaMap(IEnumerable<string> input) : base(ArenaTile.Wall)
        {
            int y = 0;
            foreach (var line in input)
            {
                for (int x = 0; x < line.Length; x++)
                {
                    char c = line[x];
                    ArenaTile t = TileMapper[c];
                    this.Add(new Position(x, y), TileMapper[c] );

                    if (t == ArenaTile.Elf)
                    {
                        Unit elf = new Unit(x, y, Unit.UnitType.Elf);
                        _units.Add(elf);
                    }
                    else if (t == ArenaTile.Goblin)
                    {
                        Unit goblin = new Unit(x, y, Unit.UnitType.Goblin);
                        _units.Add(goblin);
                    }
                    
                }
                y++;
            }
        }
        
        public override string DrawMap()
        {
            int max_X = MaxX;
            int max_Y = MaxY;

            StringBuilder sb = new StringBuilder();
            sb.Append($"Turn: {this.Turn}");

            for (int y = 0; y <= max_Y; y++)
            {
                sb.Append(Environment.NewLine);

                for (int x = 0; x <= max_X; x++)
                {
                    sb.Append((char) this[x, y]);
                }

                // Get units on the same row (Y) ordered by position
                var units = _units.Where(u => u.Y == y).OrderBy(u => u.X).ToList();
                if (units.Count > 0)
                {
                    sb.Append("     ");  // add spaces before hp
                    foreach (Unit unit in units)
                    {
                        sb.Append($" {unit.Character}({unit.HitPoints:D3}),");
                    }

                    sb.Remove(sb.Length - 1, 1);
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
            // Check if the game has finished
            if (GoblinCount == 0)
            {
                return GameStatus.ElfWin;
            }
            else if (ElfCount == 0)
            {
                return GameStatus.GoblinWin;
            }

            Turn++;
           // Get units in order
           var units = _units.OrderBy(u => u.Y).ThenBy(u => u.X).ToList();

           foreach (var unit in units)
           {
               // Units may have been killed before moving...
               if (unit.Status == Unit.UnitStatus.Alive)
               {
                   MoveUnit(unit);
                   AttackWithUnit(unit);
               }
           }


           // remove dead units - we can't do this in the loop as we can't remove items during Enumration
           // The map will be updated when they originally die.
           _units.RemoveWhere(x => x.Status == Unit.UnitStatus.Dead);

           return GameStatus.Running;
        }


        private void AttackWithUnit(Unit unit)
        {
            var enemy = GetUnitToAttack(unit);
            if (enemy != null)
            {
                unit.AttackUnit(enemy);
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
            if (unit.Status == Unit.UnitStatus.Dead) { throw new InvalidDataException("Unable to move a dead unit!");}

            var target = GetMoveTarget(unit)?.GetFirstNode();

            // If target is null - we don't want to move. 
            if (target != null)
            {
                if (unit.Position.DistanceTo(target) != 1)
                {
                    throw new InvalidDataException("Distance to target must be one!");
                }

                // Move unit and update map
                this[unit.Position] = ArenaTile.Open;
                this[target] = unit.TileType;
                unit.Move(target);
            }
        }

        // Find the move for a given unit.
        // We want to find the nearest space neighboring an enemy unit.
        // Since ties are resolved using 'Reading Order' (top to bottom, left to right)
        // we can do a breadth first search, adding neighbors in the correct order.
        // We then just need to return the first target found.
        private MapNode GetMoveTarget(Unit unit)
        {
            ArenaTile targetType = unit.EnemyTileType;  // Find shortest path to units of this type

            // squares we've checked
            HashSet<MapNode> checkedPositions = new HashSet<MapNode>();

            // Start with a list of neighbours in ReadingOrder (ie. top, left, right, down)
            var neighbors = unit.Position.GetNeighbouringPositionsInReadingOrder();

            Queue<MapNode> locationsToCheck = new Queue<MapNode>(neighbors.Select(n => new MapNode(n)));
            
            while (locationsToCheck.Count > 0)
            {
                MapNode current = locationsToCheck.Dequeue();
                ArenaTile tile = this[current];

                if (tile == targetType)
                {
                    // reached destination
                    // we don't want the enemy square -we want the position next to it - so return parent.
                    // if this is null, it means we're next to the enemyTarget - so no need to move.
                    // We don;t want to return the enemy itself as if there are multiple adjacent - the target is picked by HP rather than ReadingOrder
                    return current.Parent;  
                }
                else if (tile == ArenaTile.Open && !checkedPositions.Contains(current))
                {
                    var next = current.GetNeighbouringPositionsInReadingOrder();
                    foreach (var p in next)
                    {
                        locationsToCheck.Enqueue(new MapNode(p, current, current.DistanceFromStart+1));
                    }
                }
                // otherwise ignore it as we don't need to do anything.
                // No need to check if we've got shortest path as we're  using a breadth first search - so if its in checkPositions already, it must be faster
            }

            return null;
        }

        // Gets the unit to attack.
        private Unit GetUnitToAttack(Unit attacker)
        {
            // Get the positions on the map where there is an enemy unit (based on ArenaTileType) in a neighboring square
            // We could also do a search per neighboring position on the _units collection - but this should be faster if no enemy units (which is the norm)
            // as only 4 lookups + 1 lookup per unit (so 8 max) as opposed to 4 * <numUnits>
            var enemyPositions = attacker.Position.GetNeighbouringPositions().Where(p => this[p] == attacker.EnemyTileType);

            var enemies = new HashSet<Unit>();
            foreach (var pos in enemyPositions)
            {
                // add the unit.  There should always be one - if not something has gone horribly wrong and we want the error
                enemies.Add(_units.First(x => x.Status == Unit.UnitStatus.Alive &&  x.Type == attacker.EnemyType && x.X == pos.X && x.Y == pos.Y));
            }

            // return unit with lowest hitpoints, then in ReadingOrder
            return enemies.OrderBy(x => x.HitPoints).ThenBy(u => u.Y).ThenBy(u => u.X).FirstOrDefault();
        }
    }
}
