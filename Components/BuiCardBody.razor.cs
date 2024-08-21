using System;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap.Components {
	public partial class BuiCardBody : ComponentBase {
		[Parameter]
		public String ID { get; set; } = String.Empty;

		[Parameter]
		public String Class { get; set; } = String.Empty;

		[Parameter]
		public String Style { get; set; } = String.Empty;

		[Parameter]
		public RenderFragment? ChildContent { get; set; }
	}
}
