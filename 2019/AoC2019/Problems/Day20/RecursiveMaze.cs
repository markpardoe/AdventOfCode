using AoC.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common.Mapping._3d;

namespace Aoc.AoC2019.Problems.Day20
{
    public class RecursiveMaze : Map<TileType>
    {
        public Dictionary<Position, List<Path>> Paths { get; } = new Dictionary<Position, List<Path>>();  // List of all paths (destination + distance) from a given portal
        private readonly List<List<char>> _mazeData = new List<List<char>>();
        private readonly HashSet<Portal> _portals = new HashSet<Portal>();

        public Position3d StartPosition { get; }
        public Position3d ExitPosition { get; }

        public override int MaxX => _mazeData[0].Count;
        public override int MinX => 0;

        public override int MaxY => _mazeData.Count;
        public override int MinY => 0;

        public RecursiveMaze(List<string> mapData) : base(TileType.Void)
        {
            Dictionary<string, List<Portal>> portalPairs = new Dictionary<string, List<Portal>>();

            foreach (string row in mapData)
            {
                _mazeData.Add(row.ToList());
            }

            #region Generate Maze
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
                                StartPosition = new Position3d(x, y, 0);
                            }
                            else if (portalId == "ZZ")
                            {
                                pType = PortalType.Exit;
                                ExitPosition = new Position3d(x, y, 0);
                            }
                            else if (x == this.MaxX - 3 || y >=(this.MaxY - 3) || x == 2 || y == 2)
                            {
                                pType = PortalType.Outside;
                            }
                            else
                            {
                                pType = PortalType.Inside;
                            }

                            t = TileType.Portal;

                            Portal portal = new Portal(x, y, portalId, pType);
                            if (!portalPairs.ContainsKey(portalId))
                            {
                                portalPairs.Add(portalId, new List<Portal>());
                            }
                            portalPairs[portalId].Add(portal);
                            _portals.Add(portal);

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
            #endregion

            #region Map Paths
            // Create Paths mappings
            foreach (string Id in portalPairs.Keys)
            {
                List<Portal> portalLink = portalPairs[Id];
                foreach (Portal p in portalLink)
                {
                    Paths.Add(p, new List<Path>());
                    Paths[p].AddRange(FindPortals(p));  // Add all links to the current portal
                }
                if (portalLink.Count == 2)
                {
                    // Add the link between portal pairs.
                    Paths[portalLink[0]].Add(new Path(portalLink[0], portalLink[1], 1, true));
                    Paths[portalLink[1]].Add(new Path(portalLink[1], portalLink[0], 1, true));
                }
            }
            #endregion
        }

        public TileType this[int x, int y, int layer] => this[new Position3d(x, y, layer)];

        public TileType this[Position3d position]
        {
            get
            {
                Position pos = new Position(position.X, position.Y);
                if (!_map.ContainsKey(pos))
                {
                    return DefaultValue;
                }
                else
                {
                    return base[pos];
                }
            }
        }

        public override string DrawMap()
        {
            int min_X = MinX;
            int min_Y = MinY;
            int max_X = MaxX;
            int max_Y = MaxY;

            StringBuilder map = new StringBuilder();
            for (int y = min_Y; y < max_Y; y++)
            {
                map.Append(Environment.NewLine);

                for (int x = min_X; x < max_X; x++)
                {
                    if (base[x,y] == TileType.Portal)
                    {
                        if (x == StartPosition.X && y == StartPosition.Y)
                        {
                            map.Append("S");
                        }
                        else if (x == ExitPosition.X && y == ExitPosition.Y)
                        {
                            map.Append("X");
                        }
                        else
                        {
                            map.Append("@");
                        }
                    }                    
                    else
                    {
                        map.Append(_mazeData[y][x]);
                    }
                }
            }
            return map.ToString();
        }

        public RecursiveMapNode FindExit()
        {
            var openList = new HashSet<RecursiveMapNode>();  // list of cells to be checked
            var closedList = new HashSet<RecursiveMapNode>();  // checked locations

            RecursiveMapNode current = new RecursiveMapNode(StartPosition);
            openList.Add(current);

            while (openList.Count > 0)
            {
                // get the closest square
                current = openList.OrderBy(l => l.DistanceFromStart).First();

                // move location to closed list
                closedList.Add(current);
                openList.Remove(current);

                // check if we've found an empty cell?
                if (current.X == ExitPosition.X && current.Y == ExitPosition.Y && current.Z == ExitPosition.Z) { return current; }

                var pos2d = new Position(current.X, current.Y);
                var pathsFromPosition = Paths[pos2d];  // Get all paths from this position

                foreach (Path path in pathsFromPosition)
                {
                    if (path.Destination.IsActive(current.Z + path.LayerModifier)) // no point moving to it if its not an active portal
                    {
                        Position3d target = new Position3d(path.Destination.X, path.Destination.Y, current.Z + path.LayerModifier);
                        RecursiveMapNode locationPos = new RecursiveMapNode(target)
                        {
                            Parent = current,
                            DistanceFromStart = current.DistanceFromStart + path.TotalDistance,
                            PathTaken = path
                        };

                        if (!TryUpdateList(closedList, locationPos) && !TryUpdateList(openList, locationPos))
                        {
                            openList.Add(locationPos);
                        }
                    }
                }
            }
            return null;  // no path found
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

        public override IEnumerable<Position> GetAvailableNeighbors(Position position)
        {
            return position.GetNeighboringPositions().Where(p => base[p] != TileType.Wall && base[p] != TileType.Void);
        }

        private bool TryUpdateList(HashSet<RecursiveMapNode> list, RecursiveMapNode currentPosition)
        {
            // update distance from start if we have a shorter path
            if (list.TryGetValue(currentPosition, out RecursiveMapNode p))
            {
                if (p.DistanceFromStart > currentPosition.DistanceFromStart + 1)
                {
                    p.DistanceFromStart = currentPosition.DistanceFromStart + 1;
                    p.Parent = currentPosition;
                    p.PathTaken = currentPosition.PathTaken;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        // Finds all portals directly accessable from the current portal. 
        // Ignore the link to partner portal - we just want the ones found by moving directly within the maze
        private HashSet<Path> FindPortals(Portal start)
        {
            var results = new HashSet<MapNode>();

            var openList = new HashSet<MapNode>();  // list of cells to be checked
            var closedList = new HashSet<MapNode>();  // checked locations

            MapNode current = new MapNode(start);

            //Add current position to open list to start searching
            openList.Add(current);

            while (openList.Count > 0)
            {
                // get the closest square
                current = openList.OrderBy(l => l.DistanceFromStart).First();

                // move location to closed list
                closedList.Add(current);
                openList.Remove(current);

                TileType tile = this[current];
                // check if we've found an empty cell?
                if (tile == TileType.Portal && current.DistanceFromStart > 0)
                {
                    // If its a key - add it to the list.  If its already in the list, then it should already have the shortest path
                    if (!results.Contains(current))
                    {
                        results.Add(current);
                    }
                }

                var neighbours = GetAvailableNeighbors(current);  // Get open (not wall or door) adjacent squares.

                // for every neighbour 
                // Check if its in closedList - if so its already been checked.
                // Otherwise add it to open list (or update if shorter path to a location in shorter path).
                foreach (var location in neighbours)
                {
                    MapNode locationPos = new MapNode(location)
                    {
                        Parent = current,
                        DistanceFromStart = current.DistanceFromStart + 1
                    };

                    if (!closedList.Contains(locationPos))
                    {
                        openList.Add(locationPos);
                    }
                }
            }

            HashSet<Path> distances = new HashSet<Path>();
            foreach (MapNode node in results)
            {
                Portal destination =_portals.Where(p => p.X == node.X && p.Y == node.Y).First();
                distances.Add( new Path(start, destination, node.DistanceFromStart, false));
            }
            return distances;
        }
    }
}
