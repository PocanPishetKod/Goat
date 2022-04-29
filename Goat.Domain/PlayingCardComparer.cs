using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goat.Domain
{
    internal class PlayingCardComparer
    {
        private static readonly Dictionary<Rank, int> _pointsMap;

        static PlayingCardComparer()
        {
            _pointsMap = new Dictionary<Rank, int>()
            {
                { Rank.Six, 1 },
                { Rank.Seven, 2 },
                { Rank.Eight, 3 },
                { Rank.Nine, 4 },
                { Rank.Jack, 5 },
                { Rank.Queen, 6 },
                { Rank.King, 7 },
                { Rank.Ten, 8 },
                { Rank.Ace, 9 }
            };
        }

        public bool Compare(PlayingCard defender, PlayingCard attacker, Suit trump)
        {
            if (defender.Suit == attacker.Suit)
                return CompareRank(defender, attacker);

            if (attacker.Suit != trump && defender.Suit != trump)
                return false;

            return defender.Suit == trump;
        }

        private bool CompareRank(PlayingCard defender, PlayingCard attacker)
        {
            if (attacker.Rank == defender.Rank)
                throw new InvalidOperationException("Ranks cannot be equal");

            return _pointsMap[defender.Rank] > _pointsMap[attacker.Rank];
        }
    }
}
