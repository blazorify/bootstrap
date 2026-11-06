using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Builder {
	public static class MapScriptsEndpointExtension {
		/// <summary>
		/// Served script paths and the embedded resources behind them.
		/// </summary>
		private static readonly Dictionary<String, String> scripts = new(StringComparer.OrdinalIgnoreCase) {
			{ "/_blazorify/bootstrap/scripts.js", "Blazorify.Bootstrap.Resources.Scripts.scripts.js" },
			{ "/_blazorify/bootstrap/color-mode.js", "Blazorify.Bootstrap.Resources.Scripts.colorMode.js" },
		};

		/// <summary>
		/// Maps a middleware endpoint to serve the JavaScript files for Blazorify Bootstrap.
		/// </summary>
		/// <param name="app">
		/// The <see cref="IApplicationBuilder"/> used to configure the middleware pipeline.
		/// </param>
		/// <returns>
		/// Returns the configured <see cref="IApplicationBuilder"/> to allow for further method chaining.
		/// </returns>
		/// <remarks>
		/// This middleware intercepts requests to <c>/_blazorify/bootstrap/scripts.js</c> (component helpers)
		/// and <c>/_blazorify/bootstrap/color-mode.js</c> (color mode startup script, meant for <c>&lt;head&gt;</c>)
		/// and serves the embedded JavaScript resources from the executing assembly.
		/// If an error occurs while retrieving the resource, a 500 Internal Server Error response is returned.
		/// </remarks>
		public static IApplicationBuilder MapScriptsEndpoint(this IApplicationBuilder app) {
			// Use middleware to handle scripts
			app.Use(async (context, next) => {
				var path = context.Request.Path.Value;

				// Check if the path matches one of the script routes
				if (path != null && scripts.TryGetValue(path, out var resourceName)) {
					var loggerFactory = context.RequestServices.GetRequiredService<ILoggerFactory>();
					var logger = loggerFactory.CreateLogger($"Blazorify.Bootstrap.MapScriptsEndpoint");

					try {
						var scriptContent = Assembly.GetExecutingAssembly().GetResourceAsText(resourceName);

						context.Response.StatusCode = StatusCodes.Status200OK;
						context.Response.Headers["Content-Type"] = "text/javascript";

						await context.Response.WriteAsync(scriptContent);
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
