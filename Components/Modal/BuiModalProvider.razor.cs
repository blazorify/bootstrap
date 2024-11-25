using System;
using System.Threading.Tasks;
using Blazorify.Bootstrap.Services;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiModalProvider : BuiComponentBase {
		[Inject]
		private BuiModalService? modalService { get; set; }

		protected override async Task OnAfterRenderAsync(Boolean firstRender) {
			ArgumentNullException.ThrowIfNull(this.modalService);

			await base.OnAfterRenderAsync(firstRender);

			this.modalService.PropertyChanged += async (s, e) => {
				await this.InvokeAsync(StateHasChanged);
			};
		}
	}
}
