using System;
using System.Threading.Tasks;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Blazorify.Bootstrap {
	public partial class BuiButton : BuiContentComponentBase {
		[Parameter]
		public virtual ButtonType Type { get; set; } = ButtonType.Button;

		[Parameter]
		public Size? Size { get; set; }

		[Parameter]
		[BindClass("btn-primary", Variant.Primary)]
		[BindClass("btn-secondary", Variant.Secondary)]
		[BindClass("btn-success", Variant.Success)]
		[BindClass("btn-danger", Variant.Danger)]
		[BindClass("btn-warning", Variant.Warning)]
		[BindClass("btn-info", Variant.Info)]
		[BindClass("btn-light", Variant.Light)]
		[BindClass("btn-dark", Variant.Dark)]
		[BindClass("btn-link", Variant.Link)]
		public Variant Variant { get; set; } = Variant.None;

		[Parameter]
		[BindClass("active", true)]
		public Boolean Active { get; set; } = false;

		[Parameter]
		public Boolean Outline { get; set; } = false;

		[Parameter]
		[BindClass("disabled", true)]
		public Boolean Disabled { get; set; } = false;

		[Parameter]
		public Boolean Toggle { get; set; } = false;

		[Parameter]
		public EventCallback<MouseEventArgs> OnClick { get; set; }

		protected virtual async Task HandleClick(MouseEventArgs args) {
			if (!this.Disabled) {
				await this.OnClick.InvokeAsync(args);
			}
		}
	}
}
