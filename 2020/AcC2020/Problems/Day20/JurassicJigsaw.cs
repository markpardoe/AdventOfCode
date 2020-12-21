using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
            var data = tiles.SelectMany(x => GetMapVariants(x));
 
            CompositeImage img = new CompositeImage(gridSize);
            var result = CreateImage(img, data.ToList());

           // Console.WriteLine(result.DrawMap());
            yield return result.GetTileIdTotal();

            // Convert the compositeImage into one image (with borders removed)
            ImageMap finalImage = result.ExtractMap();

            yield return (ulong) FindMonsters(finalImage);
        }

        private long FindMonsters(ImageMap image)
        {
            // monsters could be in any rotation / flip of the map
            var finalImageGroup = GetMapVariants(image);

            foreach (var i in finalImageGroup)
            {
                int monsters = CountMonsters(i);
                if (monsters > 0)
                {
                    Console.WriteLine($"Monsters: {monsters}");
                    Console.WriteLine(i.DrawMap());
                    return i.CountValue(WaterPixel.Water);
                }
            }

            return 0;
        }

        private CompositeImage CreateImage(CompositeImage compositeImage, IReadOnlyList<ImageMap> images)
        {
            // If all spaces full - then we're done!
            if (compositeImage.IsComplete) return compositeImage;

            // Where to place the next image
            Position? nextPosition = compositeImage.GetNextFreePosition();
            if (nextPosition == null) return null;

            // Try each of the images in the free position
            foreach (var img in images)
            {
                var newImage = compositeImage.Copy();
                // If we can add a tile into the next empty spot 
                // Use recursion to try and add the next tile.. and so on until completed
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
        private IEnumerable<ImageMap> GetMapVariants(ImageMap image)
        {
            yield return image;
            var tmp = image;

            for (int i = 0; i < 3; i++)
            {
                tmp = tmp.RotateRight();
                yield return tmp;
            }

            tmp = image.Flip();
            yield return tmp;

            for (int i = 0; i < 3; i++)
            {
                tmp = tmp.RotateRight();
                yield return tmp;
            }
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

        // Relative co-ordinates for a sea monster based off 0,0 as first tile
        private IEnumerable<(int x, int y)> GetSeaMonster()
        {
            yield return (0, 0);
            yield return (18, -1);      // check out-of range values first to exit quickly
            yield return (19, 0);
            yield return (1, 1);
            yield return (4, 1);
            yield return (5, 0);
            yield return (6, 0);
            yield return (7, 1);
            yield return (10, 1);
            yield return (11, 0);
            yield return (12, 0);
            yield return (13, 1);
            yield return (16, 1);
            yield return (17, 0);
            yield return (18, 0);
        }

        private bool IsMonster(Position currentPosition, ImageMap map)
        {
            foreach (var monster in GetSeaMonster())
            {
                if (map[currentPosition.X + monster.x, currentPosition.Y + monster.y] != WaterPixel.Water)
                {
                    return false;
                }
            }
            return true;
        }

        private void DrawMonster(Position currentPosition, ImageMap map)
        {
            foreach (var monster in GetSeaMonster())
            {
                map[currentPosition.X + monster.x, currentPosition.Y + monster.y] = WaterPixel.Monster;
            }
        }

        private int CountMonsters(ImageMap map)
        {
            int count = 0;
            var locations = map.ToList();
            foreach (var location in locations)
            {
                if (location.Value == WaterPixel.Water)
                {
                    if (IsMonster(location.Key, map))
                    {
                        count++;
                        DrawMonster(location.Key, map);
                    }
                }
            }

            return count;
        }

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
