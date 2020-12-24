using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;
using AoC.Common.Mapping.Hex;

namespace AoC.AoC2020.Problems.Day24
{
    public class LobbyLayout : AoCSolution<int>
    {
        public override int Year => 2020;
        public override int Day => 24;
        public override string Name => "Day 24: Lobby Layout";
        public override string InputFileName => "Day24.txt";

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            LobbyMap lobby = new LobbyMap(input);

            yield return lobby.CountValue(TileColor.Black);
           // Console.WriteLine(lobby.DrawMap());
            
            for (int i = 0; i < 100; i++)
            {
                lobby.MoveForwards();
                //Console.WriteLine($"{i + 1}: {lobby.CountValue(TileColor.Black)} ");
            }
            //Console.WriteLine(lobby.DrawMap());
        }

        private readonly IEnumerable<string> _example = new List<string>()
        {
            "sesenwnenenewseeswwswswwnenewsewsw",
            "neeenesenwnwwswnenewnwwsewnenwseswesw", 
            "seswneswswsenwwnwse",
            "nwnwneseeswswnenewneswwnewseswneseene",
            "swweswneswnenwsewnwneneseenw",
            "eesenwseswswnenwswnwnwsewwnwsene",
            "sewnenenenesenwsewnenwwwse",
            "wenwwweseeeweswwwnwwe",
            "wsweesenenewnwwnwsenewsenwwsesesenwne",
            "neeswseenwwswnwswswnw",
            "nenwswwsewswnenenewsenwsenwnesesenew",
            "enewnwewneswsewnwswenweswnenwsenwsw",
            "sweneswneswneneenwnewenewwneswswnese",
            "swwesenesewenwneswnwwneseswwne",
            "enesenwswwswneneswsenwnewswseenwsese",
            "wnwnesenesenenwwnenwsewesewsesesew",
            "nenewswnwewswnenesenwnesewesw",
            "eneswnwswnwsenenwnwnwwseeswneewsenese",
            "neswnwewnwnwseenwseesewsenwsweewe",
            "wseweeenwnesenwwwswnew"
        };

    }
}
