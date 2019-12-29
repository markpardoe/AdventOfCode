using System;
using System.Collections.Generic;
using System.Text;
using AoC.Common.Mapping;
using System.Linq;

namespace AoC2019.Problems.Day17
{

    public enum ScaffoldType
    {
        Scaffold = 1,
        Empty = 2,
        Unknown = 0 ,
        Cleaned = 3
    }

    public class ScaffoldMap : Map<ScaffoldType>
    {
        public ScaffoldMap() :base(ScaffoldType.Unknown) { }
        
        public Position Robot { get; internal set; }
        public Direction RobotFacing { get; internal set; }


        public List<Position> FindAllIntersections()
        {
            List<Position> intersections = new List<Position>();           

            foreach (Position p in base.Keys)
            {
                if (this[p] == ScaffoldType.Scaffold)
                {
                    var neighbours = p.GetNeighbouringPositions();
                    if (neighbours.All(p => this[p] == ScaffoldType.Scaffold))
                    {
                        intersections.Add(p);
                    }
                }
            }          
            return intersections;
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
                    ScaffoldType tile = this[new Position(x, y)];
                    if (Robot.X == x && Robot.Y == y)
                    {
                        switch(RobotFacing)
                        {
                            case (Direction.Up): 
                                map.Append("^");
                                break;
                            case (Direction.Down):
                                map.Append("V");
                                break;
                            case (Direction.Left):
                                map.Append("<");
                                break;
                            case (Direction.Right):
                                map.Append(">");
                                break;
                            default:
                                map.Append("X");
                                break;
                        }
                    }
                    else if (tile == ScaffoldType.Empty)
                    {
                        map.Append(".");
                    }
                    else if (tile == ScaffoldType.Unknown)
                    {
                        map.Append("?");
                    }
                    else if (tile == ScaffoldType.Scaffold)
                    {
                        map.Append("#");
                    }

                    else if (tile == ScaffoldType.Cleaned)
                    {
                        map.Append("+");
                    }
                }
            }

            return map.ToString();
        }
    }
}
