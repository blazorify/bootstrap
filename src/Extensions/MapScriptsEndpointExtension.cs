using System;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Builder {
	public static class MapScriptsEndpointExtension {
		/// <summary>
		/// Maps a middleware endpoint to serve a JavaScript file for Blazorify Bootstrap.
		/// </summary>
		/// <param name="app">
		/// The <see cref="IApplicationBuilder"/> used to configure the middleware pipeline.
		/// </param>
		/// <returns>
		/// Returns the configured <see cref="IApplicationBuilder"/> to allow for further method chaining.
		/// </returns>
		/// <remarks>
		/// This middleware intercepts requests to <c>/_blazorify/bootstrap/scripts.js</c>
		/// and serves the embedded JavaScript resource from the executing assembly.
		/// If an error occurs while retrieving the resource, a 500 Internal Server Error response is returned.
		/// </remarks>
		public static IApplicationBuilder MapScriptsEndpoint(this IApplicationBuilder app) {
			// Use middleware to handle scripts
			app.Use(async (context, next) => {
				var path = context.Request.Path.Value;

				// Check if the path matches the theme route
				if (path != null && path.Equals("/_blazorify/bootstrap/scripts.js", StringComparison.OrdinalIgnoreCase)) {
					var loggerFactory = context.RequestServices.GetRequiredService<ILoggerFactory>();
					var logger = loggerFactory.CreateLogger($"Blazorify.Bootstrap.MapScriptsEndpoint");

					try {
						var scriptContent = Assembly.GetExecutingAssembly().GetResourceAsText("Blazorify.Bootstrap.Resources.Scripts.scripts.js");

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
