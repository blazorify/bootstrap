using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Blazorify.Bootstrap {
	/// <summary>
	/// Reads and changes the page's Bootstrap color mode (light, dark or auto).
	/// The choice is stored in <c>localStorage</c> under the key <c>theme</c>, like the Bootstrap docs do,
	/// and applied as <c>data-bs-theme</c> on the <c>&lt;html&gt;</c> element.
	/// </summary>
	/// <remarks>
	/// Uses JavaScript interop, so call it after the first render (for example in <c>OnAfterRenderAsync</c>), not while prerendering.
	/// For a page without a flash of the wrong theme, also load <c>/_blazorify/bootstrap/color-mode.js</c> in <c>&lt;head&gt;</c>.
	/// </remarks>
	public class BuiThemeService : IAsyncDisposable {
		internal const String ScriptResourceName = "Blazorify.Bootstrap.Resources.Scripts.colorMode.js";

		private readonly IJSRuntime jsRuntime;
		private DotNetObjectReference<BuiThemeService>? dotNetObjectReference = null;
		private Task? initialization = null;
		private Int32? subscriptionID = null;

		public BuiThemeService(IJSRuntime jsRuntime) {
			this.jsRuntime = jsRuntime;
		}

		/// <summary>
		/// The last mode read or set through this service, or <c>null</c> before the first call.
		/// </summary>
		public ColorMode? Mode { get; private set; }

		/// <summary>
		/// Raised when the mode changes, from this service or from another browser tab.
		/// </summary>
		public event Action<ColorMode>? Changed;

		/// <summary>
		/// Gets the chosen mode, which can be <see cref="ColorMode.Auto"/>.
		/// </summary>
		public async Task<ColorMode> GetModeAsync() {
			await this.EnsureInitializedAsync();

			var mode = Parse(await this.jsRuntime.InvokeAsync<String?>("BlazorifyColorMode.getMode"));

			this.Mode = mode;

			return mode;
		}

		/// <summary>
		/// Gets the mode the page is shown in: <see cref="ColorMode.Light"/> or <see cref="ColorMode.Dark"/>,
		/// with <see cref="ColorMode.Auto"/> resolved from the operating system's preference.
		/// </summary>
		public async Task<ColorMode> GetThemeAsync() {
			await this.EnsureInitializedAsync();

			var theme = Parse(await this.jsRuntime.InvokeAsync<String?>("BlazorifyColorMode.getTheme"));

			return theme == ColorMode.Dark ? ColorMode.Dark : ColorMode.Light;
		}

		/// <summary>
		/// Applies the mode to the page and stores it for later visits.
		/// </summary>
		public async Task SetModeAsync(ColorMode mode) {
			await this.EnsureInitializedAsync();

			await this.jsRuntime.InvokeVoidAsync("BlazorifyColorMode.setMode", ToValue(mode));

			this.SetMode(mode);
		}

		/// <summary>
		/// Switches between light and dark, based on the mode the page is shown in.
		/// </summary>
		/// <returns>The new mode.</returns>
		public async Task<ColorMode> ToggleAsync() {
			var mode = await this.GetThemeAsync() == ColorMode.Dark ? ColorMode.Light : ColorMode.Dark;

			await this.SetModeAsync(mode);

			return mode;
		}

		/// <summary>
		/// Called from JavaScript when another browser tab changes the mode.
		/// </summary>
		[JSInvokable]
		public void HandleModeChanged(String? mode) {
			this.SetMode(Parse(mode));
		}

		public async ValueTask DisposeAsync() {
			GC.SuppressFinalize(this);

			if (this.subscriptionID != null) {
				try {
					await this.jsRuntime.InvokeVoidAsync("BlazorifyColorMode.unsubscribe", this.subscriptionID);
				} catch (JSDisconnectedException) {
					// The circuit is gone, and the browser side with it.
				} catch (TaskCanceledException) {
				}
			}

			this.dotNetObjectReference?.Dispose();
		}

		/// <summary>
		/// The <c>data-bs-theme</c> / storage value for a mode.
		/// </summary>
		public static String ToValue(ColorMode mode) {
			return mode switch {
				ColorMode.Light => "light",
				ColorMode.Dark => "dark",
				_ => "auto",
			};
		}

		/// <summary>
		/// Parses a <c>data-bs-theme</c> / storage value, falling back to <see cref="ColorMode.Auto"/>.
		/// </summary>
		public static ColorMode Parse(String? value) {
			return value?.Trim().ToLowerInvariant() switch {
				"light" => ColorMode.Light,
				"dark" => ColorMode.Dark,
				_ => ColorMode.Auto,
			};
		}

		private void SetMode(ColorMode mode) {
			var changed = this.Mode != mode;

			this.Mode = mode;

			if (changed) {
				this.Changed?.Invoke(mode);
			}
		}

		private Task EnsureInitializedAsync() {
			// Retry on the next call if this one failed (for example while prerendering).
			if (this.initialization == null || this.initialization.IsFaulted || this.initialization.IsCanceled) {
				this.initialization = this.InitializeAsync();
			}

			return this.initialization;
		}

		private async Task InitializeAsync() {
			// Installs window.BlazorifyColorMode when the startup script is not on the page; a no-op otherwise.
			var script = typeof(BuiThemeService).Assembly.GetResourceAsText(ScriptResourceName);

			await this.jsRuntime.InvokeVoidAsync("eval", script);

			this.dotNetObjectReference ??= DotNetObjectReference.Create(this);
			this.subscriptionID = await this.jsRuntime.InvokeAsync<Int32>("BlazorifyColorMode.subscribe", this.dotNetObjectReference, nameof(this.HandleModeChanged));
		}
	}
}
