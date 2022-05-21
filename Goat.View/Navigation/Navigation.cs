using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goat.Application.ViewModels;

namespace Goat.View.Navigation
{
    internal class Navigation : Application.Interfaces.INavigation
    {
        private readonly INavigationAssotiationsProvider _navigationAssotiationsProvider;
        private readonly IServiceProvider _serviceProvider;

        public Navigation(INavigationAssotiationsProvider navigationAssotiationsProvider, IServiceProvider serviceProvider)
        {
            _navigationAssotiationsProvider = navigationAssotiationsProvider;
            _serviceProvider = serviceProvider;
        }

        public INavigation PageNavigation => App.Current.MainPage.Navigation;

        public Page CurrentPage => ((NavigationPage)App.Current.MainPage).CurrentPage;

        public async Task Back()
        {
            if (CurrentPageIsModal())
            {
                await PageNavigation.PopModalAsync();
            }
            else
            {
                await PageNavigation.PopAsync();
            }
        }

        public async Task Navigate<TViewModel>() where TViewModel : ViewModelBase
        {
            await PageNavigation.PushAsync(FindPage<TViewModel>());
        }

        public async Task NavigateModal<TViewModel>() where TViewModel : ViewModelBase
        {
            await PageNavigation.PushModalAsync(FindPage<TViewModel>());
        }

        private Page FindPage<TViewModel>() where TViewModel : ViewModelBase
        {
            var pageType = _navigationAssotiationsProvider.GetViewType<TViewModel>();

            if (pageType == null)
                throw new InvalidOperationException($"View not registered for viewModel {typeof(TViewModel).Name}");

            var page = _serviceProvider.GetService(pageType);

            if (page == null)
                throw new InvalidOperationException($"Page {pageType.Name} not registered");

            return (Page)page;
        }

        private bool CurrentPageIsModal()
        {
            var lastModalPage = PageNavigation.ModalStack.LastOrDefault();

            if (lastModalPage == null || lastModalPage != CurrentPage)
                return false;

            return true;
        }
    }
}
