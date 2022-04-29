using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goat.Domain.Events;
using Goat.EventEngine;

namespace Goat.Domain
{
    public class GameTable
    {
        private readonly PlayingPlaces _places;
        private readonly DeckOfCards _deckOfCards;
        private Game _currentGame;

        public uint Id { get; }

        public GameTable(int placesCount, uint id)
        {
            if (placesCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(placesCount));

            _places = new PlayingPlaces(placesCount);
            _deckOfCards = DeckOfCards.Create();
            Id = id;
        }

        public void JoinPlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            if (_places.Find(player.Id) != null)
                throw new InvalidOperationException("Player already joined");

            _places.PutPlayer(player);

            EventBus.Instance.Publish(new PlayerJoinedEvent(player));
        }

        public void LeavePlayer(uint playerId)
        {
            _places.RemovePlayer(playerId);
        }

        public void StartGame()
        {
            if (_currentGame != null)
                throw new InvalidOperationException("Game cannot be started");

            if (!_places.IsFull())
                throw new InvalidOperationException("Places are not full");

            _currentGame = new Game(_places, _deckOfCards);
        }

        public void MakeMove(uint moverId, IReadOnlyList<PlayingCard> playingCards)
        {
            if (_currentGame == null)
                throw new InvalidOperationException("Game are not started");

            var place = _places.Find(moverId);
            if (place == null)
                throw new InvalidOperationException("Player not found");

            _currentGame.MakeMove(moverId, playingCards);
        }
    }
}
