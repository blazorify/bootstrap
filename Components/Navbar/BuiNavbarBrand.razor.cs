using System;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiNavbarBrand : BuiContentComponentBase {
		[Parameter]
		public String? Href { get; set; }
	}
}
