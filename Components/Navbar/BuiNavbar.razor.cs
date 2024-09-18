using System;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiNavbar : BuiContentComponentBase {
		[Parameter]
		public Boolean Fluid { get; set; } = false;

		[Parameter]
		public String? BrandName { get; set; }

		[Parameter]
		public RenderFragment<BuiNavbarBrand>? BuiNavbarBrand { get; set; }

		[Parameter]
		public RenderFragment<BuiNavbarCollapse>? BuiNavbarCollapse { get; set; }

		[Parameter]
		public RenderFragment<BuiNavbarNav>? BuiNavbarNav { get; set; }
	}
}
