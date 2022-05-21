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
			var currentAssembly = Assembly.GetEntryAssembly();

			foundedAssemblies.Add(currentAssembly);

			var ra = currentAssembly.GetReferencedAssemblies();
			GetCustomReferencesAssemblies(currentAssembly, foundedAssemblies);
			return foundedAssemblies;
		}

		private void GetCustomReferencesAssemblies(
			Assembly target, List<Assembly> foundedAssemblies)
		{
			foreach (var referencedAssembly in target.GetReferencedAssemblies().Where(a => a.Name.StartsWith("Goat.")))
			{
				var assembly = foundedAssemblies.FirstOrDefault(a => a.GetName().Name == referencedAssembly.Name);
				if (assembly == null)
				{
					assembly = Assembly.Load(referencedAssembly);
					foundedAssemblies.Add(assembly);
				}

				GetCustomReferencesAssemblies(assembly, foundedAssemblies);
            }
		}
	}
}
