using System;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiNavItem : BuiContentComponentBase {
		[Parameter]
		[BindClass("active", true)]
		public Boolean Active { get; set; } = false;

		[Parameter]
		[BindClass("disabled", true)]
		public Boolean Disabled { get; set; } = false;

		[Parameter]
		public String? Href { get; set; }
	}
}
