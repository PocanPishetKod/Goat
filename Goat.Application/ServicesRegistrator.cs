using System;
using System.Collections.Generic;
using System.Text;
using Goat.Application.ViewModels;
using Goat.Common.DI;

namespace Goat.Application
{
    internal class ServicesRegistrator : IDependencyRegistrator
    {
        public void Register(IServiceCollectionWrapper services)
        {
            services.AddTransient<MainMenuViewModel>();
            services.AddTransient<GameSettingsViewModel>();
        }
    }
}
