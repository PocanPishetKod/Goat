using System;
using System.Collections.Generic;

namespace Goat.Domain
{
    internal class RoundTotalCalculator
    {
        public IReadOnlyList<PlayerRoundTotal> Caclulate(IReadOnlyList<GamePlayer> players)
        {
            var result = new List<PlayerRoundTotal>();
            var maxPointsPlayer = FindPlayerWithMaxPoints(players);

            foreach (var player in players)
            {
                result.Add(CalculatePoints(player, maxPointsPlayer, players.Count));
            }

            return result;
        }

        private PlayerRoundTotal CalculatePoints(GamePlayer player, GamePlayer? maxPointsPlayer, int playersCount)
        {
            if (maxPointsPlayer != null && player.Id == maxPointsPlayer.Id)
                return new PlayerRoundTotal(player, 0);

            if (player.DiscardPilePoints == 0)
                return new PlayerRoundTotal(player, 6);

            if (playersCount == 3 && player.DiscardPilePoints <= 21 || playersCount != 3 && player.DiscardPilePoints <= 31)
                return new PlayerRoundTotal(player, 4);

            if (player.DiscardPilePoints > 31)
                return new PlayerRoundTotal(player, 2);

            throw new InvalidOperationException("Invalid calculate player points");
        }

        private GamePlayer? FindPlayerWithMaxPoints(IReadOnlyList<GamePlayer> players)
        {
            var maxPointsPlayer = players[0];
            for (int i = 1; i < players.Count; i++)
            {
                if (players[i].DiscardPilePoints > maxPointsPlayer.DiscardPilePoints)
                    maxPointsPlayer = players[i];
            }

            foreach (var player in players)
            {
                if (player.Id == maxPointsPlayer.Id)
                    continue;

                if (player.DiscardPilePoints == maxPointsPlayer.DiscardPilePoints)
                    return null;
            }

            return maxPointsPlayer;
        }
    }

    internal struct PlayerRoundTotal
    {
        public readonly GamePlayer Player;
        public readonly int PointsAwarded;

        public PlayerRoundTotal(GamePlayer player, int pointsAwarded)
        {
            Player = player;
            PointsAwarded = pointsAwarded;
        }
    }
}
