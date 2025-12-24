using System;
using System.Runtime.CompilerServices;
using Blazorify.Bootstrap;
using Blazorify.Sass;

namespace Microsoft.Extensions.DependencyInjection {
	public static class AddBlazorifyExtension {
		public static IServiceCollection AddBlazorify(this IServiceCollection services) {
			services.AddScoped<BuiModalService>();
			services.AddScoped<BuiToastService>();
			services.AddSingleton<ResourceFileManager>();
			services.AddSingleton<SassCompiler>();

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
