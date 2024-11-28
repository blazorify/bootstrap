using System;
using System.Threading.Tasks;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiOffcanvas : BuiContentComponentBase {
		[Parameter]
		[BindClass("show", true)]
		public Boolean Open { get; set; } = false;

		[Parameter]
		public Boolean Closeable { get; set; } = true;

		[Parameter]
		[BindClass("offcanvas-start", Placement.Start)]
		[BindClass("offcanvas-end", Placement.End)]
		public Placement Placement { get; set; } = Placement.Start;

		[Parameter]
		public RenderFragment? Header { get; set; }

		[Parameter]
		public RenderFragment? Body { get; set; }

		[Parameter]
		public EventCallback OnShow { get; set; }

		[Parameter]
		public EventCallback OnHide { get; set; }

		public async Task Show() {
			this.Open = true;

			await this.OnShow.InvokeAsync();
			await this.InvokeAsync(this.StateHasChanged);
		}

		public async Task Hide() {
			this.Open = false;

			await this.OnHide.InvokeAsync();
			await this.InvokeAsync(this.StateHasChanged);
		}
	}
}
