using System;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Demo.Components {
	public partial class DemoExample : ComponentBase {
		[Parameter]
		public String? Class { get; set; }

		[Parameter]
		public RenderFragment? ChildContent { get; set; }
	}
}
