using System;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiLink : BuiButton {
		[Parameter]
		public String? Href { get; set; }
	}
}
