using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Blazorify.Bootstrap {
	public class BuiThemeServiceTest : BunitContext {
		public BuiThemeServiceTest() {
			this.JSInterop.Mode = JSRuntimeMode.Loose;
			this.Services.AddScoped<BuiThemeService>();
		}

		private BuiThemeService Service => this.Services.GetRequiredService<BuiThemeService>();

		[Theory]
		[InlineData("light", ColorMode.Light)]
		[InlineData("dark", ColorMode.Dark)]
		[InlineData("auto", ColorMode.Auto)]
		[InlineData(" Dark ", ColorMode.Dark)]
		[InlineData("sepia", ColorMode.Auto)]
		[InlineData(null, ColorMode.Auto)]
		public void ShouldParseValues(String? value, ColorMode expected) {
			Assert.Equal(expected, BuiThemeService.Parse(value));
		}

		[Theory]
		[InlineData(ColorMode.Light, "light")]
		[InlineData(ColorMode.Dark, "dark")]
		[InlineData(ColorMode.Auto, "auto")]
		public void ShouldConvertToValues(ColorMode mode, String expected) {
			Assert.Equal(expected, BuiThemeService.ToValue(mode));
		}

		[Fact]
		public void ShouldEmbedStartupScript() {
			var script = typeof(BuiThemeService).Assembly.GetResourceAsText(BuiThemeService.ScriptResourceName);

			Assert.Contains("window.BlazorifyColorMode", script);
			Assert.Contains("data-bs-theme", script);
		}

		[Fact]
		public async Task ShouldReadMode() {
			this.JSInterop.Setup<String?>("BlazorifyColorMode.getMode").SetResult("dark");

			var mode = await this.Service.GetModeAsync();

			Assert.Equal(ColorMode.Dark, mode);
			Assert.Equal(ColorMode.Dark, this.Service.Mode);
		}

		[Fact]
		public async Task ShouldResolveTheme() {
			this.JSInterop.Setup<String?>("BlazorifyColorMode.getTheme").SetResult("dark");

			Assert.Equal(ColorMode.Dark, await this.Service.GetThemeAsync());
		}

		[Fact]
		public async Task ShouldInstallScriptOnce() {
			await this.Service.GetModeAsync();
			await this.Service.GetModeAsync();
			await this.Service.SetModeAsync(ColorMode.Light);

			this.JSInterop.VerifyInvoke("eval", 1);
			this.JSInterop.VerifyInvoke("BlazorifyColorMode.subscribe", 1);
		}

		[Fact]
		public async Task ShouldSetModeAndRaiseChanged() {
			var changes = new List<ColorMode>();

			this.Service.Changed += changes.Add;

			await this.Service.SetModeAsync(ColorMode.Dark);
			await this.Service.SetModeAsync(ColorMode.Dark);

			var invocation = this.JSInterop.VerifyInvoke("BlazorifyColorMode.setMode", 2)[0];

			Assert.Equal("dark", invocation.Arguments[0]);
			Assert.Equal(new[] { ColorMode.Dark }, changes);
			Assert.Equal(ColorMode.Dark, this.Service.Mode);
		}

		[Fact]
		public async Task ShouldToggleBetweenLightAndDark() {
			this.JSInterop.Setup<String?>("BlazorifyColorMode.getTheme").SetResult("dark");

			var mode = await this.Service.ToggleAsync();

			Assert.Equal(ColorMode.Light, mode);
			Assert.Equal("light", this.JSInterop.VerifyInvoke("BlazorifyColorMode.setMode").Arguments[0]);
		}

		[Fact]
		public void ShouldRaiseChangedForOtherTabs() {
			var changes = new List<ColorMode>();

			this.Service.Changed += changes.Add;

			this.Service.HandleModeChanged("light");

			Assert.Equal(new[] { ColorMode.Light }, changes);
			Assert.Equal(ColorMode.Light, this.Service.Mode);
		}
	}
}
