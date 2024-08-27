using System;
using System.Threading.Tasks;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Blazorify.Bootstrap {
	public partial class BuiAlert : BuiContentComponentBase {
		[Parameter]
		[BindClass("alert-primary", Variant.Primary)]
		[BindClass("alert-secondary", Variant.Secondary)]
		[BindClass("alert-success", Variant.Success)]
		[BindClass("alert-danger", Variant.Danger)]
		[BindClass("alert-warning", Variant.Warning)]
		[BindClass("alert-info", Variant.Info)]
		[BindClass("alert-light", Variant.Light)]
		[BindClass("alert-dark", Variant.Dark)]
		public Variant Variant { get; set; } = Variant.None;

		[Parameter]
		[BindClass("alert-dismissible", true)]
		public Boolean Dismissible { get; set; } = false;

		[Parameter]
		public EventCallback<MouseEventArgs> OnDismiss { get; set; }

		protected virtual async Task HandleDismiss(MouseEventArgs args) {
			await this.OnDismiss.InvokeAsync(args);
		}
	}
}
