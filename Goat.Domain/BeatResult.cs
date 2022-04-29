using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goat.Domain
{
    internal struct BeatResult
    {
        public readonly bool IsBeat;
        public readonly IReadOnlyList<PlayingCardPosition> CurrentMoveCardPositions;

        public BeatResult(bool isBeat, IReadOnlyList<PlayingCardPosition> currentMoveCardPositions)
        {
            IsBeat = isBeat;
            CurrentMoveCardPositions = currentMoveCardPositions;
        }
    }
}
