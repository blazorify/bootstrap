using Blazorify.Bootstrap.Services;

namespace Microsoft.Extensions.DependencyInjection {
	public static class AddBlazorifyExtension {
		public static IServiceCollection AddBlazorify(this IServiceCollection services) {
			services.AddSingleton<BuiModalService>();

			return services;
		}
	}
}
