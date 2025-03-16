using System;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiBreadcrumbItem : BuiContentComponentBase {
		[Parameter]
		public String? Href { get; set; }

		[Parameter]
		[BindClass("active", true)]
		public Boolean Active { get; set; } = false;
	}
}
