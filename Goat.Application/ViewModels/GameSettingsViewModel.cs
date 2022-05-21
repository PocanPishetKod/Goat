using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Goat.Application.Interfaces;

namespace Goat.Application.ViewModels
{
    public class GameSettingsViewModel : ViewModelBase
    {
        private readonly INavigation _navigation;

        public GameSettingsViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        private ICommand? _navigateBackCommand;
        public ICommand NavigateBackCommand
        {
            get
            {
                if (_navigateBackCommand == null)
                    _navigateBackCommand = new Command(async () => await NavigateBack());

                return _navigateBackCommand;
            }
        }

        public async Task NavigateBack()
        {
            await _navigation.Back();
        }
    }
}
