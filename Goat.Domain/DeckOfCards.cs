using System;
using System.Collections.Generic;

namespace Goat.Domain
{
    internal class DeckOfCards
    {
        private readonly List<PlayingCard> _playingCards;
        private int _boundaryIndex;

        public PlayingCard this[int index]
        {
            get
            {
                return _playingCards[index];
            }
        }

        public int Count => _boundaryIndex + 1;

        private DeckOfCards()
        {
            _playingCards = new List<PlayingCard>(36);
        }

        public void Shuffle()
        {

        }

        public PlayingCard TakePlayingCard()
        {
            if (_boundaryIndex == -1)
                throw new InvalidOperationException("Deck of cards is empty");

            return _playingCards[_boundaryIndex--];
        }

        public void Reset()
        {
            _boundaryIndex = _playingCards.Count - 1;
        }

        public static DeckOfCards Create()
        {
            var result = new DeckOfCards();

            var rankValues = Enum.GetValues(typeof(Rank));
            var suitValues = Enum.GetValues(typeof(Suit));
            foreach (var rank in rankValues)
            {
                foreach (var suit in suitValues)
                {
                    result._playingCards.Add(new PlayingCard((Rank)rank, (Suit)suit));
                }
            }

            result._boundaryIndex = result._playingCards.Count - 1;

            return result;
        }
    }
}
