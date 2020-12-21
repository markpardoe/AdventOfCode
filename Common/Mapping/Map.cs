using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections;

namespace AoC.Common.Mapping
{
    /// <summary>
    /// Map of 2d Positions
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class Map<TValue> : BufferedMap<Position, TValue>
    {
        public virtual int MaxX => Map.Keys.Max(p => p.X);
        public virtual int MinX => Map.Keys.Min(p => p.X);
        public virtual int MaxY => Map.Keys.Max(p => p.Y);
        public virtual int MinY => Map.Keys.Min(p => p.Y);

        // Function to use for converting the value to a char for mapping purposes
        // Override for custom mapping
        protected virtual char? ConvertValueToChar(Position position, TValue value) => value?.ToString()?[0];

        public Map(TValue defaultValue) : base(defaultValue)
        { }
                
        public void Add(int x, int y, TValue value) => Add(new Position(x, y), value);
        
        public TValue this[int x, int y]
        {
            get => this[new Position(x, y)];
            set => this[new Position(x, y)] = value;
        }

        protected MapBoundary GetMapBoundaries(int padding = 0)  => new MapBoundary(this, padding);

        /// <summary>
        /// Returns the 4 positions surrounding the specified point for moving into.
        /// Can be overwritten to use a different criteria / selection when using the search algorithms
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        protected override IEnumerable<Position> GetAvailableNeighbors(Position position)
        {
            return position.GetNeighboringPositions();
        }

        public override string DrawMap(int padding = 0)
        {
            var bounds = GetMapBoundaries(padding);
            StringBuilder sb = new StringBuilder();

            for (int y = bounds.MinY; y <= bounds.MaxY; y++)
            {
                for (int x = bounds.MinX; x <= bounds.MaxX; x++)
                {
                    Position pos = new Position(x, y);
                    sb.Append(ConvertValueToChar(pos, this[pos]));
                }
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        // Returns all positions within the region of the map (between min and max bounds)
        // Positions are returned in order from left to right, then top to bottom.
        // Will return positions within bounds that are not keys in the map collection
        public override IEnumerable<KeyValuePair<Position, TValue>> GetBoundedEnumerator(int padding = 0)
        {
            var bounds  = GetMapBoundaries(padding);

            for (int y = bounds.MinY; y <= bounds.MaxY; y++)
            {
                for (int x = bounds.MinX; x <= bounds.MaxX; x++)
                {
                    Position pos = new Position(x, y);
                    yield return new KeyValuePair<Position, TValue>(pos, this[pos]);
                }
            }
        }

        /// <summary>
        /// Performs a BFS to find the nearest instance of value in the map.
        /// Assumes an unweighted map (ie. distance/cost between points always = 1).
        /// Relies on virtual method [GetAvailableNeighbors] to filter the neighboring positions before adding them to the search list.
        /// By default this returns the 4 points in the 4 cardinal directions (not diagonal)
        /// </summary>
        /// <param name="start"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public MapNode ShortestPathToNearestValue(Position start, TValue value)
        {
            var targets = Map.Where(x => x.Value.Equals(value));
            return ShortestPathToPosition(start, targets.Select(x => x.Key)).FirstOrDefault();
        }

        /// <summary>
        /// Performs a BFS to find shortest path to every target in the input
        /// Assumes an unweighted map (ie. distance/cost between points always = 1).
        /// Relies on virtual method [GetAvailableNeighbors] to filter the neighboring positions before adding them to the search list.
        /// By default this returns the 4 points in the 4 cardinal directions (not diagonal)
        /// </summary>
        /// <param name="start"></param>
        /// <param name="targets"></param>
        /// <returns></returns>
        public IEnumerable<MapNode> ShortestPathToPosition(Position start, IEnumerable<Position> targets)
        {
            // Create a copy of targets to avoid modifying in place
            HashSet<Position> targetList = new HashSet<Position>(targets);            
            Queue<MapNode> openList = new Queue<MapNode>();
            HashSet<MapNode> closedList = new HashSet<MapNode>();

            openList.Enqueue(new MapNode(start));

            while (openList.Count > 0)
            {
                // get the 1st node
                var currentPosition = openList.Dequeue();

                var neighbors = GetAvailableNeighbors(currentPosition.Position)
                    .Select(x => new MapNode(x, currentPosition, currentPosition.DistanceFromStart + 1));

                foreach (var n in neighbors)
                {
                    // We've reached a target - BFS guarentees its the shortest path
                    if (targetList.Contains(currentPosition.Position))
                    {

                        yield return currentPosition;

                        targetList.Remove(currentPosition.Position);

                        // found all the targets - so quit
                        if (targetList.Count == 0)
                        {
                            yield break;
                        }
                    }
                    // Check if we've already visited the node (or about to visit it)
                    // BFS guarantee shortest-path, so we don't have to check if the new distance is shorter
                    if (closedList.Contains(n) || openList.Contains(n))
                    {
                        // Already got a shorter path to this position
                        continue;
                    }
                    openList.Enqueue(n);
                }
                closedList.Add(currentPosition);
            }
            yield break;
        }

        /// <summary>
        /// Performs a BFS to find shortest path to every location where the location contains the specified value
        /// Assumes an unweighted map (ie. distance/cost between points always = 1).
        /// Relies on virtual method [GetAvailableNeighbors] to filter the neighboring positions before adding them to the search list.
        /// By default this returns the 4 points in the 4 cardinal directions (not diagonal)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public IEnumerable<MapNode> ShortestPathToValue(Position start, TValue value)
        {
            var targets = Map.Where(x => x.Value.Equals(value));

            return ShortestPathToPosition(start, targets.Select(x => x.Key));
        }
              
        /// <summary>
        /// Finds the shortest path from start to target positions using A* search algorithm
        /// https://www.geeksforgeeks.org/a-search-algorithm/
        /// Assumes an unweighted graph - so movement between neighboring points always costs '1'
        /// Relies on virtual method [GetAvailableNeighbors] to filter the neighboring positions before adding them to the search list.
        /// By default this returns the 4 points in the 4 cardinal directions (not diagonal)
        /// </summary>
        /// <param name="start"></param>
        /// <param name="target"></param>
        public MapNode ShortestPathToPosition(Position start, Position target)
        {
            HashSet<MapNode> openList = new HashSet<MapNode>() { new MapNode(new Position(start.X, start.Y))};
            HashSet<MapNode> closedList = new HashSet<MapNode>();

            while (openList.Count > 0)
            {
                // get the node with the shortest distance
                var currentPosition = openList.OrderBy(x => x.TotalDistance).First();
                openList.Remove(currentPosition);

                // Console.WriteLine($"Checking {currentPosition}");

                var neighbors = GetAvailableNeighbors(currentPosition.Position)
                    .Select(x => new MapNode(x, currentPosition, currentPosition.DistanceFromStart + 1)).ToList();

                foreach (var n in neighbors)
                {
                    n.CalculatedDistanceToTarget = n.Position.DistanceTo(target);
                    if (n.X == target.X && n.Y == target.Y)
                    {
                        return n;
                    }

                    // Check if node already in open list with a lower TotalDistance
                    if (openList.Any(x => x.X == n.X && x.Y == n.Y && x.TotalDistance <= n.TotalDistance))
                    {
                        // Already got a shorter path to this position
                        continue;
                    }

                    // Check if node with same position in closedList with lower distance (ie. we've already found a faster way to this position)
                    // Check if node already in open list with a lower TotalDistance
                    if (closedList.Any(x => x.X == n.X && x.Y == n.Y && x.TotalDistance <= n.TotalDistance))
                    {
                        // Already got a shorter path to this position
                        continue;
                    }

                    openList.Add(n);
                }

                closedList.Add(currentPosition);
            }

            return null; // no destination found
        }

        // Helper class for caching map boundaries.
        // Calculating them is expensive on large maps - so we don't want to recalculate more than neccessary
        protected readonly struct MapBoundary
        {
            public int MaxX { get; }
            public int MinX { get; }
            public int MinY { get; }
            public int MaxY { get; }

            public MapBoundary(Map<TValue> map, int padding = 0)
            {
                MaxX = map.MaxX + padding;
                MinX = map.MinX - padding;
                MaxY = map.MaxY + padding;
                MinY = map.MinY - padding;
            }

            public IEnumerator<Position> GetEnumerator()
            {
                for (int y = MinY; y <= MaxY; y++)
                {
                    for (int x = MinX; x <= MaxX; x++)
                    {
                        yield return new Position(x, y);
                    }
                }
            }         
        }
    }
}