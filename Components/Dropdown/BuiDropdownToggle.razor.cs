using System;
using System.Threading.Tasks;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Blazorify.Bootstrap {
	public partial class BuiDropdownToggle : BuiButton {
		[Parameter]
		public override ButtonType Type { get; set; } = ButtonType.Button;

		[Parameter]
		[BindClass("show", true)]
		public Boolean Open { get; set; } = false;

		[Parameter]
		[BindClass("dropdown-toggle", true)]
		public Boolean Caret { get; set; } = true;

		[Parameter]
		[BindClass("dropdown-toggle-split", true)]
		public Boolean Split { get; set; } = false;

		[CascadingParameter]
		internal BuiDropdownSharedState? State { get; set; }

		protected override async Task OnAfterRenderAsync(Boolean firstRender) {
			await base.OnAfterRenderAsync(firstRender);

			if (firstRender) {
				ArgumentNullException.ThrowIfNull(this.State);

				this.State.OnChange(state => state.Open, open => this.Open = open);
			}
		}

		protected override async Task HandleClick(MouseEventArgs args) {
			ArgumentNullException.ThrowIfNull(this.State);

			await base.HandleClick(args);

			if (!this.Disabled) {
				await this.OnClick.InvokeAsync(args);
			}

			this.State.Open = !this.State.Open;
		}

		protected async Task HandleKeypress(KeyboardEventArgs args) {
			ArgumentNullException.ThrowIfNull(this.State);

			if (this.Disabled || !this.Open) {
				return;
			}

			if (args.Key == "Escape") {
				this.State.Open = false;
			}
		}
	}
}
