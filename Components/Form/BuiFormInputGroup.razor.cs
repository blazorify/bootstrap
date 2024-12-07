using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiFormInputGroup : BuiContentComponentBase {
		[Parameter]
		public RenderFragment? Prefix { get; set; }

		[Parameter]
		public RenderFragment? Suffix { get; set; }

		protected override async Task OnParametersSetAsync() {
			await base.OnParametersSetAsync();
		}
	}
}
