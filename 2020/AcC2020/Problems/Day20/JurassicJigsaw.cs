using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;
using AoC.Common.Mapping;

namespace AoC.AoC2020.Problems.Day20
{
    public class JurassicJigsaw :AoCSolution<ulong>
    {
        public override int Year => 2020;
        public override int Day => 20;
        public override string Name => "Day 20: Jurassic Jigsaw";
        public override string InputFileName => "Day20.txt";

        public override IEnumerable<ulong> Solve(IEnumerable<string> input)
        {
            var tiles = ParseMaps(input).ToList();
            int gridSize = (int)Math.Sqrt(tiles.Count);
            var data = GetMapVariants(tiles);
            yield return SolvePartA(data, gridSize);
        }

        public ulong SolvePartA(IEnumerable<ImageMap> images, int gridSize)
        {
            CompositeImage img = new CompositeImage(gridSize);
            var result =  CreateImage(img, images.ToList());

            Console.WriteLine(result.DrawMap());
            return result.GetTileIdTotal();
        }

        private CompositeImage CreateImage(CompositeImage compositeImage, IReadOnlyList<ImageMap> images)
        {
            if (compositeImage.IsComplete) return compositeImage;

            // Where to place the next image
            Position? nextPosition = compositeImage.GetNextFreePosition();
            if (nextPosition == null) return null;

            // Try each of the images in the free position
            foreach (var img in images)
            {
                var newImage = compositeImage.Copy();
                if (newImage.TryAddImage(nextPosition.Value, img))
                {
                    newImage = CreateImage(newImage, images.Where(x => x.TileId != img.TileId).ToList());
                    // we must have found a result... so return it
                    if (newImage != null)
                    {
                        return newImage;
                    }
                }
            }
            return null;
        }

        // Gets the 8 versions of the map - rotated (x4) and flipped and rotated (x4)
        private IEnumerable<ImageMap> GetMapVariants(IEnumerable<ImageMap> images)
        {
            HashSet<ImageMap> maps = new HashSet<ImageMap>() { };
            int count = 0;
            foreach (var img in images)
            {
                count++;
                maps.Add(img);

                var tmp = img;
                for (int i = 0; i < 3; i++)
                {
                    tmp = tmp.RotateRight();
                    var r = maps.Add(tmp);
                }

                tmp = img.Flip();
                var f = maps.Add(tmp);

                for (int i = 0; i < 3; i++)
                {
                    tmp = tmp.RotateRight();
                    var t = maps.Add(tmp);
                }
            }

            return maps;
        }

        private IEnumerable<ImageMap> ParseMaps(IEnumerable<string> rawData)
        {
            List<string> current = new List<string>();
            int tileId = 0;
            foreach (string line in rawData)
            {
                // start a new tile
                if (line.StartsWith("Tile "))
                {
                    if (current.Count > 0)
                    {
                        yield return new ImageMap(tileId, current);
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

            // return final image
            if (current.Count > 0)
            {
                yield return new ImageMap(tileId, current);
            }
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
            "#...##.#..",
            "",
            "Tile 1171:",
            "####...##.",
            "#..##.#..#",
            "##.#..#.#.",
            ".###.####.",
            "..###.####",
            ".##....##.",
            ".#...####.",
            "#.##.####.",
            "####..#...",
            ".....##...",
            "",
            "Tile 1427:",
            "###.##.#..",
            ".#..#.##..",
            ".#.##.#..#",
            "#.#.#.##.#",
            "....#...##",
            "...##..##.",
            "...#.#####",
            ".#.####.#.",
            "..#..###.#",
            "..##.#..#.",
            "",
            "Tile 1489:",
            "##.#.#....",
            "..##...#..",
            ".##..##...",
            "..#...#...",
            "#####...#.",
            "#..#.#.#.#",
            "...#.#.#..",
            "##.#...##.",
            "..##.##.##",
            "###.##.#..",
            "",
            "Tile 2473:",
            "#....####.",
            "#..#.##...",
            "#.##..#...",
            "######.#.#",
            ".#...#.#.#",
            ".#########",
            ".###.#..#.",
            "########.#",
            "##...##.#.",
            "..###.#.#.",
            "",
            "Tile 2971:",
            "..#.#....#",
            "#...###...",
            "#.#.###...",
            "##.##..#..",
            ".#####..##",
            ".#..####.#",
            "#..#.#..#.",
            "..####.###",
            "..#.#.###.",
            "...#.#.#.#",
            "",
            "Tile 2729:",
            "...#.#.#.#",
            "####.#....",
            "..#.#.....",
            "....#..#.#",
            ".##..##.#.",
            ".#.####...",
            "####.#.#..",
            "##.####...",
            "##..#.##..",
            "#.##...##.",
            "",
            "Tile 3079:",
            "#.#.#####.",
            ".#..######",
            "..#.......",
            "######....",
            "####.#..#.",
            ".#...#.##.",
            "#.#####.##",
            "..#.###...",
            "..#.......",
            "..#.###..."
        };
    }
}
