using System;
using System.Threading.Tasks;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Blazorify.Bootstrap {
	public partial class BuiDropdown : BuiContentComponentBase {
		[Parameter]
		[BindClass("show", true)]
		public Boolean Open { get; set; } = false;

		internal BuiDropdownSharedState State { get; set; } = new();

		protected override async Task OnAfterRenderAsync(Boolean firstRender) {
			await base.OnAfterRenderAsync(firstRender);

			if (firstRender) {
				ArgumentNullException.ThrowIfNull(this.State);

				this.State.OnChange(s => s.Open, async open => {
					this.Open = open;

					await this.ApplyBindClassAttributes();
				});
			}
		}

		public async Task HandleOpen(MouseEventArgs args) {
			ArgumentNullException.ThrowIfNull(this.State);

			// TODO: Implement transition
			this.State.Open = this.Open;

			await Task.CompletedTask;
		}

		public async Task HandleClose(MouseEventArgs args) {
			ArgumentNullException.ThrowIfNull(this.State);

			// TODO: Implement transition
			this.State.Open = this.Open;

			await Task.CompletedTask;
		}

		public async Task HandleToggle(MouseEventArgs args) {
			if (this.Open) {
				await this.HandleClose(args);
			} else {
				await this.HandleOpen(args);
			}
		}
	}
}
