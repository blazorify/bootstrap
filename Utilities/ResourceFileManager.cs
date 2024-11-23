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
			var resourceNamespaces = path.Split('/').FirstOrDefault();

			this.logger.LogDebug("ResourceNamespaces: {resourceNamespaces}", resourceNamespaces);

			if (String.IsNullOrWhiteSpace(resourceNamespaces)) {
				return false;
			}

			var resourcePath = String.Join('/', path.Split('/').Skip(1));

			this.logger.LogDebug("ResourcePath: {resourcePath}", resourcePath);

			foreach (var resourceNamespace in resourceNamespaces.Split(';')) {
				if (this.options.Themes.TryGetValue(theme => theme.Namespace.Equals(resourceNamespace, StringComparison.OrdinalIgnoreCase), out var theme)) {
					var resourceName = this.GetResourcePath($"{resourceNamespace}/{resourcePath}");
					var resourceExists = theme.Assembly.ResourceExists(resourceName);

					if (resourceExists) {
						this.logger.LogDebug("FileExists: {resourceName}", resourceName);

						return true;
					} else {
						this.logger.LogDebug("FileNotFound: {resourceName}", resourceName);
					}
				}
			}

			return false;
		}

		/// <inheritdoc/>
		public Boolean IsAbsolutePath(String path) {
			var resourceNamespaces = path.Split('/').FirstOrDefault();

			if (String.IsNullOrWhiteSpace(resourceNamespaces)) {
				return false;
			}

			foreach (var resourceNamespace in resourceNamespaces.Split(';')) {
				if (this.options.Themes.Values.Any(theme => theme.Namespace.Equals(resourceNamespace, StringComparison.OrdinalIgnoreCase))) {
					return true;
				}
			}

			return false;
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

			var resourceNamespaces = path.Split('/').FirstOrDefault();

			ArgumentNullException.ThrowIfNullOrWhiteSpace(resourceNamespaces);

			var resourcePath = String.Join('/', path.Split('/').Skip(1));

			this.logger.LogDebug("ResourcePath: {resourcePath}", resourcePath);

			foreach (var resourceNamespace in resourceNamespaces.Split(';')) {
				var resourceName = this.GetResourcePath($"{resourceNamespace}/{resourcePath}");

				if (this.options.Themes.TryGetValue(theme => theme.Namespace.Equals(resourceNamespace, StringComparison.OrdinalIgnoreCase), out var theme)) {
					if (theme.Assembly.ResourceExists(resourceName)) {
						this.currentDirectory = Path.GetDirectoryName(path)?.Replace('\\', '/') ?? "/";

						return theme.Assembly.GetResourceAsText(resourceName);
					}
				}
			}

			var exception = new FileNotFoundException($"Resource file '{path}' not found");

			this.logger.LogError(exception, exception.Message);

			throw exception;
		}

		private String GetResourcePath(String path) {
			return path.Replace("/", ".").Replace("\\", ".");
		}
	}
}
