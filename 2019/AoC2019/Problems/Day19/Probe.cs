using Aoc.AoC2019.IntCode;
using AoC.Common.Mapping;
using System;
using System.Collections.Generic;

namespace Aoc.AoC2019.Problems.Day19
{
    public class Probe
    {
        private readonly IEnumerable<long> _software;
        private readonly TractorBeamMap _map;

        public Probe(TractorBeamMap map, IEnumerable<long> probeSoftware)
        {
            _software = probeSoftware ?? throw new ArgumentNullException(nameof(probeSoftware));
            _map = map ?? throw new ArgumentNullException(nameof(map));
        }

        public void ScanMap(int radius)
        {
            int leftEdge = 0;
            for (int y = 0; y < radius; y++)
            {
                bool leftEdgeFound = false;
                for (int x = leftEdge; x < radius; x++)
                {
                    Position p = new Position(x, y);
                    BeamStatus b = CheckPosition(x,y);
                    _map.Add(p, b);

                    if (!leftEdgeFound && b == BeamStatus.Pulling)
                    {
                        leftEdgeFound = true;
                        leftEdge = x;
                    }
                    if (leftEdgeFound && b == BeamStatus.Stationary)
                    {
                        // reached right edge
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Finds the nearest location of a ship of given dimensions
        /// where the ship is completely within the tractor beam area.
        /// Rather than checking every tile, we can just trace the left edge of the tractor beam and
        /// for every tile (on the left edge) check if its within beam area.  
        /// We can then simply check an tile <size> upwards and <size> to the right. 
        /// (ie. left-edge is bottom left corner - we just need to check top-right corner)
        /// Since beam runs left-to-right and downwards, if these 2 corners are within the beam, 
        /// the other 2 are guarenteed to also be within the beam.
        /// </summary>
        /// <param name="shipSize"></param>
        /// <returns></returns>
        public Position FindShip(int shipSize)
        {
            int leftEdge = 0;
            int y = 10;

            while (true)  // keep moving downwards until we find the sship
            {
                // We can start each X co-ordinate at column of the last edge found.  
                // Since beams run left-to-right, we know it has to be either the same or to the right
                int x = leftEdge;
                while (true) // move across until we find the left edge
                {
                    BeamStatus b = CheckPosition(x, y);
                    if (b == BeamStatus.Pulling)
                    { 
                        // Left-edge found
                        leftEdge = x;
                        if (CheckShip(x,y, shipSize))
                        {
                            return new Position(x, y - (shipSize - 1));
                        }
                        break;
                    }

                    x++;
                }
                y++;
            }
        }

        private bool CheckShip(int x, int y, int shipSize)
        {
            // w're taking the bottom left co-ordinate and checking against the upper right
            return CheckPosition(x + (shipSize - 1), y - (shipSize - 1)) == BeamStatus.Pulling;
        }


        public BeamStatus CheckPosition(int x, int y)
        {
            IVirtualMachine computer = new IntCodeVM(new List<long>(_software), x, y);
            computer.Execute();

            long? result = computer.Outputs.Dequeue();

            return (BeamStatus)result.Value;
        }
    }
}
