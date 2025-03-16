using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiFormSwitch : BuiInputComponentBase<Boolean> {
		[Parameter]
		public RenderFragment? ChildContent { get; set; }

		protected override async Task OnParametersSetAsync() {
			await base.OnParametersSetAsync();

			if (String.IsNullOrWhiteSpace(this.ID)) {
				this.ID = $"{Guid.NewGuid()}";
			}
		}

		protected override async Task HandleChange(ChangeEventArgs args) {
			var value = (Boolean)args.Value!;

			await this.ValueChanged.InvokeAsync(value);
			await this.OnChange.InvokeAsync(value);
		}
	}
}
