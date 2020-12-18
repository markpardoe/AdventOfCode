using AoC.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc.AoC2019.Problems.Day19
{
    public enum BeamStatus
    {
        Stationary = 0,
        Pulling = 1,
        Unknown = 2
    }

    public class TractorBeamMap : Map<BeamStatus>
    {
        public TractorBeamMap() : base(BeamStatus.Unknown) { }

        private readonly Dictionary<BeamStatus, char> _beamCharMapping = new Dictionary<BeamStatus, char>()
        {
            {BeamStatus.Pulling, '#'},
            {BeamStatus.Stationary, '.'},
            {BeamStatus.Unknown, '?'}
        };

        protected override char? ConvertValueToChar(Position pos, BeamStatus value)
        {
            return _beamCharMapping[value];
        }
    }
}
