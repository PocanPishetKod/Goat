using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Goat.Common.DI
{
	internal interface ICustomAssemblyFinder
    {
		IReadOnlyCollection<Assembly> GetCustomAssemblies();
	}

    internal class CustomAssemblyFinder : ICustomAssemblyFinder
    {
		public IReadOnlyCollection<Assembly> GetCustomAssemblies()
		{
			var foundedAssemblies = new List<Assembly>();
			var currentAssembly = Assembly.GetExecutingAssembly();

			foundedAssemblies.Add(currentAssembly);

			var ra = currentAssembly.GetReferencedAssemblies();
			return GetCustomReferencesAssemblies(currentAssembly, foundedAssemblies);
		}

		private IReadOnlyCollection<Assembly> GetCustomReferencesAssemblies(
			Assembly target, List<Assembly> foundedAssemblies)
		{
			foreach (var referencedAssembly in target.GetReferencedAssemblies().Where(a => a.Name.StartsWith("Goat.")))
			{
				var assembly = foundedAssemblies.FirstOrDefault(a => a.FullName == referencedAssembly.FullName);
				if (assembly == null)
				{
					assembly = Assembly.Load(referencedAssembly);
					foundedAssemblies.Add(assembly);
				}

				foundedAssemblies.AddRange(GetCustomReferencesAssemblies(assembly, foundedAssemblies));
			}

			return foundedAssemblies;
		}
	}
}
