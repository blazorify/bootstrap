using System;
using System.Runtime.CompilerServices;
using Blazorify.Bootstrap;

namespace Microsoft.Extensions.DependencyInjection {
	public static class AddBlazorifyExtension {
		public static IServiceCollection AddBlazorify(this IServiceCollection services) {
			services.AddSingleton<BuiModalService>();
			services.AddSingleton<BuiToastService>();
			services.AddSingleton<ResourceFileManager>();

			return services;
		}

		public static IServiceCollection AddBlazorify(
			this IServiceCollection services,
			Action<BootstrapOptions> options,
			[CallerMemberName] String? callerName = null
		) {
			services.Configure(options);

			return services.AddBlazorify();
		}
	}
}
