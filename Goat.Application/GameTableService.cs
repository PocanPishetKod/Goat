using System;
using System.Collections;
using System.Collections.Generic;
using Goat.Domain;

namespace Goat.Application
{
    public class GameTableService : IGameTableService
    {
        private GameTable? _currentGameTable;

        public uint CreateGameTable(int placesCount)
        {
            if (_currentGameTable != null)
                throw new InvalidOperationException("Game table already created");

            _currentGameTable = new GameTable(placesCount, 1);
            return _currentGameTable.Id;
        }
    }
}
