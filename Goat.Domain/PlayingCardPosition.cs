namespace Goat.Domain
{
    internal struct PlayingCardPosition
    {
        public readonly int Order;
        public readonly PlayingCard PlayingCard;

        public PlayingCardPosition(int order, PlayingCard playingCard)
        {
            Order = order;
            PlayingCard = playingCard;
        }
    }
}
