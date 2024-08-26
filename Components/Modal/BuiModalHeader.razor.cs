using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiModalHeader : BuiComponentBase {
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
