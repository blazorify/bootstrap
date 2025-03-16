using System;
using System.Threading.Tasks;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiDropdownMenu : BuiItemsComponentBase {
		[Parameter]
		[BindClass("dropdown-menu-start", Placement.Start)]
		[BindClass("dropdown-menu-end", Placement.End)]
		public Placement Placement { get; set; }

		[Parameter]
		[BindClass("show", true)]
		public Boolean Open { get; set; } = false;

		[CascadingParameter]
		internal BuiDropdownSharedState? State { get; set; }

		protected override async Task OnAfterRenderAsync(Boolean firstRender) {
			await base.OnAfterRenderAsync(firstRender);

			if (firstRender) {
				ArgumentNullException.ThrowIfNull(this.State);

				this.State.OnChange(state => state.Open, open => this.Open = open);
			}
		}
	}
}
