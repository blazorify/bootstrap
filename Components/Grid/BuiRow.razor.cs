using System;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiRow : BuiContentComponentBase {
		[Parameter]
		[BindClass("justify-content-start", Justify.Start)]
		[BindClass("justify-content-end", Justify.End)]
		[BindClass("justify-content-center", Justify.Center)]
		[BindClass("justify-content-between", Justify.Between)]
		[BindClass("justify-content-around", Justify.Around)]
		[BindClass("justify-content-evenly", Justify.Evenly)]
		public Justify Justify { get; set; }

		[Parameter]
		[BindClass("row-cols-{0}")]
		public String? Cols { get; set; }
	}
}
