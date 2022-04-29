using System;
using System.Collections.Generic;
using System.Linq;

namespace Goat.Domain
{
    internal class Game
    {
        private readonly List<GamePlayer> _participants;
        private readonly DeckOfCards _deckOfCards;
        private GameState _state;
        private Round? _currentRound;
        private GamePlayer? _winner;

        public Game(PlayingPlaces playingPlaces, DeckOfCards deckOfCards)
        {
            _deckOfCards = deckOfCards;
            _state = GameState.ReadyToStart;
            _participants = new List<GamePlayer>(4);
            InitializeParticipants(playingPlaces);
        }

        public void Start()
        {
            if (_state != GameState.ReadyToStart)
                throw new InvalidOperationException("Game cannot start");

            _currentRound = new Round(SelectNextRoundParticipants(), _deckOfCards);
            _state = GameState.Started;
        }

        public void MakeMove(uint moverId, IReadOnlyList<PlayingCard> playingCards)
        {
            if (_state != GameState.Started)
                throw new InvalidOperationException("Game are not started");

            var moveParameters = new MoveParameters(_participants.Find(p => p.Id == moverId), playingCards);

            if (_currentRound == null)
                throw new InvalidOperationException("Current round is null");

            _currentRound.MakeMove(moveParameters);

            if (_currentRound.State == RoundState.Finished)
            {
                var nextRoundParticipants = SelectNextRoundParticipants();
                if (nextRoundParticipants.Count >= 2)
                {
                    _deckOfCards.Reset();
                    _currentRound = new Round(nextRoundParticipants, _deckOfCards);
                }
                else
                {
                    Finish();
                }
            }
        }

        private IReadOnlyList<Participant> SelectNextRoundParticipants()
        {
            var roundParticipants = new List<Participant>();
            if (_currentRound == null)
            {
                roundParticipants.AddRange(_participants.Select(p => new Participant(p, p.Order)));
            }
            else
            {
                var nextRoundFirstMover = CalculateNextRoundFirstMover();
                if (nextRoundFirstMover == null)
                    return roundParticipants;

                roundParticipants.Add(new Participant(nextRoundFirstMover, 1));

                var nextRoundFirstMoverIndex = _participants.IndexOf(nextRoundFirstMover);
                var startIndex = nextRoundFirstMoverIndex + 1 != _participants.Count ? nextRoundFirstMoverIndex + 1 : 0;
                for (int i = startIndex, order = 2; i != nextRoundFirstMoverIndex; i++, order++)
                {
                    if (_participants[i].Score < 12)
                        roundParticipants.Add(new Participant(_participants[i], order));

                    if (i + 1 == _participants.Count)
                        i = -1;
                }
            }

            return roundParticipants;
        }

        private GamePlayer? CalculateNextRoundFirstMover()
        {
            if (_currentRound.LastAttackWinner.Score < 12)
                return _currentRound.LastAttackWinner;

            var lastAttackWinnerIndex = _participants.IndexOf(_currentRound.LastAttackWinner);
            var startIndex = lastAttackWinnerIndex + 1 != _participants.Count ? lastAttackWinnerIndex + 1 : 0;
            for (int i = startIndex; i != lastAttackWinnerIndex; i++)
            {
                if (_participants[i].Score < 12)
                    return _participants[i];
            }

            return null;
        }

        private GamePlayer CalculateWinner()
        {
            var potentialWinners = new List<GamePlayer>();
            foreach (var participant in _participants)
            {
                if (participant.Score < 12)
                    potentialWinners.Add(participant);
            }

            if (potentialWinners.Count > 1)
                throw new InvalidOperationException("Winners cannot be more then 2");

            if (potentialWinners.Count == 0)
                throw new InvalidOperationException("Winners cannot be equal 0");

            return potentialWinners[0];
        }

        private void InitializeParticipants(PlayingPlaces playingPlaces)
        {
            _participants.AddRange(playingPlaces.Select(pp => new GamePlayer(pp.Player, pp.Order)).ToList());
        }

        private void Finish()
        {
            _winner = CalculateWinner();
            _state = GameState.Finished;
        }
    }
}
