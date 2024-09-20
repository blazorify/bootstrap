using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace System.Reflection {
	public static class AssemblyExtensions {
		private static Dictionary<Assembly, String[]> AssemblyResourceNames = [];
		public static Boolean ResourceExists(this Assembly assembly, String resourceName) {
			if (!AssemblyResourceNames.ContainsKey(assembly)) {
				AssemblyResourceNames.Add(assembly, assembly.GetManifestResourceNames());
			}

			return AssemblyResourceNames[assembly].Any(name => name.Equals(resourceName, StringComparison.OrdinalIgnoreCase));
		}

		public static String GetResourceAsText(this Assembly assembly, String resourceName) {
			using (var stream = assembly.GetManifestResourceStream(resourceName)) {
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
