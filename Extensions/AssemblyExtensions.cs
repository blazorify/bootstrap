using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace System.Reflection {
	public static class AssemblyExtensions {
		private static Dictionary<Assembly, String[]> AssemblyResourceNames = [];

		private static void EnsureResourcesLoaded(this Assembly assembly) {
			if (!AssemblyResourceNames.ContainsKey(assembly)) {
				AssemblyResourceNames.Add(assembly, assembly.GetManifestResourceNames());
			}
		}

		public static Boolean ResourceExists(this Assembly assembly, String resourceName) {
			assembly.EnsureResourcesLoaded();

			return AssemblyResourceNames[assembly].Any(name => name.Equals(resourceName, StringComparison.OrdinalIgnoreCase));
		}

		public static String GetResourceAsText(this Assembly assembly, String resourceName) {
			assembly.EnsureResourcesLoaded();

			var resourceNameNormalized = AssemblyResourceNames[assembly].FirstOrDefault(
				name => name.Equals(resourceName, StringComparison.OrdinalIgnoreCase)
			);

			if (String.IsNullOrEmpty(resourceNameNormalized)) {
				throw new FileNotFoundException($"Resource '{resourceName}' was not found in the assembly '{assembly.FullName}'.");
			}

			using (var stream = assembly.GetManifestResourceStream(resourceNameNormalized)) {
				if (stream == null) {
					throw new FileNotFoundException($"Resource '{resourceName}' was not found in the assembly '{assembly.FullName}'.");
				}

				using (var reader = new StreamReader(stream)) {
					return reader.ReadToEnd();
				}
			}
		}
	}
}
