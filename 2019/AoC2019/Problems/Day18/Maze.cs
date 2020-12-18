using AoC.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc.AoC2019.Problems.Day18
{

    public class Maze : Map<MazeTile>
    {
        public HashSet<MazeTile> KeyPositions { get; } = new HashSet<MazeTile>();
        public HashSet<MazeTile> DoorPositions { get; } = new HashSet<MazeTile>();
        public List<MazeTile> StartPositions { get; } = new List<MazeTile>();

        // Working out movements per tile takes too long - we only need to know the distances between 2 keys (and any doors on the way).
        // This is a Dictionary <Position of a key, <Distance to / location of all other keys>
        public readonly Dictionary<Position, HashSet<KeyDistance>> KeyDistances = new Dictionary<Position, HashSet<KeyDistance>>(); // Distance from <Key to Key>

        /// <summary>
        /// Key based maze.
        /// </summary>
        /// <param name="mapData"></param>
        public Maze(IEnumerable<string> mapData) : base(null)
        {
            int y = 0;
            foreach (string row in mapData)
            {
                int x = 0;
                foreach (char c in row)
                {
                    Position p = new Position(x, y);

                    MazeTile tile = new MazeTile(x, y, c);
                    this.Add(tile, tile);


                    if (tile.Tile == TileType.Key)
                    {
                        KeyPositions.Add(tile);
                    }
                    if (tile.Tile == TileType.Door)
                    {
                        DoorPositions.Add(tile);
                    }
                    if (c == '@')
                    {
                        StartPositions.Add(tile);
                    }
                    x++;
                }
                y++;
            }

            // Cache distances between keys.
            foreach (MazeTile origin in KeyPositions)
            {
                HashSet<KeyDistance> distances = FindKeys(origin);
                KeyDistances.Add(origin, distances);
            }
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
                    map.Append(this[x, y].MapValue);

                }
            }
            return map.ToString();
        }

        public string DrawMap(params Position[] robots)
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
                    if (robots.Contains(new Position(x, y)))
                    {
                        map.Append("@");
                    }
                    else
                    {
                        map.Append(this[x, y].MapValue);
                    }

                }
            }
            return map.ToString();
        }

        // Find all other keys that can be moved to from the given position.
        // This ignores doors for the path finding - the returned KeyDistance will contain the doors that need to be opened en-route.
        // This uses a breadth first search to check the entire maze - guarenteeing the shortest-path to every other key.
        public HashSet<KeyDistance> FindKeys(MazeTile start)
        {
            var results = new Dictionary<string, MapNode>();

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

                MazeTile tile = this[current];
                // check if we've found an empty cell?
                if (tile.Tile == TileType.Key && current.DistanceFromStart > 0)
                {                 
                    // If its a key - add it to the list.  If its already in the list, then it should already have the shortest path
                    if (!results.ContainsKey(tile.KeyId))
                    {
                        results.Add(tile.KeyId, current);
                    }
                }

                // Get open (not wall) adjacent squares
                var neighbors = this.GetAvailableNeighbors(current);  

                // for every neighbour 
                // Check if its in closedList - if so its already been checked.
                // Otherwise add it to open list (or update if shorter path to a location in shorter path).
                foreach (var location in neighbors)
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

            // Need to convert the paths to each key into a distance.
            HashSet<KeyDistance> distances = new HashSet<KeyDistance>();

            foreach (string key in results.Keys)
            {
                MapNode node = results[key];
                KeyDistance d = new KeyDistance(start, this[node], node);
                node = node.Parent;

                // loop through list checking if any keys are required.
                while (node.Parent != null)
                {                    
                    MazeTile tile = this[node];
                    if (tile.Tile == TileType.Door)
                    {
                        d.Doors.Add(tile.KeyId);  
                    }
                    else if (tile.Tile == TileType.Key)
                    {
                        // See if we picked up extra keys on the way
                        d.ExtraKeys.Add(tile.KeyId);
                    }

                    node = node.Parent;
                }

                distances.Add(d);
            }           
            return distances;
        }

        /// <summary>
        /// Returns the neighbouring Positions we can move into - which aren't walls.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public override IEnumerable<Position> GetAvailableNeighbors(Position position)
        {
            return position.GetNeighboringPositions().Where(x => this[x] != null && this[x].Tile != TileType.Wall);
        }
    }
}
