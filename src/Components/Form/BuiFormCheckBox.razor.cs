using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiFormCheckBox : BuiInputComponentBase<Boolean> {
		[Parameter]
		public RenderFragment? ChildContent { get; set; }

		protected override async Task OnParametersSetAsync() {
			await base.OnParametersSetAsync();

			if (String.IsNullOrWhiteSpace(this.ID)) {
				this.ID = $"{Guid.NewGuid()}";
			}
		}
	}
}
