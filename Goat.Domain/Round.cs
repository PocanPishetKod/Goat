using System;
using System.Collections.Generic;
using System.Linq;

namespace Goat.Domain
{
    internal class Round
    {
        private readonly RoundTotalCalculator _roundTotalCalculator;
        private readonly List<Participant> _participants;
        private readonly DeckOfCards _deckOfCards;
        private Attack _currentAttack;
        private Suit _trump;

        public GamePlayer LastAttackWinner { get; private set; }

        public RoundState State { get; private set; }

        public Round(IReadOnlyList<Participant> participants, DeckOfCards deckOfCards)
        {
            _roundTotalCalculator = new RoundTotalCalculator();
            State = RoundState.ReadyToStart;
            _deckOfCards = deckOfCards;
            _participants = participants.ToList();
        }

        public void Start()
        {
            if (State != RoundState.ReadyToStart)
                throw new InvalidOperationException("Round cannot start");

            _deckOfCards.Shuffle();
            SetTrump();
            var nextMover = CalculateNextAttackFirstMover();
            var nextAttackParticipants = SelectNextAttackParticipants(nextMover);
            DealCards(nextAttackParticipants);
            _currentAttack = new Attack(_trump, nextAttackParticipants);
            State = RoundState.Started;
        }

        public void MakeMove(MoveParameters moveParameters)
        {
            if (State != RoundState.Started)
                throw new InvalidOperationException("Invalid game state");

            _currentAttack.MakeMove(moveParameters);

            if (_currentAttack.Finished)
            {
                if (!CanStartNextAttack())
                {
                    Finish();
                }
                else
                {
                    var nextMover = CalculateNextAttackFirstMover();
                    var nextAttackParticipants = SelectNextAttackParticipants(nextMover);
                    DealCards(nextAttackParticipants);
                    _currentAttack = new Attack(_trump, nextAttackParticipants);
                }
            }
        }

        private bool CanStartNextAttack()
        {
            return _deckOfCards.Count > 0 || PlayersHaveCards();
        }

        private bool PlayersHaveCards()
        {
            foreach (var participant in _participants)
            {
                if (participant.Player.PlayingCards.Any())
                    return true;
            }

            return false;
        }

        private IReadOnlyList<Participant> SelectNextAttackParticipants(GamePlayer nextMover)
        {
            var attackParticipants = new List<Participant>(4);
            attackParticipants.Add(new Participant(nextMover, 1));

            var nextMoverIndex = _participants.IndexOf(_participants.First(p => p.Player.Id == nextMover.Id));
            var startIndex = nextMoverIndex != _participants.Count - 1 ? nextMoverIndex + 1 : 0;
            for (int i = startIndex, order = 2; _participants[i].Player.Id != nextMover.Id; i++, order++)
            {
                attackParticipants.Add(new Participant(_participants[i].Player, order));

                if (i == _participants.Count - 1)
                    i = -1;
            }

            return attackParticipants;
        }

        private GamePlayer CalculateNextAttackFirstMover()
        {
            return _currentAttack != null ? _currentAttack.Winner : _participants[new Random().Next(_participants.Count)].Player;
        }

        private void SetTrump()
        {
            _trump = _deckOfCards[new Random().Next(_deckOfCards.Count)].Suit;
        }

        private void DealCards(IReadOnlyList<Participant> attackParticipants)
        {
            while (attackParticipants[0].Player.PlayingCards.Count < 4)
            {
                if (_deckOfCards.Count == 0)
                    return;

                foreach (var participant in attackParticipants)
                {
                    participant.Player.GivePlayingCard(_deckOfCards.TakePlayingCard());
                }
            }
        }

        private void Finish()
        {
            var roundResult = new RoundResult(
                _roundTotalCalculator.Caclulate(_participants.Select(p => p.Player).ToList()),
                _currentAttack.Winner);

            foreach (var item in roundResult.PlayerRoundTotals)
            {
                item.Player.Score += item.PointsAwarded;
            }

            LastAttackWinner = roundResult.LastAttackWinner;

            State = RoundState.Finished;
        }
    }
}
