using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goat.Application.Interfaces;
using Goat.Application.ViewModels;
using Goat.Common.DI;
using Goat.View.Navigation;

namespace Goat.View
{
    internal class ServicesRegistrator : IDependencyRegistrator
    {
        public void Register(IServiceCollectionWrapper services)
        {
            services.AddSingleton<Application.Interfaces.INavigation, Navigation.Navigation>()
                .AddSingleton<INavigationAssotiationsProvider, NavigationAssotiationsProvider>();
        }
    }
}
