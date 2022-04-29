using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goat.Domain
{
    public struct PlayingCard
    {
        public readonly Rank Rank;

        public readonly Suit Suit;

        public PlayingCard(Rank rank, Suit suit)
        {
            Rank = rank;
            Suit = suit;
        }
    }
}
