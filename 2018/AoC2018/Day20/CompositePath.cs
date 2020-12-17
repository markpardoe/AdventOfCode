using System.Collections.Generic;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day20
{

    public interface IPath
    {
         IEnumerable<Position> Move(Position currentPosition);
    }

    public class Path : IPath
    {
        private readonly IEnumerable<CompassDirection> _directions;

        public Path(IEnumerable<CompassDirection> directions)
        {
            _directions = directions;
        }

        public IEnumerable<Position> Move(Position currentPosition)
        {
            throw new System.NotImplementedException();
        }
    }


    public class CompositePath : IPath
    {
        private readonly List<CompositePath> _paths = new List<CompositePath>();

        private readonly List<List<char>> _children = new List<List<char>>();
        public IReadOnlyCollection<List<char>> Children => _children;
        //    public int Index { get; } = 0;  // last index of data we used

        private int _index;
        public int EndIndex => _index;


        public CompositePath(List<char> inputData, int startIndex)
        {

            List<char> currentPath = new List<char>();

            for (_index = startIndex; _index <= inputData.Count; _index++)
            {
                char current = inputData[_index];

                //    switch (current)
                //    {
                //        case '(':
                //            // Start a new group - then store it in _paths and skip past the group in the input
                //            CompositePath compositePath = new CompositePath(inputData, _index + 1);
                //            _paths.Add(compositePath);
                //            _index = compositePath.EndIndex + 1;
                //            return;

                //        case ')':
                //            return;

                //            break;
                //        case '|':
                //            if (levelCount == 0)
                //            {
                //                // At top layer - so store current compositePath and start a new one
                //                _children.Add(currentPath);
                //                currentPath = new List<char>();
                //            }
                //            else
                //            {
                //                // otherwise just keep going
                //                currentPath.Add(current);
                //            }
                //            break;
                //        case '$':
                //            _children.Add(currentPath);
                //            return;
                //        default:  // must be N,S, E or W
                //            currentPath.Add(current);
                //            break;
                //    }

                //}

                if (currentPath.Count > 0)
                {
                    _children.Add(currentPath);
                }
            }
        }

        public IEnumerable<Position> Move(Position currentPosition)
        {
            throw new System.NotImplementedException();
        }
    }
}