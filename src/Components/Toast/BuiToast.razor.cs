using System;
using System.Threading.Tasks;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Blazorify.Bootstrap {
	public partial class BuiToast : BuiContentComponentBase {
		[Inject]
		public BuiToastService? toastService { get; set; }

		[Parameter]
		[BindClass("text-bg-primary", Variant.Primary)]
		[BindClass("text-bg-secondary", Variant.Secondary)]
		[BindClass("text-bg-success", Variant.Success)]
		[BindClass("text-bg-danger", Variant.Danger)]
		[BindClass("text-bg-warning", Variant.Warning)]
		[BindClass("text-bg-info", Variant.Info)]
		[BindClass("text-bg-light", Variant.Light)]
		[BindClass("text-bg-dark", Variant.Dark)]
		public Variant Variant { get; set; } = Variant.None;

		[Parameter]
		[BindClass("show", true)]
		public Boolean Shown { get; set; } = true;

		[Parameter]
		public String? Title { get; set; }

		[Parameter]
		public String? Icon { get; set; }

		[Parameter]
		public DateTime? Time { get; set; }

		[Parameter]
		public Boolean Dismissible { get; set; } = true;

		[Parameter]
		public EventCallback<MouseEventArgs> OnDismiss { get; set; }

		private Boolean showHeader {
			get {
				if (String.IsNullOrWhiteSpace(this.Title) == false) {
					return true;
				}

				if (String.IsNullOrWhiteSpace(this.Icon) == false) {
					return true;
				}

				if (this.Time != null) {
					return true;
				}

				return false;
			}
		}

		protected virtual async Task HandleDismiss(MouseEventArgs args) {
			ArgumentNullException.ThrowIfNull(this.toastService);

			await this.OnDismiss.InvokeAsync(args);

			await this.toastService.Hide(this.ID!);
		}
	}
}
