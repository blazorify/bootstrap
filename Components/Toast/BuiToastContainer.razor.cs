using System;
using System.Threading.Tasks;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiToastContainer : BuiContentComponentBase {
		[Inject]
		public BuiToastService? toastService { get; set; }

		[Parameter]
		[BindClass("top-0 start-0", Placement.TopStart)]
		[BindClass("top-0 start-50 translate-middle-x", Placement.TopCenter)]
		[BindClass("top-0 end-0", Placement.TopEnd)]
		[BindClass("top-50 start-0 translate-middle-y", Placement.MiddleStart)]
		[BindClass("top-50 start-50 translate-middle", Placement.MiddleCenter)]
		[BindClass("top-50 end-0 translate-middle-y", Placement.MiddleEnd)]
		[BindClass("bottom-0 start-0", Placement.BottomStart)]
		[BindClass("bottom-0 start-50 translate-middle-x", Placement.BottomCenter)]
		[BindClass("bottom-0 end-0", Placement.BottomEnd)]
		public Placement Placement { get; set; } = Placement.BottomEnd;

		[Parameter]
		public TimeSpan? AutoClose { get; set; } = TimeSpan.FromSeconds(5);

		protected override async Task OnAfterRenderAsync(Boolean firstRender) {
			ArgumentNullException.ThrowIfNull(this.toastService);

			await base.OnAfterRenderAsync(firstRender);

			if (firstRender) {
				this.toastService.Toasts.CollectionChanged += async (s, e) => {
					await this.InvokeAsync(this.StateHasChanged);
				};
			}
		}
	}
}
