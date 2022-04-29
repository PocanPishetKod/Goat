using System.Collections.Generic;

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
