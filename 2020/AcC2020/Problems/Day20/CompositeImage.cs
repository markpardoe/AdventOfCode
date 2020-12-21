using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common.Mapping;

namespace AoC.AoC2020.Problems.Day20
{
    public class CompositeImage 
    {
        public int GridSize { get; }
        private readonly FixedSizeMap<ImageMap> _images;
        private readonly HashSet<int> _tileId = new HashSet<int>();

        public bool IsComplete => GetNextFreePosition() == null;

        public CompositeImage(int gridSize)
        {
            GridSize = gridSize;
            _images = new FixedSizeMap<ImageMap>(null, new Position(0,0), new Position(gridSize - 1, gridSize - 1));
        }

        public bool TryAddImage(Position position, ImageMap img)
        {

            if (img == null) throw new ArgumentNullException(nameof(img));
            if (_images[position] != null)
            {
                throw new ArgumentException($"Position {position} is already occupied!");
            }

            // check if the image fits in the given space
            // by comparing rows to each of the neighboring spaces
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                Position p = position.Move(direction);
                ImageMap map = _images[p];

                if (map == null) {continue;}

                // Check if we can put the image here by comparing its edge with the adjourning edge on the neighbor
                // Eg.  This.TopEdge = TopNeighbor.BottomEdge
                string edge = img.GetSide(direction);
                string neighboringEdge = map.GetSide(Directions.OppositeDirection[direction]);
                if (!edge.Equals(neighboringEdge))
                {
                    return false;
                }
            }

            AddImage(position, img);
            return true;
        }

        public Position? GetNextFreePosition()
        {
            foreach (var location in _images.GetBoundedEnumerator())
            {
                if (location.Value == null)
                {
                    return location.Key;
                }
            }

            return null;
        }

        private void AddImage(Position postion, ImageMap img)
        {
            _tileId.Add(img.TileId);
            _images[postion] = img;
        }


        public CompositeImage Copy()
        {
            var result = new CompositeImage(GridSize);

            for (int y = 0; y < GridSize; y++)
            {
                for (int x = 0; x < GridSize; x++)
                {
                    if (_images[x, y] != null)
                    {
                        result.AddImage(new Position(x, y), _images[x, y]);
                    }
                }
            }

            return result;
        }

        public ulong GetTileIdTotal()
        {
            if (!IsComplete)
            {
                return 0;
            }
            return (ulong) _images[0, 0].TileId 
                 * (ulong) _images[0, GridSize-1].TileId 
                 * (ulong) _images[GridSize-1, GridSize-1].TileId *
                   (ulong) _images[GridSize-1, 0].TileId;
        }

        public string DrawMap()
        {
            if (!IsComplete)
            {
                return "Partial image found";
            }
            StringBuilder sb = new StringBuilder();
            var imageHeight = _images[0, 0].MaxY;
    

            // iterate across the grid
            for (int y =0; y < GridSize; y++)
            {
                for (int row = 0; row <= imageHeight; row++)
                {
                    sb.AppendLine();
                    for (int x = 0; x < GridSize; x++)
                    {
                        if (_images[x,y] == null)
                        {
                            sb.Append(new string(' ', imageHeight + 1));
                        }
                        else
                        {
                            sb.Append(_images[x, y].GetRowState(row));
                            sb.Append(" "); // space between grids
                        }

                    }
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
