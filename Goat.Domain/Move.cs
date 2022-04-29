using System.Collections.Generic;

namespace Goat.Domain
{
    internal struct Move
    {
        public readonly GamePlayer Mover;
        public readonly IReadOnlyList<PlayingCardPosition> PlayingCardPositions;
        public bool IsDiscard;

        public Move(GamePlayer mover, IReadOnlyList<PlayingCardPosition> playingCardPositions, bool isDiscard)
        {
            Mover = mover;
            PlayingCardPositions = playingCardPositions;
            IsDiscard = isDiscard;
        }
    }
}
