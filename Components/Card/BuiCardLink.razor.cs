using System;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiCardLink : BuiContentComponentBase {
		[Parameter]
		public String? Href { get; set; }
	}
}
