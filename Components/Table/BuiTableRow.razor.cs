using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiTableRow : BuiContentComponentBase {
		[Parameter]
		[BindClass("align-baseline", Align.Baseline)]
		[BindClass("align-top", Align.Top)]
		[BindClass("align-middle", Align.Middle)]
		[BindClass("align-bottom", Align.Bottom)]
		[BindClass("align-text-top", Align.TextTop)]
		[BindClass("align-text-bottom", Align.TextBottom)]
		public Align Align { get; set; } = Align.Default;
	}
}
