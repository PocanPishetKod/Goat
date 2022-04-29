using System;

namespace Goat.Domain
{
    public class PlayingPlace
    {
        public Player? Player { get; set; }

        public bool IsFree => Player == null;

        public int Order { get; }

        public PlayingPlace(int order)
        {
            if (order <= 0)
                throw new ArgumentOutOfRangeException(nameof(order));

            Order = order;
        }
    }
}
