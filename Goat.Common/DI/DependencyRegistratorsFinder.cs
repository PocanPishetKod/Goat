using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Goat.Common.DI
{
    internal interface IDependencyRegistratorsFinder
    {
        IReadOnlyCollection<IDependencyRegistrator> Find();
    }

    internal class DependencyRegistratorsFinder : IDependencyRegistratorsFinder
    {
        private readonly ICustomAssemblyFinder _assemblyFinder;

        public DependencyRegistratorsFinder(ICustomAssemblyFinder assemblyFinder)
        {
            _assemblyFinder = assemblyFinder;
        }

        public IReadOnlyCollection<IDependencyRegistrator> Find()
        {
            var result = new List<IDependencyRegistrator>();

            var assemblies = _assemblyFinder.GetCustomAssemblies();
            if (assemblies.Count == 0)
                return new List<IDependencyRegistrator>();

            foreach (var assembly in assemblies)
            {
                var targetTypes = FindTargetTypes(assembly);
                if (targetTypes.Count == 0)
                    continue;

                foreach (var type in targetTypes)
                {
                    result.Add((IDependencyRegistrator)Activator.CreateInstance(type));
                }
            }

            return result;
        }

        private IReadOnlyCollection<Type> FindTargetTypes(Assembly assembly)
        {
            return assembly.ExportedTypes
                .Where(t => !t.IsInterface && t.GetInterface(nameof(IDependencyRegistrator)) != null)
                .ToList();
        }
    }
}
