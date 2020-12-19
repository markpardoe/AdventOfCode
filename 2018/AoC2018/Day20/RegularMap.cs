using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day20
{
    public class RegularMap : AoCSolution<int>
    {
        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            string data = input.First();
            RoomMap map = new RoomMap(data);


            var rooms = map.FindPathToAllTargets();
            yield return rooms.Max(x => x.DistanceFromStart);
            yield return rooms.Count(x => x.DistanceFromStart >= 1000);
        }

        public override int Year => 2018;
        public override int Day => 20;
        public override string Name => "Day 20: A Regular Map";
        public override string InputFileName => "Day20.txt";


        private readonly string _example1 = @"^ENNWSWW(NEWS|)SSSEEN(WNSE|)EE(SWEN|)NNN$";
        private readonly string _example2 = @"^ESSWWN(E|NNENN(EESS(WNSE|)SSS|WWWSSSSE(SW|NNNE)))$";
        private readonly string _example3 = @"^WSSEESWWWNW(S|NENNEEEENN(ESSSSW(NWSW|SSEN)|WSWWN(E|WWS(E|SS))))$";

        private readonly string _example4 = @"^E(NN|S)E$"; // 4
        private readonly string _example5 = @"^(N|S)N$"; // 2
        private readonly string _example6 = @"^EEE(NN|SSS)EEE$"; // 9
        private readonly string _example7 = @"^E(N|SS)EEE(E|SSS)$"; // 9
    }
}
