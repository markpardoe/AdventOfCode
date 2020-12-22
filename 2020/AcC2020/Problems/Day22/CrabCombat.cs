using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;

namespace AoC.AoC2020.Problems.Day22
{
    public class CrabCombat :AoCSolution<int>
    {
        public override int Year => 2020;
        public override int Day => 22;
        public override string Name => "Day 22: Crab Combat";
        public override string InputFileName => "Day22.txt";

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            var players = ParseInput(input).ToList();
            
            IGame game = new RecursiveGame();
            yield return FindWinningScore(game, players[0], players[1]);
        }

        private int FindWinningScore(IGame game, IEnumerable<int> hand1, IEnumerable<int> hand2) 
        {
            var player1 = new Hand(hand1);
            var player2 = new Hand(hand2);

            var winner = game.Play(player1, player2);

            Console.WriteLine($"Winner = {winner}");

            return winner == Winner.PlayerOne ? player1.Score : player2.Score;
        }

        private IEnumerable<IEnumerable<int>> ParseInput(IEnumerable<string> rawData)
        {
            var cards = new List<int>();

            foreach (var line in rawData)
            {
                if (line.StartsWith("Player"))
                {
                    if (cards.Count > 0)
                    {
                        yield return cards;
                        cards = new List<int>();
                    }
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    cards.Add(int.Parse(line));
                }
            }

            if (cards.Count > 0)
            {
                yield return cards;
                cards = new List<int>();
            }
        }
        
        private readonly IEnumerable<string> _example = new List<string>()
        {
            "Player 1:",
            "9",
            "2",
            "6",
            "3",
            "1",
            "",
            "Player 2:",
            "5",
            "8",
            "4",
            "7",
            "10"
        };
    }
}
