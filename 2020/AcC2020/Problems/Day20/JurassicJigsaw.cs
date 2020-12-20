using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;

namespace AoC.AoC2020.Problems.Day20
{
    public class JurassicJigsaw :AoCSolution<long>
    {
        public override IEnumerable<long> Solve(IEnumerable<string> input)
        {
            var data = ParseMaps(_simpleMap);

            foreach (var img in data)
            {
                Console.WriteLine();
                Console.WriteLine($"Tile: {img.TileId}");
                Console.WriteLine(img.DrawMap());
            }

            yield return 5;
        }

        public override int Year => 2020;
        public override int Day => 20;
        public override string Name => "Day 20: Jurassic Jigsaw";
        public override string InputFileName => "Day20.txt";


        private IEnumerable<ImageMap> ParseMaps(IEnumerable<string> rawData)
        {
            List<ImageMap> maps = new List<ImageMap>();
  
            List<string> current = new List<string>();
            int tileId = 0;
            foreach (string line in rawData)
            {
                // start a new tile
                if (line.StartsWith("Tile "))
                {
                    if (current.Count > 0)
                    {
                        var img = new ImageMap(tileId, current);
                        maps.AddRange(GetMapVariants(img));
                    }
                    // get new tileId
                    tileId = int.Parse(line.Split(' ')[1].Replace(":", "").Trim());
                    current = new List<string>();
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        current.Add(line);
                    }
                }
            }

            if (current.Count > 0)
            {
                var img = new ImageMap(tileId, current);
                maps.AddRange(GetMapVariants(img));
            }

            return maps;
        }

        // Gets the 8 versions of the map - rotated (x4) and flipped and rotated (x4)
        private IEnumerable<ImageMap> GetMapVariants(ImageMap map)
        {
            HashSet<ImageMap> maps = new HashSet<ImageMap>() {map};

            var img = map;
            for (int i = 0; i < 3; i++)
            {
                img = img.RotateRight();
                maps.Add(img);
            }

            img = map.Flip();
            maps.Add(img);
            for (int i = 0; i < 3; i++)
            {
                img = img.RotateRight();
                maps.Add(img);
            }
            return maps;
        }

        private readonly IEnumerable<string> _simpleMap = new List<string>()
        {
            "Tile 0:",
            "..##",
            "#..#",
            ".#.#",
            "#..#"
        };

        private readonly IEnumerable<string> _example1 = new List<string>()
        {
            "Tile 2311:",
            "..##.#..#.",
            "##..#.....",
            "#...##..#.",
            "####.#...#",
            "##.##.###.",
            "##...#.###",
            ".#.#.#..##",
            "..#....#..",
            "###...#.#.",
            "..###..###",
            "",
            "Tile 1951:",
            "#.##...##.",
            "#.####...#",
            ".....#..##",
            "#...######",
            ".##.#....#",
            ".###.#####",
            "###.##.##.",
            ".###....#.",
            "..#.#..#.#",
            "#...##.#.."
        };
    }
}
