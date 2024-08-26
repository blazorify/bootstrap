using System;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiCardLink : BuiComponentBase {
		[Parameter]
		public String? Href { get; set; }
	}
}
