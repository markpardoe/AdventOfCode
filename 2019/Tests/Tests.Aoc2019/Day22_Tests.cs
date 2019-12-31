using Aoc.AoC2019.Problems.Day22;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Aoc.AoC2019.Tests
{
    public class Day22_Tests
    {
        [Theory]
        [InlineData(5, 0,1,2,3,4)]
        [InlineData(1, 0)]
        [InlineData(10, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9)]
        public  void Test_DeckInitialisedCorrectly(int numCards, params int[] expectedDeck)
        {
            Deck deck = new Deck(numCards);
            List<int> expectedResult = expectedDeck.ToList();

            Assert.Equal(expectedResult, deck.GetCards());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void Test_Deck_Constructor_Throws_Error_On_NumCards_Less_Than_One(int numCards)
        {
            Deck d;
            Assert.Throws<InvalidOperationException>(() => d = new Deck(numCards));
        }

        [Theory]
        [InlineData(5,4,3,2,1,0)]
        [InlineData(1, 0)]
        [InlineData(10, 9,8,7,6,5,4,3,2,1,0)]
        public void Test_Deck_DealIntoNewDeck(int numCards, params int[] expectedDeck)
        {
            Deck deck = new Deck(numCards);
            deck.DealIntoNewStack();
            List<int> expectedResult = expectedDeck.ToList();

            Assert.Equal(expectedResult, deck.GetCards());
        }

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(2)]
        public void Test_Deck_DealIntoNewDeck_Twice_Returns_OriginalDeck(int numCards)
        {
            Deck deck = new Deck(numCards);
            IEnumerable<int> expectedResult = deck.GetCards();

            deck.DealIntoNewStack();
            deck.DealIntoNewStack();

            IEnumerable<int> actualResult = deck.GetCards();

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(10, 0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9)]  // cut 0 cards - the deck shouldn't alter
        [InlineData(10, 3, 3,4,5,6,7,8,9,0,1,2)]
        [InlineData(10, 1, 1, 2,3,4,5,6,7,8,9,0)]
        [InlineData(10, -1, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8)]
        [InlineData(10, -4, 6,7,8,9,0,1,2,3,4,5)]
        [InlineData(10, 10, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9)]  // cut all cards - so the deck shouldn't alter
        [InlineData(10, -10, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9)]  // cut all cards - so the deck shouldn't alter
        public void Test_Deck_CutCards(int numCards, int cardsToCut, params int[] expectedDeck)
        {
            Deck deck = new Deck(numCards);
            deck.CutCards(cardsToCut);
            List<int> expectedResult = expectedDeck.ToList();
            Assert.Equal(expectedResult, deck.GetCards());
        }

        [Theory]
        [InlineData(10, 11)]
        [InlineData(1, 5)]
        [InlineData(1, -2)]
        [InlineData(10, -11)]
        public void Test_Deck_CutCards_Throws_Exception_If_Cards_To_Cut_Is_Greater_Than_Deck_Size(int numCards, int cardsToCut)
        {
            Deck d = new Deck(numCards);
            Assert.Throws<ArgumentOutOfRangeException>(() => d.CutCards(cardsToCut));
        }

        [Theory]
        [InlineData(10, 3, 0,7,4,1,8,5,2,9,6,3)]
        public void Test_Deck_Increment(int numCards, int incrementSize, params int[] expectedResult)
        {
            Deck d = new Deck(numCards);
            d.Increment(incrementSize);
            List<int> expected = new List<int>(expectedResult);
            List<int> actual = new List<int>(d.GetCards());
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(Day22_TestData))]
        public void Test_Deck_ShuffleCommandList(List<string> commands, List<int> expectedResults)
        {
            Deck deck = new Deck(10);
            deck.Shuffle(commands);
            List<int> actualResults = deck.GetCards().ToList();
            Assert.Equal(expectedResults, actualResults);

        }

        public class Day22_TestData : IEnumerable<object[]>
        {

            private readonly List<string> Example1 = new List<string>()
            {
                "deal with increment 7",
                "deal into new stack",
                "deal into new stack"
            };
            private readonly List<int> Example1_Result = new List<int>() { 0, 3, 6, 9, 2, 5, 8, 1, 4, 7 };

            private readonly List<string> Example2 = new List<string>()
            {
                "cut 6",
                "deal with increment 7",
                "deal into new stack"
            };
            private readonly List<int> Example2_Result = new List<int>() { 3, 0, 7, 4, 1, 8, 5, 2, 9, 6 };

            private readonly List<string> Example3 = new List<string>()
            {
                "deal with increment 7",
                "deal with increment 9",
                "cut -2"
            };
            private readonly List<int> Example3_Result = new List<int>() { 6, 3, 0, 7, 4, 1, 8, 5, 2, 9 };

            private readonly List<string> Example4 = new List<string>()
            {
                "deal into new stack",
                "cut -2",
                "deal with increment 7",
                "cut 8",
                "cut -4",
                "deal with increment 7",
                "cut 3",
                "deal with increment 9",
                "deal with increment 3",
                "cut -1"
            };

            private static readonly List<int> Example4_Result = new List<int>() { 9, 2, 5, 8, 1, 4, 7, 0, 3, 6 };

            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { Example1, Example1_Result};
                yield return new object[] { Example2, Example2_Result};
                yield return new object[] { Example3, Example3_Result };
                yield return new object[] { Example4, Example4_Result };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

    }
}
