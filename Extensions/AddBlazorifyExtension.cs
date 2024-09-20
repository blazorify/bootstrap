using System;
using System.Runtime.CompilerServices;
using Blazorify.Bootstrap;
using Blazorify.Bootstrap.Services;
using LibSassHost;

namespace Microsoft.Extensions.DependencyInjection {
	public static class AddBlazorifyExtension {
		public static IServiceCollection AddBlazorify(this IServiceCollection services) {
			services.AddSingleton<BuiModalService>();
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
