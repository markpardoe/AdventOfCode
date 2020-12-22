using System;
using System.Collections.Generic;
using System.Text;

namespace AoC.AoC2020.Problems.Day22
{
    public interface IGame
    {
        public Winner Play(Hand player1, Hand player2);
    }

    public class Game : IGame
    {

        public Winner Play(Hand player1, Hand player2)
        {
            while (player1.CardsRemaining > 0 && player2.CardsRemaining > 0)
            {
                int card1 =player1.NextCard;
                int card2 = player2.NextCard;

                if (card1 > card2)
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
