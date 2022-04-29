using System;
using System.Collections.Generic;

namespace Goat.Domain
{
    internal class GamePlayer
    {
        private readonly List<PlayingCard> _playingCards;
        private readonly DiscardPile _discardPile;
        private readonly Player _player;
        private int _score;

        public uint Id => _player.Id;

        public int Order { get; }

        public int DiscardPilePoints => _discardPile.CalculatePoints();

        public int Score
        {
            get => _score;
            set
            {
                _score += value;

                if (_score > 12)
                    _score = 12;
            }
        }

        public IReadOnlyList<PlayingCard> PlayingCards => _playingCards;

        public GamePlayer(Player player, int order)
        {
            _playingCards = new List<PlayingCard>(4);
            _discardPile = new DiscardPile();
            _player = player;
            Order = order;
        }

        public void GivePlayingCard(PlayingCard playingCard)
        {
            _playingCards.Add(playingCard);
        }

        public void TakePlayingCards(IReadOnlyList<PlayingCard> playingCards)
        {
            foreach (var card in playingCards)
            {
                if (!_playingCards.Remove(card))
                    throw new InvalidOperationException($"Player {Id} does not have card {card.Suit} {card.Rank}");
            }
        }

        public void AddCardsToDiscardPile(IReadOnlyList<PlayingCard> playingCards)
        {
            _discardPile.AddPlayingCards(playingCards);
        }
    }
}
