using System;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap.Components {
	public partial class BuiModalFooter : ComponentBase {
		[Parameter]
		public String? ID { get; set; }

		[Parameter]
		public String? Class { get; set; }

		[Parameter]
		public String? Style { get; set; }

		[Parameter]
		public RenderFragment? ChildContent { get; set; }

		[CascadingParameter]
		public BuiModal? Modal { get; set; }
	}
}
