using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goat.Application.ViewModels;
using Goat.View.Pages;

namespace Goat.View.Navigation
{
    internal struct NavigationInfo
    {
        public readonly Type ViewModelType;
        public readonly Type ViewType;

        public NavigationInfo(Type viewModelType, Type viewType)
        {
            ViewModelType = viewModelType ?? throw new ArgumentNullException(nameof(viewModelType));
            ViewType = viewType ?? throw new ArgumentNullException(nameof(viewType));
        }
    }

    internal interface INavigationAssotiationsProvider
    {
        Type? GetViewType<TViewModel>() where TViewModel : ViewModelBase;
    }

    internal class NavigationAssotiationsProvider : INavigationAssotiationsProvider
    {
        private static readonly NavigationInfo[] _navigationInfos;

        static NavigationAssotiationsProvider()
        {
            _navigationInfos = new NavigationInfo[]
            {
                new NavigationInfo(typeof(MainMenuViewModel), typeof(MainMenuPage)),
                new NavigationInfo(typeof(GameSettingsViewModel), typeof(GameSettingsPage))
            };
        }

        public Type? GetViewType<TViewModel>() where TViewModel : ViewModelBase
        {
            var viewModelType = typeof(TViewModel);
            
            foreach (var info in _navigationInfos)
            {
                if (info.ViewModelType == viewModelType)
                    return info.ViewType;
            }

            return null;
        }
    }
}
