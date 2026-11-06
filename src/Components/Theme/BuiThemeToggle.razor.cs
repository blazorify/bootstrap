using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	/// <summary>
	/// Light / dark / auto color mode switcher, modeled on the toggler in the Bootstrap docs.
	/// Renders a dropdown by default, or a single button that cycles through the modes.
	/// </summary>
	public partial class BuiThemeToggle : BuiComponentBase, IDisposable {
		[Inject]
		private BuiThemeService themeService { get; set; } = default!;

		/// <summary>
		/// <c>true</c> (default) for a dropdown with one item per mode; <c>false</c> for a button that cycles through them.
		/// </summary>
		[Parameter]
		public Boolean Dropdown { get; set; } = true;

		/// <summary>
		/// Offers <see cref="ColorMode.Auto"/> (follow the operating system). Without it the button toggles between light and dark.
		/// </summary>
		[Parameter]
		public Boolean IncludeAuto { get; set; } = true;

		[Parameter]
		public Variant Variant { get; set; } = Variant.None;

		[Parameter]
		public Size? Size { get; set; }

		/// <summary>
		/// Shows the dropdown caret.
		/// </summary>
		[Parameter]
		public Boolean Caret { get; set; } = true;

		[Parameter]
		public Placement MenuPlacement { get; set; } = Placement.End;

		/// <summary>
		/// Accessible name of the toggle; visually hidden unless <see cref="ShowLabel"/> is set.
		/// </summary>
		[Parameter]
		public String Label { get; set; } = "Toggle theme";

		[Parameter]
		public Boolean ShowLabel { get; set; } = false;

		[Parameter]
		public String LightLabel { get; set; } = "Light";

		[Parameter]
		public String DarkLabel { get; set; } = "Dark";

		[Parameter]
		public String AutoLabel { get; set; } = "Auto";

		/// <summary>
		/// Bootstrap Icons names (without the <c>bi-</c> prefix).
		/// </summary>
		[Parameter]
		public String LightIcon { get; set; } = "sun-fill";

		[Parameter]
		public String DarkIcon { get; set; } = "moon-stars-fill";

		[Parameter]
		public String AutoIcon { get; set; } = "circle-half";

		/// <summary>
		/// Raised after the user picks a mode.
		/// </summary>
		[Parameter]
		public EventCallback<ColorMode> OnChange { get; set; }

		/// <summary>
		/// The current mode, or <c>null</c> until it is read after the first render.
		/// </summary>
		public ColorMode? CurrentMode { get; private set; }

		private IReadOnlyList<ColorMode> Modes => this.IncludeAuto
			? [ColorMode.Light, ColorMode.Dark, ColorMode.Auto]
			: [ColorMode.Light, ColorMode.Dark];

		protected override async Task OnAfterRenderAsync(Boolean firstRender) {
			await base.OnAfterRenderAsync(firstRender);

			if (firstRender) {
				this.themeService.Changed += this.HandleChanged;
				this.CurrentMode = await this.themeService.GetModeAsync();

				await this.InvokeAsync(this.StateHasChanged);
			}
		}

		/// <summary>
		/// Applies and stores a mode, as if the user picked it.
		/// </summary>
		public async Task SetModeAsync(ColorMode mode) {
			await this.themeService.SetModeAsync(mode);

			this.CurrentMode = mode;

			await this.OnChange.InvokeAsync(mode);
		}

		/// <summary>
		/// Moves to the next mode: light, dark, auto (when included), then light again.
		/// </summary>
		public async Task CycleAsync() {
			ColorMode next;

			if (this.IncludeAuto) {
				var current = this.CurrentMode ?? await this.themeService.GetModeAsync();

				next = current switch {
					ColorMode.Light => ColorMode.Dark,
					ColorMode.Dark => ColorMode.Auto,
					_ => ColorMode.Light,
				};
			} else {
				next = await this.themeService.GetThemeAsync() == ColorMode.Dark ? ColorMode.Light : ColorMode.Dark;
			}

			await this.SetModeAsync(next);
		}

		public void Dispose() {
			this.themeService.Changed -= this.HandleChanged;

			GC.SuppressFinalize(this);
		}

		private String IconFor(ColorMode mode) {
			return mode switch {
				ColorMode.Light => this.LightIcon,
				ColorMode.Dark => this.DarkIcon,
				_ => this.AutoIcon,
			};
		}

		private String LabelFor(ColorMode mode) {
			return mode switch {
				ColorMode.Light => this.LightLabel,
				ColorMode.Dark => this.DarkLabel,
				_ => this.AutoLabel,
			};
		}

		private void HandleChanged(ColorMode mode) {
			this.CurrentMode = mode;

			_ = this.InvokeAsync(this.StateHasChanged);
		}
	}
}
