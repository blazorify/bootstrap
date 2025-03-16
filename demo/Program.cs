using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Blazorify.Demo {
	public class Program {
		private static void Main(String[] args) {
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddRazorPages();
			builder.Services.AddServerSideBlazor();

			builder.Services.AddBlazorify(options => {
				options.Themes.Add("custom", new() {
					Assembly = Assembly.GetExecutingAssembly(),
					Namespace = "Blazorify.Demo.Resources.Styles",
				});
			});

			var app = builder.Build();

			if (!app.Environment.IsDevelopment()) {
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.MapThemeEndpoint();

			app.UseRouting();

			app.MapBlazorHub();
			app.MapFallbackToPage("/_Host");

			app.Run();
		}
	}
}