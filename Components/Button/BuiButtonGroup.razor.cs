using System;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiButtonGroup : BuiContentComponentBase {
		[Parameter]
		[BindClass("btn-group-sm", Bootstrap.Size.Small)]
		[BindClass("btn-group-lg", Bootstrap.Size.Large)]
		public Size? Size { get; set; }

		[Parameter]
		[BindClass("btn-group", false)]
		[BindClass("btn-group-vertical", true)]
		public Boolean Vertical { get; set; } = false;
	}
}
