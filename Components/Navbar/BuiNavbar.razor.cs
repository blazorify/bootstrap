using System;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiNavbar : BuiContentComponentBase {
		[Parameter]
		public Boolean Fluid { get; set; } = false;

		[Parameter]
		public String? BrandName { get; set; }

		[Parameter]
		[BindClass("navbar-expand", NavbarExpand.Always)]
		[BindClass("navbar-expand-sm", NavbarExpand.SM)]
		[BindClass("navbar-expand-md", NavbarExpand.MD)]
		[BindClass("navbar-expand-lg", NavbarExpand.LG)]
		[BindClass("navbar-expand-xl", NavbarExpand.XL)]
		[BindClass("navbar-expand-xxl", NavbarExpand.XXL)]
		public NavbarExpand Expand { get; set; } = NavbarExpand.Always;

		[Parameter]
		public RenderFragment<BuiNavbarBrand>? NavbarBrand { get; set; }

		[Parameter]
		public RenderFragment<BuiNavbarCollapse>? NavbarCollapse { get; set; }

		[Parameter]
		public RenderFragment<BuiNavbarNav>? NavbarNav { get; set; }
	}
}
