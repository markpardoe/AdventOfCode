using AoC.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc.AoC2019.Problems.Day20
{
    public class Maze : Map<TileType>
    {
        public Dictionary<Position, Position> Portals { get; } = new Dictionary<Position, Position>();
        private readonly List<List<char>> _mazeData = new List<List<char>>();

        public Position StartPosition { get; }
        public Position ExitPosition { get; }

        public Maze(List<string> mapData) : base(TileType.Void)
        {
            Dictionary<string, List<Portal>> portals = new Dictionary<string, List<Portal>>();

            foreach (string row in mapData)
            {
                _mazeData.Add(row.ToList());
            }           

            for (int y = 0; y < _mazeData.Count; y++) 
            {
                for (int x = 0; x < _mazeData[y].Count;x++)
                {
                    Char c = _mazeData[y][x];
                    TileType t;
                    if (c == '#')
                    {
                        t = TileType.Wall;
                    }
                    else if (c == '.')
                    {
                        string portalId;
                        if (IsPortal(x, y, out portalId))
                        {
                            PortalType pType;
                            if (portalId == "AA")
                            {
                                pType = PortalType.Start;
                                StartPosition = new Position(x, y);
                            }
                            else if (portalId == "ZZ")
                            {
                                pType = PortalType.Exit;
                                ExitPosition = new Position(x, y);
                            }
                            else if (x == this.MaxX - 2 || y == this.MaxY - 2 || x == 2 || y == 2)
                            {
                                pType = PortalType.Outside;
                            }
                            else
                            {
                                pType = PortalType.Inside;
                            }

                            t = TileType.Portal;

                            Portal portal = new Portal(x, y, portalId, pType);
                            if (!portals.ContainsKey(portalId))
                            {
                                portals.Add(portalId, new List<Portal>());
                            }
                            portals[portalId].Add(portal);
                        }
                        else
                        {
                            t = TileType.Path;
                        }
                    }
                    else
                    {
                        t = TileType.Void;
                    }

                    this.Add(new Position(x,y), t);
                }
            }


            // Create portal mapping
            foreach(List<Portal> portalLink in portals.Values)
            {
                if (portalLink.Count > 2)
                {
                    throw new InvalidOperationException();
                }
                else if (portalLink.Count == 2)
                {
                    Portals.Add(portalLink[0].Position, portalLink[1].Position);
                    Portals.Add(portalLink[1].Position, portalLink[0].Position);
                }
            }
        }

        private bool IsPortal(int x, int y, out string portalId)
        {
            if (_mazeData[y][x] != '.')  // Portals must be on a path!
            {
                portalId = null;
                return false;
            }

            portalId = GetPortalId(x + 1, y, x + 2, y); // check to right
            if (portalId != null) { return true; }

            portalId = GetPortalId(x -2, y, x - 1, y);  // check to left
            if (portalId != null) { return true; }
            portalId = GetPortalId(x, y + 1, x, y + 2); // check downwards
            if (portalId != null) { return true; }
            portalId = GetPortalId(x, y - 2, x, y - 1); // check upwards
            if (portalId != null) { return true; }

            return false;
        }

        private string GetPortalId(int x1, int y1, int x2, int y2)
        {
            char A = _mazeData[y1][x1];
            char B = _mazeData[y2][x2];

            if (A >= 65 && A <= 90 && B >= 65 && B <= 90) 
            {
                return A.ToString() + B.ToString();
            }
            else
            {
                return null;
            }
        }

        protected override IEnumerable<Position> GetNeighboringPositions(Position position)
        {
            var neighbours =  position.GetNeighboringPositions().Where(p => base[p] != TileType.Wall && base[p] != TileType.Void).ToList();

            // if its a portal - we can move through it to the next portal
            if (this[position] == TileType.Portal && Portals.ContainsKey(position))
            {
                neighbours.Add(Portals[position]);
            }
            return neighbours;
        }

        protected override char? ConvertValueToChar(Position position, TileType value)
        {
            if (this[position] == TileType.Portal)
            {
                return '@';
            }
            else if (position.Equals(StartPosition))
            {
                return 'S';
            }
            else if (ExitPosition.Equals(position))
            {
                return 'X';
            }
            else
            {
                return _mazeData[position.X][position.Y];
            }

        }
        
        public MapNode FindExit()
        {
            return FindPathToPosition(StartPosition, ExitPosition);
        }
    }
}
