using System;
using System.Collections.Generic;
using System.Text;
using AoC.Common.Mapping;

namespace AoC2019.Problems.Day10
{
    public class AsteroidMap
    {
        private readonly List<List<bool>> _map = new List<List<bool>>();
        public int Width { get; }
        public int Height { get; }
        public readonly HashSet<Position> Asteroids = new HashSet<Position>();

        public HashSet<Position> SearchGrid { get; } = new HashSet<Position>();

        public AsteroidMap(List<string> data)
        {
            for (int i = 0; i< data.Count; i++) 
            {
                List<bool> rowData = new List<bool>();
                string row = data[i];
                for (int j = 0; j < row.Length; j++)
                {
                    char c = row[j];
                    if (c == '#')
                    {
                        rowData.Add(true);
                        Asteroids.Add(new Position(j, i));
                    }
                    else
                    {
                        rowData.Add(false);
                    }
                   
                }
                _map.Add(rowData);

                this.Width = _map.Count;
                this.Height = _map[0].Count;                
            }

            GenerateSearchGrid();
        }

        private void GenerateSearchGrid()
        {
            SearchGrid.Add(new Position(0, 1));
            SearchGrid.Add(new Position(1, 0));
            SearchGrid.Add(new Position(0, -1));
            SearchGrid.Add(new Position(-1, 0));
            
            for (int i = 1; i <= Height; i++)
            {
                for (int j = 1; j <= Width; j++)
                {
                    int gcd = this.Gcd(i, j);
                    int gcd_X = i / gcd;
                    int gcd_Y = j / gcd;

                    SearchGrid.Add(new Position(gcd_X, -gcd_Y));
                    SearchGrid.Add(new Position(gcd_X, gcd_Y));

                    SearchGrid.Add(new Position(-gcd_X, gcd_Y));

                    SearchGrid.Add(new Position(-gcd_X, -gcd_Y));
                }
            }
        }

        public int CountVisibleAsteroids(Position asteroid)
        {
            return FindVisibleAsteroids(asteroid).Count;
        }

        public List<Position> FindVisibleAsteroids(Position asteroid)
        {
            List<Position> found = new List<Position>();
            foreach (Position p in SearchGrid)
            {
            //    Console.WriteLine("Search Grid:" + p.ToString());
                for (int i = 1; i <= Width; i++)
                {
                    Position ToCheck = new Position(asteroid.X + (p.X * i), asteroid.Y + (p.Y * i));
               //     Console.WriteLine("    Checking Against: " + ToCheck.ToString());
                    if (InMap(ToCheck))
                    {
                        if (_map[ToCheck.Y][ToCheck.X])
                        {
                   //         Console.WriteLine("        Asteroid found: " + ToCheck.ToString());
                            found.Add(ToCheck);
                            break;
                        }
                    }
                    else
                    {
                   //     Console.WriteLine("        Out Of bounds " + ToCheck.ToString());
                        break;
                    }
                }
            }
            return found;
        }

        private bool InMap(int x, int y)
        {
            return (x < Height && x >= 0 && y < Width && y >= 0);
        }

        private bool InMap(Position p)
        {
            return InMap(p.X, p.Y);
        }

        public void DestroyAsteroid(Position position)
        {
            if (_map[position.Y][position.X] != true)
            {
                throw new InvalidOperationException($"Asteroid at {position.ToString()} does not exist!");
            }
            else
            {
                _map[position.Y][position.X] = false;
                Asteroids.Remove(position);
            }
        }


        private int Gcd(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            return a == 0 ? b : a;
        }

        public string DrawMap()
        {
            StringBuilder b = new StringBuilder();

            foreach (List<bool> rows in _map)
            {
                foreach (bool value in rows)
                {
                    if (value)
                    {
                        b.Append("#");
                    }
                    else
                    {
                        b.Append(".");
                    }
                }
                b.Append(Environment.NewLine);
            }

            return b.ToString();
        }
    }
}
