using System;
using System.Collections.Generic;
using System.Linq;

namespace Goat.Domain
{
    internal class Attack
    {
        private readonly List<Move> _moves;
        private readonly Suit _trump;
        private readonly AttackParticipants _participants;
        private readonly PlayingCardBeatProcess _cardPositionCalculator;

        public bool Finished { get; private set; }

        public GamePlayer? Winner { get; private set; }

        public Attack(Suit trump, IReadOnlyList<Participant> participants)
        {
            _moves = new List<Move>();
            _trump = trump;
            _participants = new AttackParticipants(participants);
            _cardPositionCalculator = new PlayingCardBeatProcess();
        }

        public void MakeMove(MoveParameters moveParameters)
        {
            if (Finished)
                throw new InvalidOperationException("Attack already completed");

            if (_participants.CurrentMover == null)
                throw new InvalidOperationException("Current mover is null");

            if (_participants.CurrentMover.Id != moveParameters.Mover.Id)
                throw new InvalidOperationException($"Player {moveParameters.Mover.Id} cannot move now");

            if (!_participants.Contains(moveParameters.Mover.Id))
                throw new InvalidOperationException($"Player {moveParameters.Mover.Id} are not a participant");

            TakeCards(moveParameters);

            if (_moves.Count == 0)
            {
                var playingCardPositions = new List<PlayingCardPosition>();
                for (int i = 0; i < moveParameters.PlayingCards.Count; i++)
                {
                    playingCardPositions.Add(new PlayingCardPosition(i + 1, moveParameters.PlayingCards[i]));
                }

                _moves.Add(new Move(moveParameters.Mover, playingCardPositions, false));
            }
            else
            {
                var lastAttackMove = GetLastAttackMove();

                if (lastAttackMove.PlayingCardPositions.Count != moveParameters.PlayingCards.Count)
                    throw new InvalidOperationException($"There must be {lastAttackMove.PlayingCardPositions.Count} cards");

                var beatResult = _cardPositionCalculator.
                    Beat(moveParameters.PlayingCards, lastAttackMove.PlayingCardPositions, _trump);

                _moves.Add(new Move(moveParameters.Mover, beatResult.CurrentMoveCardPositions, beatResult.IsBeat));
            }

            if (!_participants.TrySetNextMover())
            {
                Winner = CalculateWinner();
                GiveTrickToWinner();
                Finished = true;
            }
        }

        private Move GetLastAttackMove()
        {
            for (int i = _moves.Count - 1; i >= 0; i--)
            {
                if (!_moves[i].IsDiscard)
                    return _moves[i];
            }

            throw new InvalidOperationException("Defend moves not found");
        }

        private GamePlayer CalculateWinner()
        {
            for (int i = _moves.Count - 1; i >= 0; i--)
            {
                if (!_moves[i].IsDiscard)
                    return _moves[i].Mover;
            }

            throw new InvalidOperationException("Winnder not found");
        }

        private void GiveTrickToWinner()
        {
            if (Winner == null)
                throw new InvalidOperationException("Winner is null");

            foreach (var move in _moves)
            {
                Winner.AddCardsToDiscardPile(move.PlayingCardPositions.Select(pp => pp.PlayingCard).ToList());
            }
        }

        private void TakeCards(MoveParameters moveParameters)
        {
            moveParameters.Mover.TakePlayingCards(moveParameters.PlayingCards);
        }
    }
}
