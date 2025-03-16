namespace Microsoft.AspNetCore.Builder {
	public static class UseBlazorifyExtension {
		public static IApplicationBuilder UseBlazorify(this IApplicationBuilder app) {
			app.MapScriptsEndpoint();
			app.MapThemeEndpoint();

			return app;
		}
	}
}
