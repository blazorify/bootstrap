using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public abstract class BuiContentComponentBase : BuiComponentBase {
		[Parameter]
		public RenderFragment? ChildContent { get; set; }
	}
}
