using LibSassHost;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Blazorify.Bootstrap {
	public class ResourceFileManager : IFileManager {
		private readonly BootstrapOptions options;
		private readonly ILogger<ResourceFileManager> logger;

		private String currentDirectory = "/";

		/// <inheritdoc/>
		public Boolean SupportsConversionToAbsolutePath => false;

		public ResourceFileManager(
			IOptions<BootstrapOptions> optionsAccessor,
			ILogger<ResourceFileManager> logger
		) {
			this.options = optionsAccessor.Value;
			this.logger = logger;
		}

		/// <inheritdoc/>
		public String GetCurrentDirectory() {
			this.logger.LogDebug("GetCurrentDirectory: {currentDirectory}", this.currentDirectory);

			return this.currentDirectory;
		}

		/// <inheritdoc/>
		public Boolean FileExists(String path) {
			var resourceNamespace = path.Split('/').FirstOrDefault();

			if (String.IsNullOrWhiteSpace(resourceNamespace)) {
				return false;
			}

			if (this.options.Themes.TryGetValue(theme => theme.Namespace.Equals(resourceNamespace, StringComparison.OrdinalIgnoreCase), out var theme)) {
				var resourceName = this.GetResourcePath(path);
				var resourceExists = theme.Assembly.ResourceExists(resourceName);

				if (resourceExists) {
					this.logger.LogDebug("FileExists: {path}", path);
				}

				return resourceExists;
			}

			return false;
		}

		/// <inheritdoc/>
		public Boolean IsAbsolutePath(String path) {
			var resourceNamespace = path.Split('/').FirstOrDefault();

			if (String.IsNullOrWhiteSpace(resourceNamespace)) {
				return false;
			}

			return this.options.Themes.Values.Any(theme => theme.Namespace.Equals(resourceNamespace, StringComparison.OrdinalIgnoreCase));
		}

		/// <inheritdoc/>
		public String ToAbsolutePath(String path) {
			this.logger.LogDebug("ToAbsolutePath: {path}", path);
			return path;
		}

		/// <inheritdoc/>
		public String GetDirectoryName(String path) {
			this.logger.LogDebug("GetDirectoryName: {path}", path);
			return String.Empty;
		}

		/// <inheritdoc/>
		public String ReadFile(String path) {
			this.logger.LogDebug("ReadFile: {path}", path);

			var resourceNamespace = path.Split('/').FirstOrDefault();

			ArgumentNullException.ThrowIfNullOrWhiteSpace(resourceNamespace);

			var resourceName = this.GetResourcePath(path);

			if (this.options.Themes.TryGetValue(theme => theme.Namespace.Equals(resourceNamespace, StringComparison.OrdinalIgnoreCase), out var theme)) {
				this.currentDirectory = Path.GetDirectoryName(path)?.Replace('\\', '/') ?? "/";

				return theme.Assembly.GetResourceAsText(resourceName);
			}

			var exception = new FileNotFoundException($"Resource file '{resourceName}' not found");

			this.logger.LogError(exception, exception.Message);

			throw exception;
		}

		private String GetResourcePath(String path) {
			return path.Replace("/", ".").Replace("\\", ".");
		}
	}
}
