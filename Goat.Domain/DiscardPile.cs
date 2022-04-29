using System;
using System.Collections.Generic;

namespace Goat.Domain
{
    internal class DiscardPile
    {
        private readonly List<PlayingCard> _playingCards;
        private readonly ToPointsConverter _toPointsConverter;

        public DiscardPile()
        {
            _playingCards = new List<PlayingCard>(20);
            _toPointsConverter = new ToPointsConverter();
        }

        public void AddPlayingCards(IReadOnlyList<PlayingCard> playingCards)
        {
            if (playingCards == null)
                throw new ArgumentNullException(nameof(playingCards));

            if (_playingCards.Count + playingCards.Count >= 36)
                throw new InvalidOperationException("Card count >= 36");

            _playingCards.AddRange(playingCards);
        }

        public void Clear()
        {
            _playingCards.Clear();
        }

        public int CalculatePoints()
        {
            int points = 0;

            foreach (var playingCard in _playingCards)
            {
                points += _toPointsConverter.Convert(playingCard);
            }

            return points;
        }
    }
}
