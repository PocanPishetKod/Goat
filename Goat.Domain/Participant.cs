using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goat.Domain
{
    internal struct Participant
    {
        public readonly GamePlayer Player;
        public readonly int Order;

        public Participant(GamePlayer player, int order)
        {
            Player = player;
            Order = order;
        }
    }
}
