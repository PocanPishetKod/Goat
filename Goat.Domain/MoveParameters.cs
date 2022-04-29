using System.Collections.Generic;

namespace Goat.Domain
{
    internal struct MoveParameters
    {
        public readonly GamePlayer Mover;
        public readonly IReadOnlyList<PlayingCard> PlayingCards;

        public MoveParameters(GamePlayer mover, IReadOnlyList<PlayingCard> playingCard)
        {
            Mover = mover;
            PlayingCards = playingCard;
        }
    }
}
