using System.Collections.Generic;

namespace Goat.Domain.Events
{
    public struct DeckOfCardsCreatedEvent
    {
        public readonly IReadOnlyList<PlayingCard> PlayingCards;

        public DeckOfCardsCreatedEvent(IReadOnlyList<PlayingCard> playingCards)
        {
            PlayingCards = playingCards;
        }
    }
}
