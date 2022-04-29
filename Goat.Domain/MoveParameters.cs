using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
