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

		/// <summary>
		/// This tells LibSass whether this IFileManager supports converting relative paths to absolute paths.
		/// </summary>
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
			var currentDirectory = "/";

			this.logger.LogDebug("GetCurrentDirectory: {currentDirectory}", currentDirectory);

			return currentDirectory;
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

				this.logger.LogDebug("FileExists: {path} => {resourceName} = {resourceExists}", path, resourceName, resourceNamespace);

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

			if (this.options.Themes.TryGetValue(theme => theme.Namespace.Equals(resourceNamespace, StringComparison.OrdinalIgnoreCase), out var theme)) {
				this.logger.LogDebug("IsAbsolutePath: {path} => {resourceNamespace} = True", path, resourceNamespace);

				return true;
			}

			this.logger.LogDebug("IsAbsolutePath: {path} => {resourceNamespace} = False", path, resourceNamespace);

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

			var resourceNamespace = path.Split('/').FirstOrDefault();

			ArgumentNullException.ThrowIfNullOrWhiteSpace(resourceNamespace);

			var resourceName = this.GetResourcePath(path);

			if (this.options.Themes.TryGetValue(theme => theme.Namespace.Equals(resourceNamespace, StringComparison.OrdinalIgnoreCase), out var theme)) {
				this.logger.LogDebug("FileExists: {path} => {resourceName}", path, resourceName);

				return theme.Assembly.GetResourceAsText(resourceName);
			}

			var exception = new FileNotFoundException($"Resource file '{resourceName}' not found");

			this.logger.LogError(exception, exception.Message);

			throw exception;
		}

		private String GetResourcePath(String path) {
			var resourcePath = path.Replace("/", ".").Replace("\\", ".");

			this.logger.LogDebug("GetResourcePath: {path} => {resourcePath} = True", path, resourcePath);

			return resourcePath;
		}
	}
}
