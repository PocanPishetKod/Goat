using System.Collections.Generic;

namespace Goat.Domain
{
    internal class ToPointsConverter
    {
        private static readonly Dictionary<Rank, int> _pointsMap;

        static ToPointsConverter()
        {
            _pointsMap = new Dictionary<Rank, int>()
            {
                { Rank.Six, 0 },
                { Rank.Seven, 0 },
                { Rank.Eight, 0 },
                { Rank.Nine, 0 },
                { Rank.Jack, 2 },
                { Rank.Queen, 3 },
                { Rank.King, 4 },
                { Rank.Ten, 10 },
                { Rank.Ace, 11 }
            };
        }

        public int Convert(PlayingCard playingCard)
        {
            return _pointsMap[playingCard.Rank];
        }
    }
}
