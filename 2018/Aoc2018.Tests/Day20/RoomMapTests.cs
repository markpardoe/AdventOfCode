using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aoc.Aoc2018.Day20;
using AoC.Common.Mapping;
using Shouldly;
using Xunit;

namespace AoC.AoC2018.Tests.Day20
{
    public class RoomMapTests
    {
        [Theory]
        [InlineData(@"^ENNWSWW(NEWS|)SSSEEN(WNSE|)EE(SWEN|)NNN$", 18)]
        [InlineData(@"^ESSWWN(E|NNENN(EESS(WNSE|)SSS|WWWSSSSE(SW|NNNE)))$", 23)]
        [InlineData(@"^WSSEESWWWNW(S|NENNEEEENN(ESSSSW(NWSW|SSEN)|WSWWN(E|WWS(E|SS))))$", 31)]
        [InlineData(@"^E(NN|S)E$", 4)]
        [InlineData(@"^(N|S)N$", 2)]
        [InlineData(@"^EEE(NN|SSS)EEE$", 9)]
        [InlineData(@"^E(N|SS)EEE(E|SSS)$", 9)]
        public void PathToAllRooms_WithExamples_FindsCorrectLongestPath(string input, int expected)
        {
            var sut = new RoomMap(input);

            var allRooms = sut.ShortestPathToValue(sut.Start, MapTile.Room);
            int actual = allRooms.Max(x => x.DistanceFromStart);

            actual.ShouldBe(expected);
        }
    }
}
