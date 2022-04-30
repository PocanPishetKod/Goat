using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Goat.Common.DI
{
    public static class ServicesInitializator
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            var registratorsFinder = new DependencyRegistratorsFinder(new CustomAssemblyFinder());
            var registrators = registratorsFinder.Find();

            if (registrators.Count == 0)
                return services;

            var wrapper = new ServiceCollectionWrapper(services);

            foreach (var registrator in registrators)
            {
                registrator.Register(wrapper);
            }

            return services;
        }
    }
}
