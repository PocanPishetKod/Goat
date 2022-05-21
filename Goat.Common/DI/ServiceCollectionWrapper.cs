using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Goat.Common.DI
{
    public class ServiceCollectionWrapper : IServiceCollectionWrapper
    {
        private readonly IServiceCollection _services;

        public ServiceCollectionWrapper(IServiceCollection services)
        {
            _services = services;
        }

        public IServiceCollectionWrapper AddTransient<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            _services.AddTransient<TInterface, TImplementation>();
            return this;
        }

        public IServiceCollectionWrapper AddSingleton<TService>(TService instance)
            where TService : class
        {
            _services.AddSingleton(instance);
            return this;
        }

        IServiceCollectionWrapper IServiceCollectionWrapper.AddSingleton<TInterface, TImplementation>()
        {
            _services.AddSingleton<TInterface, TImplementation>();
            return this;
        }

        public IServiceCollectionWrapper AddTransient<TImplementation>()
            where TImplementation : class
        {
            _services.AddTransient<TImplementation>();
            return this;
        }
    }
}
