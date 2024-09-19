using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Components;
using Blazorify.Bootstrap.Attributes;

namespace Blazorify.Bootstrap {
	public partial class BuiDropdownMenuItem : BuiContentComponentBase {
		[Parameter]
		[BindClass("active", true)]
		public Boolean Active { get; set; } = false;

		[Parameter]
		[BindClass("disabled", true)]
		public Boolean Disabled { get; set; } = false;

		[Parameter]
		public EventCallback<MouseEventArgs> OnClick { get; set; }

		protected async Task HandleClick(MouseEventArgs args) {
			if (!this.Disabled) {
				await this.OnClick.InvokeAsync(args);
			}
		}
	}
}
