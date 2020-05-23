using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day22
{
    public class Deck
    {
        private List<int> _cards = new List<int>();
        public int DeckSize => _cards.Count;

        public Deck(int deckSize)
        {
            if (deckSize <=0)
            {
                throw new InvalidOperationException("Invalid Decksize.");
            }

            for (int i=0; i< deckSize; i++)
            {
                _cards.Add(i);
            }
        }

        public int this[int index]
        {
            get
            {
                return _cards[index];
            }
        }

        public int IndexOf(int value)
        {
            return _cards.IndexOf(value);
        }

        public override string ToString()
        {
            return string.Join(", ", _cards);
        }

        public void DealIntoNewStack()
        {
            _cards.Reverse();
        }

        public void CutCards(int numCards)
        {
            if (Math.Abs(numCards) > this.DeckSize)
            {
                throw new ArgumentOutOfRangeException("Deck size is too small.");
            }

            if (numCards > 0)
            {
                _cards = (_cards.Skip(numCards).Concat(_cards.Take(numCards))).ToList();
            }
            else if (numCards <0)
            {
                int skip = Math.Abs(numCards);

                var first = _cards.Take(_cards.Count - skip);  // take first set of cards from the deck
                var last = _cards.Skip(_cards.Count - skip);
                _cards = last.Concat(first).ToList();
            }
        }

        public void Increment(int n)
        {
            if (n <=0)
            {
                throw new ArgumentOutOfRangeException("Increment Value must be greater than one.");
            }

            int deckSize = _cards.Count;

            Dictionary<int, int> shuffledCards = new Dictionary<int, int>(); // Postion : CardValue dictionary
            int insertPosition = 0;

            for (int index = 0; index < deckSize;index++) 
            {
                shuffledCards.Add(insertPosition, _cards[index]);  // take the next card and add to the new deck
                insertPosition = (insertPosition + n) % DeckSize;  // wrap the position the card will be added to - to the dictionary
            }


            // Add the shuffled cards back into the deck
            _cards.Clear();
            for (int i=0; i< deckSize;i++)
            {
                _cards.Add(shuffledCards[i]);
            }
        }

        public IReadOnlyList<int> GetCards()
        {
            return new List<int>(_cards);
        }

        public void Shuffle(IEnumerable<string> shuffles)
        {
            foreach (string shuffle in shuffles)
            {
                string[] shuffleData = shuffle.Split(' ');
                if (shuffleData.Length == 2)
                {
                    // Cut the cards
                    int cutSize = Int32.Parse(shuffleData[1]);
                    this.CutCards(cutSize);
                }
                else if (shuffleData.Length == 4)
                {
                    if (shuffleData[3].Equals("stack", StringComparison.CurrentCultureIgnoreCase))
                    {
                        this.DealIntoNewStack();
                    }
                    else
                    {
                        int incrementValue = Int32.Parse(shuffleData[3]);
                        this.Increment(incrementValue);
                    }
                }
                else
                {
                    throw new InvalidOperationException("Invalid Shuffle: " + shuffle);
                }

            }
        }
    }
}
