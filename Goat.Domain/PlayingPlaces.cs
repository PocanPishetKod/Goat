using System;
using System.Collections;
using System.Collections.Generic;

namespace Goat.Domain
{
    internal class PlayingPlaces : IEnumerable<PlayingPlace>
    {
        private readonly int _placesCount;
        private readonly List<PlayingPlace> _playingPlaces;

        public int Count => _placesCount;

        public PlayingPlaces(int placesCount)
        {
            _placesCount = placesCount;
            _playingPlaces = new List<PlayingPlace>(placesCount);
            CreatePlaces();
        }

        public void PutPlayer(Player player)
        {
            if (IsFull())
                throw new InvalidOperationException("All places are occupied");

            var freePlace = FindNextFreePlace();
            if (freePlace == null)
                throw new InvalidOperationException("All places are occupied");

            freePlace.Player = player;
        }

        public void RemovePlayer(uint playerId)
        {
            var place = Find(playerId);
            if (place == null)
                throw new InvalidOperationException("Player not found");

            place.Player = null;
        }

        public PlayingPlace? Find(uint playerId)
        {
            foreach (var place in _playingPlaces)
            {
                if (place.Player != null && place.Player.Id == playerId)
                    return place;
            }

            return null;
        }

        public bool IsFull()
        {
            foreach (var place in _playingPlaces)
            {
                if (place.IsFree)
                    return false;
            }

            return true;
        }

        private PlayingPlace? FindNextFreePlace()
        {
            foreach (var place in _playingPlaces)
            {
                if (place.IsFree)
                    return place;
            }

            return null;
        }

        private void CreatePlaces()
        {
            if (_playingPlaces.Count > 0)
                throw new InvalidOperationException("Places already created");

            for (int i = 0; i <= _placesCount; i++)
            {
                _playingPlaces.Add(new PlayingPlace(i + 1));
            }
        }

        public IEnumerator<PlayingPlace> GetEnumerator()
        {
            for (int i = 0; i < _playingPlaces.Count; i++)
            {
                yield return _playingPlaces[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
