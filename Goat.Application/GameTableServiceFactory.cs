using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goat.Application
{
    public static class GameTableServiceFactory
    {
        public static IGameTableService Create()
        {
            return new GameTableService();
        }
    }
}
