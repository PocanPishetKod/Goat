using System.Collections.Generic;
using System.Linq;

namespace Goat.Domain
{
    internal class AttackParticipants
    {
        private readonly List<Participant> _participants;

        public GamePlayer CurrentMover { get; private set; }

        public AttackParticipants(IReadOnlyList<Participant> participants)
        {
            _participants = participants.OrderBy(p => p.Order).ToList();
            TrySetNextMover();
        }

        public bool TrySetNextMover()
        {
            CurrentMover = FindNextMover();
            return CurrentMover != null;
        }

        public bool Contains(uint playerId)
        {
            foreach (var participant in _participants)
            {
                if (participant.Player.Id == playerId)
                    return true;
            }

            return false;
        }

        private GamePlayer FindNextMover()
        {
            if (CurrentMover == null)
                return _participants[0].Player;

            for (int i = 0; i < _participants.Count; i++)
            {
                if (_participants[i].Player.Id == CurrentMover.Id)
                {
                    return i + 1 < _participants.Count ? _participants[i + 1].Player : null;
                }
            }

            return null;
        }
    }
}
