using System.Collections.Generic;
using System.Linq;

namespace AoC.AoC2020.Problems.Day22
{
    public class Hand
    {
        private Queue<int> _cards = new Queue<int>();
        public int CardsRemaining => _cards.Count;

        public IReadOnlyList<int> Cards => _cards.ToList();

        public Hand(IEnumerable<int> cards)
        {
            foreach (int card in cards)
            {
                _cards.Enqueue(card);
            }
        }

        public Hand GetSubHand(int numCards)
        {
            return new Hand(_cards.Take(numCards));
        }

        public int NextCard => _cards.Dequeue();

        public void AddCards(int card1, int cards2)
        {
            _cards.Enqueue(card1);
            _cards.Enqueue(cards2);
        }

        public int Score
        {
            get
            {
                var cards = _cards.ToList();
                int score = 0;
                int length = cards.Count;

                foreach (int card in _cards)
                {
                    score += card * length;
                    length--;
                }

                return score;
            }
        }

        public override string ToString()
        {
            return string.Join(',', _cards);
        }
    }
}