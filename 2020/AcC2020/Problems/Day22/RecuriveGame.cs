using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC.AoC2020.Problems.Day22
{
    public enum Winner
    {
        PlayerOne = 1,
        PlayerTwo = 2
    }

    public class RecursiveGame : IGame
    {
        public Winner Play(Hand player1, Hand player2)
        {
            HashSet<string> previousStates = new HashSet<string>();

            while (player1.CardsRemaining > 0 && player2.CardsRemaining > 0)
            {
                string state = $"{player1} -- {player2}";
                if (!previousStates.Add(state))
                {
                    // Found previous state - player 1 wins automatically
                    return Winner.PlayerOne;
                }

                // draw next cards
                int card1 = player1.NextCard;
                int card2 = player2.NextCard;

                Winner? winner = null;
                // not enough cards in hands for sub-game - highest card wins
                if (card1 > player1.CardsRemaining || card2 > player2.CardsRemaining)
                {
                    winner = card1 > card2 ? Winner.PlayerOne : Winner.PlayerTwo;
                }
                else
                {
                    winner = Play(player1.GetSubHand(card1), player2.GetSubHand(card2));
                }

                if (winner == Winner.PlayerOne)
                {
                    player1.AddCards(card1, card2);
                }
                else
                {
                    player2.AddCards(card2, card1);
                }

            }

            return player1.CardsRemaining > 0 ? Winner.PlayerOne : Winner.PlayerTwo;
        }
    }
}
