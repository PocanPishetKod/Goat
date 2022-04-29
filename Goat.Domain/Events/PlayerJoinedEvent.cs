using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goat.Domain.Events
{
    public struct PlayerJoinedEvent
    {
        public readonly Player Player;

        public PlayerJoinedEvent(Player player)
        {
            Player = player;
        }
    }
}
