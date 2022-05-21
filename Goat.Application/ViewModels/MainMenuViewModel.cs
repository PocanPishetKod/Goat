using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Goat.Application.Interfaces;

namespace Goat.Application.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        private readonly INavigation _navigation;

        public MainMenuViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        private ICommand? _startOfflineGameCommand;
        public ICommand StartOfflineGameCommand
        {
            get
            {
                if (_startOfflineGameCommand == null)
                    _startOfflineGameCommand = new Command(async () => await StartOfflaneGame());

                return _startOfflineGameCommand;
            }
        }

        public async Task StartOfflaneGame()
        {
            await _navigation.NavigateModal<GameSettingsViewModel>();
        }
    }
}
