using System;
using System.Collections.Generic;
using System.Text;

namespace Goat.Common.DI
{
    public interface IServiceCollectionWrapper
    {
        IServiceCollectionWrapper AddTransient<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface;

        IServiceCollectionWrapper AddSingleton<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface;

        IServiceCollectionWrapper AddSingleton<TService>(TService instance)
            where TService : class;
    }
}
