using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Blazorify.Bootstrap {
	public class ResourceFileManager {
		private readonly ILogger<ResourceFileManager> logger;
		private readonly ConcurrentDictionary<String, String> extractedThemes = new(StringComparer.OrdinalIgnoreCase);

		public ResourceFileManager(ILogger<ResourceFileManager> logger) {
			this.logger = logger;
		}

		/// <summary>
		/// Ensures the embedded SCSS resources for a theme exist on disk and returns the root directory.
		/// </summary>
		public String EnsureThemeOnDisk(BootstrapThemeOptions theme) {
			return this.extractedThemes.GetOrAdd(theme.Namespace, _ => {
				var tempRoot = Path.Combine(Path.GetTempPath(), "blazorify-bootstrap", theme.Namespace);
				this.logger.LogDebug("Extracting SCSS resources for {namespace} to {path}", theme.Namespace, tempRoot);
				Directory.CreateDirectory(tempRoot);

				var prefix = $"{theme.Namespace}.";
				var resourceNames = theme.Assembly.GetManifestResourceNames()
					.Where(name => name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
					.Where(name => name.EndsWith(".scss", StringComparison.OrdinalIgnoreCase));

				foreach (var resourceName in resourceNames) {
					var relativeName = resourceName.Substring(prefix.Length);
					var relativePath = this.GetRelativePathFromResource(relativeName);
					var outputPath = Path.Combine(tempRoot, relativePath);

					Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);

					var bytes = theme.Assembly.GetResource(resourceName);
					File.WriteAllBytes(outputPath, bytes);
				}

				return tempRoot;
			});
		}

		private String GetRelativePathFromResource(String resourceName) {
			var resourceParts = resourceName.Split('.', StringSplitOptions.RemoveEmptyEntries);

			if (resourceParts.Length < 2) {
				return resourceName;
			}

			var directories = resourceParts.Take(resourceParts.Length - 2);
			var fileName = $"{resourceParts[^2]}.{resourceParts[^1]}";

			if (directories.Any()) {
				return Path.Combine(Path.Combine(directories.ToArray()), fileName);
			}

			return fileName;
		}
	}
}
