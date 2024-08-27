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

		public async Task HandleOpen(MouseEventArgs args) {
			// TODO: Implement transition
			this.Open = true;

			await this.InvokeAsync(StateHasChanged);
		}

		public async Task HandleClose(MouseEventArgs args) {
			// TODO: Implement transition
			this.Open = false;

			await this.InvokeAsync(StateHasChanged);
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
