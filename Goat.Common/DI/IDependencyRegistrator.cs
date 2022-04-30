using System;

namespace Goat.Common.DI
{
    public interface IDependencyRegistrator
    {
        void Register(IServiceCollectionWrapper services);
    }
}
