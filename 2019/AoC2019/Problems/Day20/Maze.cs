using AoC.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
                    Portals.Add(portalLink[0], portalLink[1]);
                    Portals.Add(portalLink[1], portalLink[0]);
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

        public string GetPortalId(int x1, int y1, int x2, int y2)
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

        public override IEnumerable<Position> GetAvailableNeighbours(Position position)
        {
            var neighbours =  position.GetNeighbouringPositions().Where(p => base[p] != TileType.Wall && base[p] != TileType.Void).ToList();

            // if its a portal - we can move through it to the next portal
            if (this[position] == TileType.Portal && Portals.ContainsKey(position))
            {
                neighbours.Add(Portals[position]);
            }
            return neighbours;
        }

        public override string DrawMap()
        {
            int min_X = MinX;
            int min_Y = MinY;
            int max_X = MaxX;
            int max_Y = MaxY;

            StringBuilder map = new StringBuilder();
            for (int y = min_Y; y <= max_Y; y++)
            {
                map.Append(Environment.NewLine);

                for (int x = min_X; x <= max_X; x++)
                {
                    if (base[x, y] == TileType.Portal)
                    {
                        map.Append("@");
                    }
                    else if (x == StartPosition.X && y == StartPosition.Y)
                    {
                        map.Append("S");
                    }
                    else if (x == ExitPosition.X && y == ExitPosition.Y)
                    {
                        map.Append("X");
                    }
                    else
                    {
                        map.Append(_mazeData[y][x]);
                    }
                }
            }
            return map.ToString();
        }


        public MapNode FindExit()
        {
            HashSet<MapNode> openList = new HashSet<MapNode>();
            HashSet<MapNode> closedList = new HashSet<MapNode>();

            MapNode current = new MapNode(StartPosition);
            openList.Add(current);

            while (openList.Count > 0)
            {
                // get the closest square
                current = openList.OrderBy(l => l.DistanceFromStart).First();

                // move location to closed list
                closedList.Add(current);
                openList.Remove(current);

                // check if we've found an empty cell?
                if (ExitPosition.Equals(current)) { return current; }

                var neighbours = GetAvailableNeighbours(current);

                foreach (var location in neighbours)
                {
                    MapNode locationPos = new MapNode(location)
                    {
                        Parent = current,
                        DistanceFromStart = current.DistanceFromStart + 1
                    };

                    // if location is in the closed / open lists  - check if we've found a faster way there
                    // & update distance from start if we have a shorter path                    
                    // Otherwise we need to add it as a new move
                    if (!closedList.Contains(locationPos))
                    {
                        openList.Add(locationPos);
                    }
                }
            }
            return null;
        }
    }
}
