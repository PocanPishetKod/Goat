using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goat.Domain
{
    public class Player
    {
        public uint Id { get; }

        public Player(uint id)
        {
            Id = id;
        }
    }
}
