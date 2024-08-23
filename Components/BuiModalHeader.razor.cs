using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap.Components {
	public partial class BuiModalHeader : ComponentBase {
		[Parameter]
		public String? ID { get; set; }

		[Parameter]
		public String? Class { get; set; }

		[Parameter]
		public String? Style { get; set; }

		[Parameter]
		public RenderFragment? ChildContent { get; set; }

		[CascadingParameter]
		public BuiModal? Modal { get; set; }

		protected override async Task OnParametersSetAsync() {
			ArgumentNullException.ThrowIfNull(this.Modal);

			await base.OnParametersSetAsync();
		}

		private async Task OnClose() {
			await this.Modal!.Hide();
		}
	}
}
