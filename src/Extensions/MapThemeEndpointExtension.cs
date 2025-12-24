using System;
using System.Collections.Generic;
using System.IO;
using Blazorify.Bootstrap;
using Blazorify.Sass;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Builder {
	public static class MapThemeEndpointExtension {
		private static Dictionary<String, String> cache = [];

		/// <summary>
		/// Registers middleware to dynamically serve embedded CSS theme files.
		/// Maps requests to the route pattern "/_blazorify/bootstrap/{themeName}.css"
		/// and serves the corresponding theme based on the theme name provided in the URL.
		/// </summary>
		/// <param name="app">
		/// The <see cref="IApplicationBuilder"/> used to configure the middleware pipeline.
		/// </param>
		/// <returns>
		/// Returns the configured <see cref="IApplicationBuilder"/> to allow for further method chaining.
		/// </returns>
		public static IApplicationBuilder MapThemeEndpoint(this IApplicationBuilder app) {
			// Use middleware to handle the custom theme route
			app.Use(async (context, next) => {
				var path = context.Request.Path.Value;

				// Check if the path matches the theme route
				if (path != null && path.StartsWith("/_blazorify/bootstrap/") && path.EndsWith(".css")) {
					var themeName = Path.GetFileNameWithoutExtension(path);

					if (cache.ContainsKey(themeName)) {
						context.Response.StatusCode = StatusCodes.Status200OK;
						context.Response.Headers["Content-Type"] = "text/css";

						await context.Response.WriteAsync(cache[themeName]);
						return;
					}

					// Ensure theme name is not empty or null
					ArgumentException.ThrowIfNullOrWhiteSpace(themeName);

					var themeFileManager = context.RequestServices.GetRequiredService<ResourceFileManager>();
					var sassCompiler = context.RequestServices.GetRequiredService<SassCompiler>();
					var optionsAccessor = context.RequestServices.GetRequiredService<IOptions<BootstrapOptions>>();
					var loggerFactory = context.RequestServices.GetRequiredService<ILoggerFactory>();

					var logger = loggerFactory.CreateLogger($"Blazorify.Bootstrap.MapThemeEndpoint");

					try {
						var options = optionsAccessor.Value;

						if (options.Themes.TryGetValue(themeName, out var theme)) {
							var includePaths = new List<String>();
							var themeRoot = themeFileManager.EnsureThemeOnDisk(theme);

							includePaths.Add(themeRoot);

							if (options.Themes.TryGetValue("bootstrap", out var bootstrapTheme)) {
								var bootstrapRoot = themeFileManager.EnsureThemeOnDisk(bootstrapTheme);

								if (!includePaths.Exists(path => path.Equals(bootstrapRoot, StringComparison.OrdinalIgnoreCase))) {
									includePaths.Add(bootstrapRoot);
								}
							}

							var entryPath = Path.Combine(themeRoot, "index.scss");

							if (!File.Exists(entryPath)) {
								throw new FileNotFoundException($"Entry file '{entryPath}' was not found for theme '{themeName}'.");
							}

							var scssContent = File.ReadAllText(entryPath);
							var compilationOptions = new SassCompilerOptions {
								IncludePaths = includePaths,
							};

							var result = sassCompiler.Compile(scssContent, compilationOptions);

							context.Response.StatusCode = StatusCodes.Status200OK;
							context.Response.Headers["Content-Type"] = "text/css";

							cache.TryAdd(themeName, result);

							await context.Response.WriteAsync(result);
						} else {
							logger.LogInformation("Theme '{themeName}' not found", themeName);

							context.Response.StatusCode = StatusCodes.Status404NotFound;
							await context.Response.WriteAsync("Not found");
						}
					} catch (Exception ex) {
						logger.LogError(ex, ex.Message);

						context.Response.StatusCode = StatusCodes.Status500InternalServerError;
						await context.Response.WriteAsync(ex.Message);
					}

					return;
				}

				// Call the next middleware in the pipeline as route doesn't match
				await next.Invoke();
			});

			return app;
		}
	}
}
