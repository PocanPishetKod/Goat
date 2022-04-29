using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goat.Domain
{
    internal class PlayingCardBeatProcess
    {
        private readonly PlayingCardComparer _playingCardComparer;

        public PlayingCardBeatProcess()
        {
            _playingCardComparer = new PlayingCardComparer();
        }

        public BeatResult Beat(IReadOnlyList<PlayingCard> currentMoveCards,
            IReadOnlyList<PlayingCardPosition> lastMoveCardPositions, Suit trump)
        {
            var defendMap = CalculateDefendMap(currentMoveCards, lastMoveCardPositions, trump);
            var currentMoveCardPositionsMap = new List<BeatMapElement>();

            for (int i = 0; i < defendMap.Count; i++)
            {
                BeatMapElement? beaten = null;

                var attackCardIndex = 0;
                if (currentMoveCardPositionsMap.Exists(bm => bm.DefendCard.Equals(defendMap[i].DefendCard)))
                {
                    var currentBeatMapElement = currentMoveCardPositionsMap.Find(bm => bm.DefendCard.Equals(defendMap[i].DefendCard));
                    if (currentBeatMapElement.Index == defendMap[i].AttackCardPositions.Count - 1)
                    {
                        if (i == 0)
                            return new BeatResult(false, CreateDefaultCardPositions(currentMoveCards));

                        i -= 2;
                        currentMoveCardPositionsMap.Remove(currentBeatMapElement);
                        continue;
                    }

                    attackCardIndex = currentBeatMapElement.Index + 1;
                    currentMoveCardPositionsMap.Remove(currentBeatMapElement);
                }

                for (int j = attackCardIndex; j < defendMap[i].AttackCardPositions.Count; j++)
                {
                    if (currentMoveCardPositionsMap.Any(cp => cp.AttackCardPosition.Equals(defendMap[i].AttackCardPositions[j])))
                        continue;

                    beaten = new BeatMapElement(defendMap[i].DefendCard, defendMap[i].AttackCardPositions[j], j);
                    break;
                }

                if (i == 0 && !beaten.HasValue)
                    return new BeatResult(false, CreateDefaultCardPositions(currentMoveCards));

                if (!beaten.HasValue)
                {
                    i -= 2;
                    continue;
                }

                currentMoveCardPositionsMap.Add(beaten.Value);
            }

            var currentMoveCardPositions = new List<PlayingCardPosition>();
            foreach (var item in currentMoveCardPositionsMap)
            {
                currentMoveCardPositions.Add(new PlayingCardPosition(item.AttackCardPosition.Order, item.DefendCard));
            }

            return new BeatResult(true, currentMoveCardPositions);
        }

        private IReadOnlyList<PlayingCardPosition> CreateDefaultCardPositions(IReadOnlyList<PlayingCard> playingCards)
        {
            var result = new List<PlayingCardPosition>();
            for (int i = 0; i < playingCards.Count; i++)
            {
                result.Add(new PlayingCardPosition(i + 1, playingCards[i]));
            }

            return result;
        }

        private IReadOnlyList<DefendMapElement> CalculateDefendMap(
            IReadOnlyList<PlayingCard> currentMoveCards,
            IReadOnlyList<PlayingCardPosition> lastMoveCardPositions,
            Suit trump)
        {
            var result = new List<DefendMapElement>(4);

            for (int defendIndex = 0; defendIndex < currentMoveCards.Count; defendIndex++)
            {
                var defendCards = new List<PlayingCardPosition>();
                for (int attackIndex = 0; attackIndex < lastMoveCardPositions.Count; attackIndex++)
                {
                    if (_playingCardComparer.Compare(
                        currentMoveCards[defendIndex], lastMoveCardPositions[attackIndex].PlayingCard, trump))
                    {
                        defendCards.Add(lastMoveCardPositions[attackIndex]);
                    }
                }

                result.Add(new DefendMapElement(currentMoveCards[defendIndex], defendCards));
            }

            return result;
        }

        private struct DefendMapElement
        {
            public readonly PlayingCard DefendCard;
            public readonly IReadOnlyList<PlayingCardPosition> AttackCardPositions;

            public DefendMapElement(PlayingCard defendCard, IReadOnlyList<PlayingCardPosition> attackCardPositions)
            {
                DefendCard = defendCard;
                AttackCardPositions = attackCardPositions;
            }
        }

        private struct BeatMapElement
        {
            public readonly PlayingCard DefendCard;
            public readonly PlayingCardPosition AttackCardPosition;
            public readonly int Index;

            public BeatMapElement(PlayingCard defendCard, PlayingCardPosition attackCardPosition, int index)
            {
                AttackCardPosition = attackCardPosition;
                Index = index;
                DefendCard = defendCard;
            }
        }
    }
}
