using AoC.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc.Aoc2018.Day22
{
    public enum Equipment
    {
        Neither,
        Torch,
        ClimbingGear
    }


    public class Explorer
    {
        private const int SwapTime = 7;

        // Which piece of equipment CAN'T be used in each area.
        private readonly Dictionary<RegionType, Equipment> _invalidEquipment = new Dictionary<RegionType, Equipment>()
        {
            {RegionType.Rocky, Equipment.Neither},
            {RegionType.Narrow, Equipment.ClimbingGear},
            {RegionType.Wet, Equipment.Torch}
        };

        private readonly CaveMap _map;
        public Position CurrentPosition { get; private set; }= new Position(0,0);


        public Explorer(CaveMap map)
        {
            _map = map ?? throw new ArgumentNullException(nameof(map));
        }

        public int FindShortestPath(Position start, Position target)
        {
            CaveNode node = new CaveNode(start, Equipment.Torch)
                {
                    DistanceToTarget = start.DistanceTo(target)
                }; // start with the torch

            // holds the shortest (found) distance to each node
            Dictionary<CaveNode, int> shortestPaths = new Dictionary<CaveNode, int>() {{node, 0}};
            HashSet<CaveNode> toCheck = new HashSet<CaveNode>() {node};  // Set of nodes to be checked

            while (toCheck.Count > 0)
            { 
                node = toCheck.OrderBy(x => x.TotalDistance).First();  // Get node with shortest path
                var currentRegion = _map[node.Position];

                toCheck.Remove(node);
             //   Console.WriteLine($"Checking: {node}");

                // Found result - so exit
                if (node.Position.Equals(target))
                {
                //    Console.Out.WriteLine("Path Found:");
                //    Console.WriteLine(node.WritePath());
                    return node.DistanceFromStart;
                }

                // Get neighboring positions - ignoring invalid co-ordinates (x & y must both be >=0)
                var neighboringPositions = node.Position.GetNeighboringPositions().Where(n => n.X >=0 && n.Y >=0);
                
                List<CaveNode> nodesToAdd = new List<CaveNode>();  // new CaveNodes we need to add to processing list

                foreach (var neighbor in neighboringPositions)
                {
                    var neighborRegion = _map[neighbor];
                    if (neighborRegion == null) // Have to make the map bigger
                    {
                        _map.ExpandRegionMap(neighbor.X, neighbor.Y);
                        neighborRegion = _map[neighbor];
                    }

                    // If moving to a different region type - then we need to swap equipment
                    Equipment equip = node.Equiped;
                    if (currentRegion.RegionType != neighborRegion.RegionType)
                    { 
                        equip = FindValidEquipment(currentRegion.RegionType, neighborRegion.RegionType);
                    }
  
                    if (equip == null) continue;  // no valid equipment found - so we can't move
                    
                    CaveNode neighborNode = new CaveNode(neighbor, equip)
                    {
                        DistanceFromStart = node.DistanceFromStart + (equip == node.Equiped ? 1 : 1 + SwapTime),
                        DistanceToTarget = neighbor.DistanceTo(target),
                        Parent = node
                    };

                    nodesToAdd.Add(neighborNode);
                }

                // Add new nodes to the queue
                foreach (var newNode in nodesToAdd)
                {

                    // have to be holding torch at the end
                    if (newNode.Position.DistanceTo(target) == 0 && newNode.Equiped != Equipment.Torch)
                    {
                        newNode.Equiped = Equipment.Torch;
                        newNode.DistanceFromStart += SwapTime;
                    }

                    // check if we've already found a shorter path to the current node
                    // Only keep new node if the new path is shorter
                    if (shortestPaths.ContainsKey(newNode))
                    {
                        int shortest = shortestPaths[newNode];
                        // we've got a new shortest distance to the node
                        if (newNode.TotalDistance  < shortest)
                        {
                            shortestPaths[newNode] = newNode.TotalDistance;
                            toCheck.Remove(newNode);  // just in case a longer path version is already in there
                            toCheck.Add(newNode);
                        }
                    }
                    else
                    {
                        shortestPaths[newNode] = newNode.TotalDistance;
                        toCheck.Add(newNode);
                    }
                }
            }

            return -1;  // no path found
        }

        // Find the item of equipment that is valid for both regions
        private Equipment FindValidEquipment(RegionType region1, RegionType region2)
        {
            foreach (Equipment equip in Enum.GetValues(typeof(Equipment)))
            {
                if (_invalidEquipment[region1] != equip && _invalidEquipment[region2] != equip)
                {
                    return equip;
                }
            }

            throw new Exception("No equipment found");
        }

        private bool AddNodeToList(HashSet<CaveNode> list, CaveNode node)
        {
            // update distance from start if we have a shorter path
            if (list.TryGetValue(node, out CaveNode existingNode))
            {
                if (existingNode.DistanceFromStart > node.DistanceFromStart)
                {
                    existingNode.DistanceFromStart = node.DistanceFromStart;
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        private class CaveNode : IEquatable<CaveNode>
        {
            public int DistanceFromStart { get; set; } = 0;
            public int DistanceToTarget { get; set; } = 0;
            public int TotalDistance => DistanceFromStart + DistanceToTarget;
            public Equipment Equiped { get; set; }
            public Position Position { get; }
            public CaveNode Parent { get; set; }

            public CaveNode(int x, int y, Equipment equipment) : this(new Position(x, y), equipment) { }

            public CaveNode(Position position, Equipment equipment) 
            {
                Position = position;
                Equiped = equipment;
            }

            public bool Equals(CaveNode other)
            {
                if (other == null) return false;
                return this.Position.Equals(other.Position) && this.Equiped == other.Equiped;
            }

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                if (obj is CaveNode) return (Equals((CaveNode)obj));
                return false;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(this.Position.X, this.Position.Y, this.Equiped);
            }

            public override string ToString()
            {
                return $"{Position} Distance From Start = {DistanceFromStart}, Total = {TotalDistance}, Equipped = {Equiped}";
            }

            public string WritePath()
            {
                var node = this;
                List<CaveNode> allNodes = new List<CaveNode>() { node };

                while (node.Parent != null)
                {
                    node = node.Parent;
                    allNodes.Add(node);
                }

                StringBuilder sb = new StringBuilder();
                for (int i = allNodes.Count - 1; i >= 0; i--)
                {
                    sb.AppendLine(allNodes[i].ToString());
                }

                return sb.ToString();

            }
        }
    }

}
