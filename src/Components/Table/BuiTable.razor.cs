using System;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiTable : BuiContentComponentBase {
		[Parameter]
		[BindClass("table-hover", true)]
		public Boolean Hover { get; set; } = false;
	}
}
