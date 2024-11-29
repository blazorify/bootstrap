using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiBadge : BuiContentComponentBase {
		[Parameter]
		[BindClass("text-bg-primary", Variant.Primary)]
		[BindClass("text-bg-secondary", Variant.Secondary)]
		[BindClass("text-bg-success", Variant.Success)]
		[BindClass("text-bg-danger", Variant.Danger)]
		[BindClass("text-bg-warning", Variant.Warning)]
		[BindClass("text-bg-info", Variant.Info)]
		[BindClass("text-bg-light", Variant.Light)]
		[BindClass("text-bg-dark", Variant.Dark)]
		public Variant Variant { get; set; } = Variant.None;
	}
}
