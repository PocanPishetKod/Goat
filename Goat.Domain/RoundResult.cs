using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goat.Domain
{
    internal struct RoundResult
    {
        public readonly IReadOnlyList<PlayerRoundTotal> PlayerRoundTotals;
        public readonly GamePlayer LastAttackWinner;

        public RoundResult(IReadOnlyList<PlayerRoundTotal> playerRoundTotals, GamePlayer lastAttackWinner)
        {
            PlayerRoundTotals = playerRoundTotals;
            LastAttackWinner = lastAttackWinner;
        }
    }
}
