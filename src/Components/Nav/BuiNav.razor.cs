using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiNav : BuiItemsComponentBase {
		[Parameter]
		[BindClass("nav-tabs", NavType.Tabs)]
		[BindClass("nav-pills", NavType.Pills)]
		[BindClass("nav-underline", NavType.Underline)]
		public NavType Type { get; set; } = NavType.None;
	}
}
